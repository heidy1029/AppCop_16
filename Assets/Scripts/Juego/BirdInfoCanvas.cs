// Assets/Scripts/Juego/BirdInfoCanvas.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro

public class BirdInfoCanvas : MonoBehaviour
{
    public GameObject canvas;
    public Button buttonClose;
    public TextMeshProUGUI birdNameText;
    public TextMeshProUGUI speciesText;
    public TextMeshProUGUI descriptionText;
    public Image mainImage;
    public TextMeshProUGUI habitatText;
    public TextMeshProUGUI dietText;
    public TextMeshProUGUI reproductionText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI funFact1Text;
    public TextMeshProUGUI funFact2Text;
    public Image secondaryImage;
    public TextMeshProUGUI locationText;
    public TextMeshProUGUI bibliographyText;

    private int currentTriviaId;

    private void Start()
    {
        buttonClose.onClick.AddListener(OnBackButtonPressed);
    }

    public void UpdateBirdInfo(int triviaId, string birdName, string species, string description, Sprite mainImageSprite, string habitat, string diet, string reproduction, string size, string funFact1, string funFact2, Sprite secondaryImageSprite, string location, string bibliography)
    {
        currentTriviaId = triviaId;
        birdNameText.text = birdName;
        speciesText.text = species;
        descriptionText.text = description;
        mainImage.sprite = mainImageSprite;
        habitatText.text = habitat;
        dietText.text = diet;
        reproductionText.text = reproduction;
        sizeText.text = size;
        funFact1Text.text = funFact1;
        funFact2Text.text = funFact2;
        secondaryImage.sprite = secondaryImageSprite;
        locationText.text = location;
        bibliographyText.text = bibliography;

        ShowCanvas(true);
    }

    public void ShowCanvas(bool isActive)
    {
        canvas.SetActive(isActive);
    }

    /// <summary>
    /// Método para manejar el botón de "Volver" o similar.
    /// </summary>
    public void OnBackButtonPressed()
    {
        ShowCanvas(false);
        // Llama el evento donde se este escuchando, ver ejemplo en LevelManager.cs
        EventController.Instance.SetTriviaCompleted(currentTriviaId);

        // Activar el canvas de trivia si es necesario
        // Puedes hacerlo referenciando directamente al TriviaManager o usando otro método
        TriviaManager triviaManager = FindObjectOfType<TriviaManager>();
        /*if (triviaManager != null && triviaManager.triviaCanvas != null)
        {
            triviaManager.triviaCanvas.SetActive(true);
        }*/
    }
}
