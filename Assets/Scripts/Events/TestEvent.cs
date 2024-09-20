using UnityEngine;

public class TestEvent : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to event when script is enabled
        EventController.OnTriviaCompleted += OnTriviaCompleted;
    }

    private void OnDisable()
    {
        // Unsubscribe from event when script is disabled
        EventController.OnTriviaCompleted -= OnTriviaCompleted;
    }

    private void OnTriviaCompleted(int triviaId)
    {
        Debug.Log($"Test trivia {triviaId} completed");
    }

    private void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i)))
            {
                EventController.Instance.SetTriviaCompleted(i + 1);
                Debug.Log($"Simulating trivia {(i + 1)}");
            }
        }
    }
}
