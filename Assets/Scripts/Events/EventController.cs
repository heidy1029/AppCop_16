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

    public delegate void TriviaCompleted(int modelId);
    public static event TriviaCompleted OnTriviaCompleted;

    public void SetTriviaStarted(int triviaId)
    {
        OnTriviaStarted?.Invoke(triviaId);
    }

    public void SetTriviaAnswered(int triviaId)
    {
        OnTriviaAnswered?.Invoke(triviaId);
    }

    public void SetTriviaCompleted(int modelId)
    {
        OnTriviaCompleted?.Invoke(modelId);
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
