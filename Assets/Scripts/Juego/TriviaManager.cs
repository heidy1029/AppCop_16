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
    public Example example; // Asigna en el Inspector o se buscará automáticamente
    public GameObject triviaCanvas; // Asigna en el Inspector
    public TextMeshProUGUI questionText; // Asigna en el Inspector
    public Button[] answerButtons; // Asigna en el Inspector

    private int modelIndex; // Índice del modelo actual
    private List<Question> currentQuestions;

    private int _currentModelId;
    private int currentQuestionIndex = 0;

    private class Model
    {
        public int modelIndex;
        public bool isChecked;
    }

    private Hashtable _models = new Hashtable();

    //Agregar un modelo si no existe al diccionario en la key modelId
    public void AddModel(int modelId, int modelIndex)
    {
        if(!_models.ContainsKey(modelId))
        {
            Model model = new Model();
            model.modelIndex = modelIndex;
            model.isChecked = false;
            _models.Add(modelId, model);
        }
    }

    //Comprobar si todos los modelos del modelId ya fueron recolectados
    public bool CheckAllModels(int modelId)
    {
        foreach(DictionaryEntry model in _models)
        {
            if((int)model.Key == modelId)
            {
                if(!((Model)model.Value).isChecked)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void Start()
    {
        EventController.OnTriviaAnswered += OnTriviaAnswered;

        if (example == null)
        {
            example = FindObjectOfType<Example>();
            if (example == null)
            {
                Debug.LogError("No se encontró el script Example en la escena.");
            }
        }

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
    public void LoadQuestions(int modelId, int modelIndex)
    {
        _currentModelId = modelId;

        if (example.allModelQuestions == null || example.allModelQuestions.Count == 0)
        {
            Debug.LogError("La lista allModelQuestions en Example está vacía.");
            return;
        }

        EventController.Instance.SetTriviaStarted(modelIndex);

        ModelQuestions modelQuestions = example.allModelQuestions.Find(m => m.modelIndex == modelIndex);

        if (modelQuestions != null && modelQuestions.questions.Count > 0)
        {
            Debug.Log($"Preguntas cargadas correctamente para el modelIndex: {modelIndex}");
            currentQuestions = modelQuestions.questions;
            currentQuestionIndex = 0;

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
            ShowBirdInfo(modelIndex);
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

        if (currentQuestionIndex >= currentQuestions.Count)
        {
            Debug.LogWarning("Índice de pregunta fuera de rango.");
            EndTrivia();
            return;
        }

        Question currentQuestion = currentQuestions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];

                // Capturar el índice correctamente usando una variable temporal
                int answerIndex = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() =>
                {
                    Debug.Log($"Botón {answerIndex} presionado");
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
        Debug.Log($"Revisando respuesta: {selectedAnswerIndex}");

        if (currentQuestions == null || currentQuestions.Count == 0)
        {
            Debug.LogError("No hay preguntas cargadas para verificar la respuesta.");
            return;
        }

        if (currentQuestionIndex >= currentQuestions.Count)
        {
            Debug.LogError("Índice de pregunta actual fuera de rango.");
            return;
        }

        if (selectedAnswerIndex == currentQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Respuesta correcta");
            currentQuestionIndex++;

            if (currentQuestionIndex < currentQuestions.Count)
            {
                ShowNextQuestion();
            }
            else
            {
                EndTrivia(); // Finalizar la trivia
                Debug.Log("Trivia completada");
            }
        }
        else
        {
            Debug.Log("Respuesta incorrecta");
            // Opcional: Manejar lógica de respuesta incorrecta (ej. mostrar mensaje, restar puntos, etc.)
        }
    }

    /// <summary>
    /// Finaliza la trivia y muestra la información del ave correspondiente.
    /// </summary>
    public void EndTrivia()
    {
        Debug.Log("Trivia completada");

        // Llama el evento donde se este escuchando, ver ejemplo en LevelManager.cs
        //EventController.Instance.SetTriviaCompleted(modelIndex);

        ShowBirdInfo(modelIndex);
    }

    /// <summary>
    /// Muestra la información del ave en el canvas correspondiente.
    /// </summary>
    /// <param name="modelIndex">Índice del modelo del ave.</param>
    private void ShowBirdInfo(int modelIndex)
    {
        if (example == null)
        {
            Debug.LogError("Referencia a Example no asignada.");
            return;
        }

        BirdInfo currentBirdInfo = example.GetBirdInfo(modelIndex);

        if (currentBirdInfo != null)
        {
            // Actualizar la información en el canvas
            birdInfoCanvas.UpdateBirdInfo(
                modelIndex,
                currentBirdInfo.birdName,
                currentBirdInfo.species,
                currentBirdInfo.description,
                currentBirdInfo.mainImage,
                currentBirdInfo.habitat,
                currentBirdInfo.diet,
                currentBirdInfo.reproduction,
                currentBirdInfo.size,
                currentBirdInfo.funFact1,
                currentBirdInfo.funFact2,
                currentBirdInfo.secondaryImage,
                currentBirdInfo.location,
                currentBirdInfo.bibliography
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
            Debug.LogError($"No se encontró información para el modelIndex: {modelIndex}");
        }
    }

    private void OnTriviaAnswered(int triviaId)
    {
        var checkedAllModels = CheckAllModels(triviaId);
        Debug.Log($"Modelo ID: {_currentModelId} Trivia respondida: {triviaId}");

        if (checkedAllModels)
        {
            Debug.Log($"Todos los modelos con el ID {_currentModelId} han sido recolectados.");
            EventController.Instance.SetTriviaCompleted(_currentModelId);
        }
    }

    private void OnDestroy()
    {
        EventController.OnTriviaAnswered -= OnTriviaAnswered;
    }
}
