using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Ambient[] _ambients;
    [SerializeField] private Material _originalMaterial;
    [SerializeField] private Material _lockedMaterial;
    [SerializeField] private GameObject _canvasLevelCompleted;
    [SerializeField] private Button _buttonNextLevel;

    private Dictionary<int, Ambient> _ambientDictionary;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

    private void OnTriviaStarted(int triviaId)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriviaCompleted(int triviaId, bool visibleCursor)
    {
        if (visibleCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } 
    }

    private void UnlockAmbientsUpTo()
    {
        foreach( var plane in _ambients)
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