using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Question
{
    public string questionText;
    public List<string> answers;
    public int correctAnswerIndex;
}

[System.Serializable]
public class ModelQuestions
{
    public int modelIndex;
    public List<Question> questions;
}

public class TriviaManager : MonoBehaviour
{
    public Example example;
    public GameObject triviaCanvas;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    private List<Question> currentQuestions;
    private int currentQuestionIndex;

    private void Start()
    {
        if (example == null)
        {
            example = FindObjectOfType<Example>();
        }
    }

    public void LoadQuestions(int modelIndex)
    {
        ModelQuestions modelQuestions = example.allModelQuestions.Find(m => m.modelIndex == modelIndex);

        if (modelQuestions != null)
        {
            currentQuestions = modelQuestions.questions;
            currentQuestionIndex = 0;
            triviaCanvas.SetActive(true);
            ShowNextQuestion();
        }
        else
        {
            Debug.LogWarning($"No se encontraron preguntas para el modelIndex: {modelIndex}");
            triviaCanvas.SetActive(false);
        }
    }

    private void ShowNextQuestion()
    {
        Question currentQuestion = currentQuestions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];

                int answerIndex = i; // Captura el índice de la respuesta
                answerButtons[i].onClick.RemoveAllListeners(); // Limpia los listeners previos
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex)); // Añade el listener
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }


    private void CheckAnswer(int selectedAnswerIndex)
    {
        // Verifica si la respuesta seleccionada es correcta
        if (selectedAnswerIndex == currentQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Respuesta correcta");

            // Avanza a la siguiente pregunta si la hay
            currentQuestionIndex++;

            if (currentQuestionIndex < currentQuestions.Count)
            {
                ShowNextQuestion();
            }
            else
            {
                EndTrivia();
                Debug.Log("Trivia completada");
            }
        }
        else
        {
            Debug.Log("Respuesta incorrecta");

        }
    }
    public void EndTrivia()
    {
        if (triviaCanvas != null)
        {
            triviaCanvas.SetActive(false);
        }

        // Llama al método para agregar la imagen al catálogo
        ModelTrigger modelTrigger = FindObjectOfType<ModelTrigger>();
        if (modelTrigger != null)
        {
            modelTrigger.OnTriviaCompleted();
        }

        // Restaurar el control del cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}