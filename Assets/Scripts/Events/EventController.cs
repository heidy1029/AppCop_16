using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private static EventController instance;
    
    public static EventController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventController>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("EventController");
                    instance = singletonObject.AddComponent<EventController>();
                }
            }
            return instance;
        }
    }

    public delegate void TriviaCompleted(int triviaId);
    public static event TriviaCompleted OnTriviaCompleted;

    private List<int> completedTrivias = new List<int>();

    public int currentTrivia { get; private set; } = 0;

    public void SetTriviaCompleted(int triviaId)
    {
        if (!completedTrivias.Contains(triviaId))
        {
            completedTrivias.Add(triviaId);
            Debug.Log("Trivia " + triviaId + " completed.");
        }

        currentTrivia = triviaId;

        // Trigger the even
        OnTriviaCompleted?.Invoke(triviaId);
    }

    // Method to check if a trivia has been completed
    public bool IsTriviaCompleted(int triviaId)
    {
        return completedTrivias.Contains(triviaId);
    }

    // Method to obtain completed trivia
    public List<int> GetCompletedTrivias()
    {
        return new List<int>(completedTrivias);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
