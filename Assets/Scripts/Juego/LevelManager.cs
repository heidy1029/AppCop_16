using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Ambient[] _ambients;
    [SerializeField] private Material _originalMaterial;
    [SerializeField] private Material _lockedMaterial;
    [SerializeField] private bool _resetUnlockedAmbientId;

    private Dictionary<int, Ambient> _ambientDictionary;
    private int _highestUnlockedTriviaId;

    private void Awake()
    {
        var i = EventController.Instance;
    }

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

        if (_resetUnlockedAmbientId)
        {
            PlayerPrefs.SetInt("HighestUnlockedTriviaId", 1); // Reset and save it
        }

        // Load the highest unlocked trivia index from PlayerPrefs (default to 1)
        if (PlayerPrefs.HasKey("HighestUnlockedTriviaId"))
        {
            _highestUnlockedTriviaId = PlayerPrefs.GetInt("HighestUnlockedTriviaId", 1);
        }

        // Unlock all ambients up to the highest unlocked trivia index
        UnlockAmbientsUpTo(_highestUnlockedTriviaId);

        // Subscribe to the event
        EventController.OnTriviaStarted += OnTriviaStarted;
        EventController.OnTriviaAnswered += OnTriviaAnswered;
        EventController.OnTriviaCompleted += OnTriviaCompleted;
    }

    private void OnTriviaStarted(int triviaId)
    {
        Debug.Log("Started");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriviaAnswered(int triviaId)
    {
        Debug.Log("Answered");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriviaCompleted(int modelId)
    {
        Debug.Log("Completed");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Unlock the next trivia in sequence if it exists
        int nextModelId = modelId + 1;
        if (_ambientDictionary.TryGetValue(nextModelId, out var nextAmbient))
        {
            nextAmbient.gameObject.GetComponent<MeshRenderer>().material = _originalMaterial;
            nextAmbient.UnlockedAmbient(nextModelId);

            // Update the highest unlocked trivia index
            _highestUnlockedTriviaId = Mathf.Max(_highestUnlockedTriviaId, nextModelId);
            PlayerPrefs.SetInt("HighestUnlockedTriviaId", _highestUnlockedTriviaId); // Save it
        }
    }

    private void UnlockAmbientsUpTo(int maxId)
    {
        foreach( var plane in _ambients)
        {
            plane.GetComponent<MeshRenderer>().material = _lockedMaterial;
        }

        // Unlock all ambients from 0 up to maxId
        for (int i = 0; i <= maxId; i++)
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