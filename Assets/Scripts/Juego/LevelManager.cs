using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    [SerializeField] private Ambient[] _ambients;
    [SerializeField] private Material _originalMaterial;
    [SerializeField] private Material _lockedMaterial;
    [SerializeField] private GameObject _canvasLevelCompleted;
    [SerializeField] private Button _buttonNextLevel;
    [SerializeField] private GameObject _prefabCard;
    [SerializeField] private Data _data;
    [SerializeField] private bool _isMobile;
    [SerializeField] private GameObject _mobileUI;

    private Dictionary<int, Ambient> _ambientDictionary;

    public bool IsMobile => _isMobile;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

#if !UNITY_EDITOR
        _isMobile = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
#endif
    }

    private void Start()
    {
        _data = FindObjectOfType<Data>();
        if (_data == null)
        {
            Debug.LogError("No se encontró el componente 'Data' en la escena.");
        }
        if (!_isMobile)
        {
            _mobileUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            _mobileUI.SetActive(true);
        }

        // Initialize the dictionary
        _ambientDictionary = new Dictionary<int, Ambient>();

        // Populate the dictionary with Ambient objects
        foreach (var ambient in _ambients)
        {
            if (!_ambientDictionary.ContainsKey(ambient.Id))
            {
                _ambientDictionary.Add(ambient.Id, ambient);
            }
        }

        int currentBirdTypeId = EventController.Instance.GetCurrentBirdType();

        Ambient selectedAmbient = null;
        foreach (var ambient in _ambients)
        {
            if (ambient.Id == currentBirdTypeId)
            {
                selectedAmbient = ambient;
                break;
            }
        }

        if (selectedAmbient == null)
        {
            Debug.LogWarning($"No se encontró un ambiente con el ID {currentBirdTypeId}.");
            return; // Salir si no se encuentra el ambiente
        }

        List<BirdInfo> currentBirdInfos = _data.GetBirdInfos(EventController.Instance.GetCurrentBirdType());

        foreach (var info in currentBirdInfos)
        {
            Vector3 randomPosition = GetRandomPositionOnPlane(selectedAmbient.gameObject.transform); // Obtener posición aleatoria en el plano

            // Realizar un raycast para determinar la altura del plano
            RaycastHit hit;
            if (Physics.Raycast(randomPosition + Vector3.up * 100, Vector3.down, out hit, Mathf.Infinity))
            {
                // Posicionar la tarjeta 1 metro sobre el plano
                randomPosition.y = hit.point.y + 1f;

                // Instanciar la tarjeta
                var model = Instantiate(_prefabCard, randomPosition, Quaternion.Euler(0, 0, 90));
                var card = model.GetComponent<ModelTrigger>();
                var image = Resources.Load<Sprite>(info.MainImage);
                card.Configure(image, EventController.Instance.GetCurrentBirdType(), info.ModelIndex);
            }
        }

        // Unlock all ambients up to the highest unlocked trivia index
        UnlockAmbientsUpTo();

        _buttonNextLevel.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        // Subscribe to the event
        EventController.OnTriviaStarted += OnTriviaStarted;
        EventController.OnTriviaCompleted += OnTriviaCompleted;
    }

    private Vector3 GetRandomPositionOnPlane(Transform ambientTransform)
    {
        // Asegúrate de que el plano tenga un collider
        Collider planeCollider = ambientTransform.GetComponent<Collider>();

        if (planeCollider != null)
        {
            // Obtener los límites del collider
            Bounds bounds = planeCollider.bounds;

            // Definir el margen de 5 metros
            float margin = 2f;

            // Calcular los nuevos límites ajustados con el margen
            float minX = bounds.min.x + margin;
            float maxX = bounds.max.x - margin;
            float minZ = bounds.min.z + margin;
            float maxZ = bounds.max.z - margin;

            // Generar una posición aleatoria dentro de esos límites ajustados
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            return new Vector3(randomX, 0, randomZ); // Y se ajustará después con el raycast
        }

        Debug.LogWarning("El collider no se encontró en el ambiente.");
        return Vector3.zero; // Retorna un vector cero si no se puede calcular la posición
    }


    private void OnTriviaStarted(int triviaId)
    {
        if (!_isMobile)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriviaCompleted(int triviaId, bool visibleCursor)
    {
        if (visibleCursor)
        {
            if (!_isMobile)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            int nextBirdType = EventController.Instance.GetCurrentBirdType() + 1;
            EventController.Instance.SetCurrentBirdType(nextBirdType);

            _canvasLevelCompleted.SetActive(true);

            if (_ambientDictionary.TryGetValue(nextBirdType, out var nextAmbient))
            {
                nextAmbient.gameObject.GetComponent<MeshRenderer>().material = _originalMaterial;
                nextAmbient.UnlockedAmbient(nextBirdType);
            }
        }
        else
        {
            if (!_isMobile)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void UnlockAmbientsUpTo()
    {
        foreach (var plane in _ambients)
        {
            plane.GetComponent<MeshRenderer>().material = _lockedMaterial;
        }

        for (int i = 0; i <= EventController.Instance.GetCurrentBirdType(); i++)
        {
            if (_ambientDictionary.TryGetValue(i, out var ambient))
            {
                ambient.gameObject.GetComponent<MeshRenderer>().material = _originalMaterial;
                ambient.UnlockedAmbient(i);
            }
        }
    }

    private void OnDestroy()
    {
        EventController.OnTriviaStarted -= OnTriviaStarted;
        EventController.OnTriviaCompleted -= OnTriviaCompleted;
    }
}