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

    public delegate void TriviaStarted(int triviaId);
    public static event TriviaStarted OnTriviaStarted;

    public delegate void TriviaAnswered(int triviaId);
    public static event TriviaAnswered OnTriviaAnswered;

    public delegate void TriviaCompleted(int birdType);
    public static event TriviaCompleted OnTriviaCompleted;

    private int _currentBirdType = 1; // Default to 1

    public int GetCurrentBirdType()
    {
        return _currentBirdType;
    }

    public void SetCurrentBirdType(int birdType)
    {
        _currentBirdType = birdType;
    }

    public void SetTriviaStarted(int triviaId)
    {
        OnTriviaStarted?.Invoke(triviaId);
    }

    public void SetTriviaAnswered(int triviaId)
    {
        OnTriviaAnswered?.Invoke(triviaId);
    }

    public void SetTriviaCompleted(int birdType)
    {
        OnTriviaCompleted?.Invoke(birdType);
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
