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
        UnlockAmbientsUpTo(EventController.Instance.GetCurrentBirdType() - 1);

        _buttonNextLevel.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        // Subscribe to the event
        EventController.OnTriviaStarted += OnTriviaStarted;
        EventController.OnTriviaAnswered += OnTriviaAnswered;
        EventController.OnTriviaCompleted += OnTriviaCompleted;
    }

    private void OnTriviaStarted(int triviaId)
    {
        //Debug.Log("Started");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriviaAnswered(int triviaId)
    {
        //Debug.Log("Answered");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriviaCompleted(int birdType)
    {
        //Debug.Log("Completed");
         Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Unlock the next trivia in sequence if it exists
        int nextBirdType = birdType + 1;
        EventController.Instance.SetCurrentBirdType(nextBirdType);

        _canvasLevelCompleted.SetActive(true);

        if (_ambientDictionary.TryGetValue(nextBirdType, out var nextAmbient))
        {
            nextAmbient.gameObject.GetComponent<MeshRenderer>().material = _originalMaterial;
            nextAmbient.UnlockedAmbient(nextBirdType);
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
        EventController.OnTriviaAnswered -= OnTriviaAnswered;
        EventController.OnTriviaCompleted -= OnTriviaCompleted;
    }
}