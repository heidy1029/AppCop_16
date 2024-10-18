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
    public CollectionBook collectionBook;
    public Data data; // Asigna en el Inspector o se buscará automáticamente
    public GameObject triviaCanvas; // Asigna en el Inspector
    public TextMeshProUGUI questionText; // Asigna en el Inspector
    public Button[] answerButtons; // Asigna en el Inspector
    private List<Question> currentQuestions;

    private int currentTriviaId; // Índice del modelo actual
    private int currentQuestionIndex = 0;


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
        }
    }

    // Comprobar si todos los modelos del modelId ya fueron recolectados
    public bool CheckAllModels()
    {
        foreach (var model in _triviaCompleted)
        {
            if (!model.Value)
            {
                return false;
            }
        }
        return true;
    }

    private void Start()
    {
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
            currentQuestions = modelQuestions.Questions;
            currentQuestionIndex = 0;
            currentTriviaId = triviaId; // Aquí se actualiza el currentTriviaId

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

        BirdInfo currentBirdInfo = data.GetBirdInfo(EventController.Instance.GetCurrentBirdType(), currentTriviaId);

        if (currentBirdInfo != null)
        {
            // Actualizar la información del ave en el canvas, incluyendo la ruta del sonido
            birdInfoCanvas.UpdateBirdInfo(
                currentTriviaId,
                currentBirdInfo.BirdName,
                currentBirdInfo.Species,
                currentBirdInfo.Description,
                currentBirdInfo.MainImage, // Imagen principal
                currentBirdInfo.Habitat,
                currentBirdInfo.Diet,
                currentBirdInfo.Reproduction,
                currentBirdInfo.Size,
                currentBirdInfo.FunFact1,
                currentBirdInfo.FunFact2,
                currentBirdInfo.Location,
                currentBirdInfo.Bibliography,
                currentBirdInfo.AutorSonido,
                currentBirdInfo.BirdSound // La ruta del sonido del ave
            );

            birdInfoCanvas.gameObject.SetActive(true);
            triviaCanvas.SetActive(false);

            int cardIndex = currentTriviaId;
            int currentLevel = EventController.Instance.GetCurrentLevel();

            if (!string.IsNullOrEmpty(currentBirdInfo.MainImage))
            {
                collectionBook.AddImageToCollection(currentBirdInfo.MainImage, cardIndex, currentLevel);
            }
            else
            {
                Debug.LogError("La imagen principal de currentBirdInfo es nula o vacía.");
            }
        }
        else
        {
            Debug.LogError($"No se encontró información para el modelIndex: {currentTriviaId} en el birdTypeId {EventController.Instance.GetCurrentBirdType()}");
        }
    }

    /*private void ShowBirdInfo()
    {
        SetModelChecked(currentTriviaId);

        if (data == null)
        {
            Debug.LogError("Referencia a Data no asignada.");
            return;
        }

        BirdInfo currentBirdInfo = data.GetBirdInfo(EventController.Instance.GetCurrentBirdType(), currentTriviaId);

        if (currentBirdInfo != null)
        {
            // Actualizar la información del ave en el canvas
            birdInfoCanvas.UpdateBirdInfo(
                currentTriviaId,
                currentBirdInfo.BirdName,
                currentBirdInfo.Species,
                currentBirdInfo.Description,
                currentBirdInfo.MainImage, // Imagen principal
                currentBirdInfo.Habitat,
                currentBirdInfo.Diet,
                currentBirdInfo.Reproduction,
                currentBirdInfo.Size,
                currentBirdInfo.FunFact1,
                currentBirdInfo.FunFact2,
                currentBirdInfo.Location,
                currentBirdInfo.Bibliography
            );

            birdInfoCanvas.gameObject.SetActive(true);
            triviaCanvas.SetActive(false);

            collectionBook.AddImageToCollection(currentBirdInfo.MainImage);
        }
        else
        {
            Debug.LogError($"No se encontró información para el modelIndex: {currentTriviaId} en el birdTypeId {EventController.Instance.GetCurrentBirdType()}");
        }
    }*/


    public void SetTriviaAnswered(int triviaId)
    {
        var checkedAllModels = CheckAllModels();

        EventController.Instance.SetTriviaCompleted(triviaId, checkedAllModels);
    }
}