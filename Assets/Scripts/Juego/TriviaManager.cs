// Assets/Scripts/Juego/TriviaManager.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TriviaManager : MonoBehaviour
{
    private static TriviaManager _instance;
    public static TriviaManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TriviaManager>();
            }
            return _instance;
        }
    }

    [Header("Referencias del UI")]
    public BirdInfoCanvas birdInfoCanvas; // Asigna en el Inspector
    public Data data; // Asigna en el Inspector o se buscará automáticamente
    public GameObject triviaCanvas; // Asigna en el Inspector
    public TextMeshProUGUI questionText; // Asigna en el Inspector
    public Button[] answerButtons; // Asigna en el Inspector
    private List<Question> currentQuestions;

    private int currentTriviaId; // Índice del modelo actual
    private int currentQuestionIndex = 0;
    private int[] keyButton = { 0, 1, 2, 3 };

    private Dictionary<int, bool> _triviaCompleted = new Dictionary<int, bool>();

    // Agregar una nueva key birdType si no existe y agregar en esa key nuevo modelo con el índice del modelo
    public void AddModel(int birdType, int triviaId)
    {
        if (birdType == EventController.Instance.GetCurrentBirdType())
        {
            if (!_triviaCompleted.ContainsKey(triviaId))
            {
                _triviaCompleted.Add(triviaId, false);
            }
        }
    }

    public void SetModelChecked(int triviaId)
    {
        if (_triviaCompleted.ContainsKey(triviaId))
        {
            _triviaCompleted[triviaId] = true;
            Debug.Log($"Modelo {triviaId} completado.");
        }
    }

    // Comprobar si todos los modelos del modelId ya fueron recolectados
    public bool CheckAllModels()
    {
        foreach (var model in _triviaCompleted)
        {
            Debug.Log($"Modelo {model.Key} completado: {model.Value}");
            if (!model.Value)
            {
                return false;
            }
        }
        return true;
    }

    private void Start()
    {
        EventController.OnTriviaAnswered += OnTriviaAnswered;

        if (birdInfoCanvas == null)
        {
            birdInfoCanvas = FindObjectOfType<BirdInfoCanvas>();
            if (birdInfoCanvas == null)
            {
                Debug.LogError("No se encontró el canvas BirdInfoCanvas en la escena.");
            }
        }

        if (triviaCanvas == null)
        {
            Debug.LogError("No se asignó el Trivia Canvas en el Inspector.");
        }

        // Inicialmente, desactivar el canvas de trivia
        if (triviaCanvas != null)
        {
            triviaCanvas.SetActive(false);
        }

        // Asegurarse de que el canvas de información del ave esté desactivado
        if (birdInfoCanvas != null)
        {
            birdInfoCanvas.gameObject.SetActive(false);
        }

        // Inicializar los botones de respuesta
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    public void LoadQuestions(int triviaId)
    {
        var model = data.GetModelQuestions(EventController.Instance.GetCurrentBirdType());

        if (model == null || model.Count == 0)
        {
            Debug.LogError("La lista allModelQuestions en Data está vacía.");
            return;
        }

        EventController.Instance.SetTriviaStarted(triviaId);

        ModelQuestion modelQuestions = model.Find(m => m.ModelIndex == triviaId);

        if (modelQuestions != null && modelQuestions.Questions.Count > 0)
        {
            //Debug.Log($"Preguntas cargadas correctamente para el modelIndex: {modelIndex}");
            currentQuestions = modelQuestions.Questions;
            currentQuestionIndex = 0;
            currentTriviaId = triviaId;

            triviaCanvas.SetActive(true);

            birdInfoCanvas.gameObject.SetActive(false);

            ShowNextQuestion();
        }
        else
        {
            Debug.LogWarning($"No se encontraron preguntas para el modelIndex: {triviaId}");
            triviaCanvas.SetActive(false);

            ShowBirdInfo();
        }
    }

    private void ShowNextQuestion()
    {
        if (currentQuestions == null || currentQuestions.Count == 0)
        {
            Debug.LogError("No hay preguntas cargadas para mostrar.");
            return;
        }

        Question currentQuestion = currentQuestions[currentQuestionIndex];
        questionText.text = currentQuestion.QuestionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.Answers.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.Answers[i];
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void CheckAnswer(int selectedAnswerIndex)
    {
        Debug.Log($"Respuesta seleccionada: {selectedAnswerIndex}");
        if (selectedAnswerIndex == currentQuestions[currentQuestionIndex].CorrectAnswerIndex)
        {
            if (currentQuestionIndex < currentQuestions.Count - 1)
            {
                currentQuestionIndex++;
                ShowNextQuestion();
            }
            else
            {
                ShowBirdInfo();
            }
        }
        else
        {
            //Debug.Log("Respuesta incorrecta");
            // Mostrar mensaje de respuesta incorrecta
        }
    }

    private void ShowBirdInfo()
    {
        SetModelChecked(currentTriviaId);

        if (data == null)
        {
            Debug.LogError("Referencia a Data no asignada.");
            return;
        }

        BirdInfo currentBirdInfo = data.GetBirdInfo(currentTriviaId);

        if (currentBirdInfo != null)
        {
            birdInfoCanvas.UpdateBirdInfo(
                currentTriviaId,
                currentBirdInfo.BirdName,
                currentBirdInfo.Species,
                currentBirdInfo.Description,
                currentBirdInfo.MainImage,
                currentBirdInfo.Habitat,
                currentBirdInfo.Diet,
                currentBirdInfo.Reproduction,
                currentBirdInfo.Size,
                currentBirdInfo.FunFact1,
                currentBirdInfo.FunFact2,
                currentBirdInfo.SecondaryImage,
                currentBirdInfo.Location,
                currentBirdInfo.Bibliography
            );

            triviaCanvas.SetActive(false);

            birdInfoCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError($"No se encontró información para el modelIndex: {currentTriviaId}");
        }
    }

    private void OnTriviaAnswered(int triviaId)
    {
        var birtType = EventController.Instance.GetCurrentBirdType();
        var checkedAllModels = CheckAllModels();
        //Debug.Log($"Modelo ID: {birtType} Trivia respondida: {triviaId}");

        if (checkedAllModels)
        {
            //Debug.Log($"Todos los modelos con el ID {birtType} han sido recolectados.");
            EventController.Instance.SetTriviaCompleted(birtType);
        }
    }

    private void OnDestroy()
    {
        EventController.OnTriviaAnswered -= OnTriviaAnswered;
    }
}