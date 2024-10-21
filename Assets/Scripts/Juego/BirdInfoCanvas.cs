using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BirdInfoCanvas : MonoBehaviour
{
    public GameObject canvas;
    public Button buttonClose;
    public TextMeshProUGUI birdNameText;
    public TextMeshProUGUI speciesText;
    public TextMeshProUGUI descriptionText;
    public Image mainImage;
    public TextMeshProUGUI habitatText;
    public TextMeshProUGUI dietText;
    public TextMeshProUGUI reproductionText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI funFact1Text;
    public TextMeshProUGUI funFact2Text;
    public TextMeshProUGUI locationText;
    public TextMeshProUGUI bibliographyText;
    public TextMeshProUGUI autorsonidoText;

    public AudioClip BirdSound; // Sonido del ave actual
    //public AudioClip openSound; // Sonido para abrir el canvas
    //public AudioClip closeSound; // Sonido para cerrar el canvas

    private AudioSource audioSource;
    private int currentTriviaId;
    private void Start()
{
    audioSource = GetComponent<AudioSource>();

    if (audioSource == null)
    {
        Debug.LogWarning("No hay un AudioSource en el GameObject. Agregando un AudioSource automáticamente.");
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    buttonClose.onClick.AddListener(OnBackButtonPressed);
}

    /// <summary>
    /// Actualiza la información del ave en el canvas y carga el sonido del ave.
    /// </summary>
    public void UpdateBirdInfo(
        int modelIndex,
        string birdName,
        string species,
        string description,
        string mainImagePath,
        string habitat,
        string diet,
        string reproduction,
        string size,
        string funFact1,
        string funFact2,
        string location,
        string bibliography,
        string autorsonido,
        string birdSoundPath) // Ruta del sonido del ave
    {
        currentTriviaId = modelIndex;
        birdNameText.text = birdName;
        speciesText.text = species;
        descriptionText.text = description;
        habitatText.text = habitat;
        dietText.text = diet;
        reproductionText.text = reproduction;
        sizeText.text = size;
        funFact1Text.text = funFact1;
        funFact2Text.text = funFact2;
        locationText.text = location;
        bibliographyText.text = bibliography;
        if (autorsonidoText != null) autorsonidoText.text = autorsonido;

        // Cargar y asignar los sprites desde la carpeta de recursos
        Sprite mainImageSprite = Resources.Load<Sprite>(mainImagePath);
        if (mainImageSprite != null)
        {
            mainImage.sprite = mainImageSprite;
        }
        else
        {
            Debug.LogWarning($"No se pudo cargar la imagen principal desde la ruta: {mainImagePath}");
        }

      BirdSound = Resources.Load<AudioClip>(birdSoundPath);
        if (BirdSound == null)
        {
            Debug.LogWarning($"No se pudo cargar el sonido del ave desde la ruta: {birdSoundPath}");
        }
        else
        {
            // Asignar el AudioClip al AudioSource y reproducir el sonido
            audioSource.clip = BirdSound;
            audioSource.Play();  // Reproducir el sonido del ave
        }

        ShowCanvas(true);
    }

    public void ShowCanvas(bool isActive)
    {
        canvas.SetActive(isActive);

    }

    /// <summary>
    /// Método para manejar el botón de "Volver" o similar.
    /// </summary>
    public void OnBackButtonPressed()
    {
        ShowCanvas(false);

        // Reproducir el sonido de cerrar el canvas


        TriviaManager.Instance.SetTriviaAnswered(currentTriviaId);
    }
}

