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

    private int currentTriviaId; // Índice del modelo actual
    private List<Question> currentQuestions;

    private int currentQuestionIndex = 0;

    private List<bool> _triviaCompleted = new List<bool>();

    // Agregar una nueva key birdType si no existe y agregar en esa key nuevo modelo con el índice del modelo
    public void AddModel(int birdType)
    {
        if (birdType == EventController.Instance.GetCurrentBirdType())
        {
            _triviaCompleted.Add(false);
        }
    }

    public void SetModelChecked(int triviaId)
    {
        _triviaCompleted[triviaId] = true;

        foreach (var trivia in _triviaCompleted)
        {
            Debug.Log(trivia);
        }
    }

    // Comprobar si todos los modelos del modelId ya fueron recolectados
    public bool CheckAllModels()
    {
        var completed = true;
        foreach (var trivia in _triviaCompleted)
        {
            if (!trivia)
            {
                completed = false;
                break;
            }
        }
        return completed;
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
    }

    /// <summary>
    /// Carga las preguntas para un modelo específico.
    /// </summary>
    /// <param name="modelIndex">Índice del modelo.</param>
    public void LoadQuestions(int modelIndex)
    {
        var model = data.GetModelQuestions(EventController.Instance.GetCurrentBirdType());

        if (model == null || model.Count == 0)
        {
            Debug.LogError("La lista allModelQuestions en Data está vacía.");
            return;
        }

        EventController.Instance.SetTriviaStarted(modelIndex);

        ModelQuestion modelQuestions = model.Find(m => m.ModelIndex == modelIndex);

        if (modelQuestions != null && modelQuestions.Questions.Count > 0)
        {
            //Debug.Log($"Preguntas cargadas correctamente para el modelIndex: {modelIndex}");
            currentQuestions = modelQuestions.Questions;
            currentQuestionIndex = 0;
            currentTriviaId = modelIndex;

            // Activar el canvas de trivia
            if (triviaCanvas != null)
            {
                triviaCanvas.SetActive(true);
            }

            // Desactivar el canvas de información del ave por si estaba activo
            if (birdInfoCanvas != null)
            {
                birdInfoCanvas.gameObject.SetActive(false);
            }

            ShowNextQuestion();
        }
        else
        {
            Debug.LogWarning($"No se encontraron preguntas para el modelIndex: {modelIndex}");
            // Opcional: podrías desactivar el canvas de trivia o manejar este caso de otra manera
            if (triviaCanvas != null)
            {
                triviaCanvas.SetActive(false);
            }

            // Opcional: Mostrar información del ave directamente si no hay preguntas
            ShowBirdInfo();
        }
    }

    /// <summary>
    /// Muestra la siguiente pregunta en la trivia.
    /// </summary>
    private void ShowNextQuestion()
    {
        if (currentQuestions == null || currentQuestions.Count == 0)
        {
            Debug.LogError("No hay preguntas para mostrar.");
            EndTrivia();
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

                // Capturar el índice correctamente usando una variable temporal
                int answerIndex = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() =>
                {
                    //Debug.Log($"Botón {answerIndex} presionado");
                    CheckAnswer(answerIndex);
                });
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Verifica si la respuesta seleccionada es correcta.
    /// </summary>
    /// <param name="selectedAnswerIndex">Índice de la respuesta seleccionada.</param>
    private void CheckAnswer(int selectedAnswerIndex)
    {
        // Debug.Log($"Revisando respuesta: {selectedAnswerIndex}");

        if (currentQuestions == null || currentQuestions.Count == 0)
        {
            Debug.LogError("No hay preguntas cargadas para verificar la respuesta.");
            return;
        }

        if (selectedAnswerIndex == currentQuestions[currentQuestionIndex].CorrectAnswerIndex)
        {
            //Debug.Log("Respuesta correcta");
           
            if (currentQuestionIndex < currentQuestions.Count - 1)
            {
                currentQuestionIndex++;
                ShowNextQuestion();
            }
            else
            {
                EndTrivia(); // Finalizar la trivia
                //Debug.Log("Trivia completada");
            }
        }
        else
        {
            //Debug.Log("Respuesta incorrecta");
            // Opcional: Manejar lógica de respuesta incorrecta (ej. mostrar mensaje, restar puntos, etc.)
        }
    }

    /// <summary>
    /// Finaliza la trivia y muestra la información del ave correspondiente.
    /// </summary>
    public void EndTrivia()
    {
        //Debug.Log("Trivia completada");

        // Llama el evento donde se este escuchando, ver ejemplo en LevelManager.cs
        //EventController.Instance.SetTriviaCompleted(modelIndex);

        ShowBirdInfo();
    }

    /// <summary>
    /// Muestra la información del ave en el canvas correspondiente.
    /// </summary>
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
            // Actualizar la información en el canvas
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

            // Desactivar el canvas de trivia
            if (triviaCanvas != null)
            {
                triviaCanvas.SetActive(false);
            }

            // Activar el canvas de información del ave
            if (birdInfoCanvas != null)
            {
                birdInfoCanvas.gameObject.SetActive(true);
            }
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