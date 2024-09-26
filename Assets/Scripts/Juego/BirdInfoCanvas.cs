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

    /// <summary>
    /// Actualiza la información del ave en el canvas.
    /// </summary>
    public void UpdateBirdInfo(
        int modelIndex,
        string birdName,
        string species,
        string description,
        string mainImagePath,
        string habitat,
        string diet,
        string reproduction,
        string size,
        string funFact1,
        string funFact2,
        string secondaryImagePath,
        string location,
        string bibliography)
    {
        currentTriviaId = modelIndex;
        birdNameText.text = birdName;
        speciesText.text = species;
        descriptionText.text = description;
        habitatText.text = habitat;
        dietText.text = diet;
        reproductionText.text = reproduction;
        sizeText.text = size;
        funFact1Text.text = funFact1;
        funFact2Text.text = funFact2;
        locationText.text = location;
        bibliographyText.text = bibliography;

        // Cargar y asignar los sprites desde la carpeta de recursos
        Sprite mainImageSprite = Resources.Load<Sprite>(mainImagePath);
        Sprite secondaryImageSprite = Resources.Load<Sprite>(secondaryImagePath);

        if (mainImageSprite != null)
        {
            mainImage.sprite = mainImageSprite;
        }
        else
        {
            Debug.LogWarning($"No se pudo cargar la imagen principal desde la ruta: {mainImagePath}");
        }

        if (secondaryImageSprite != null)
        {
            secondaryImage.sprite = secondaryImageSprite;
        }
        else
        {
            Debug.LogWarning($"No se pudo cargar la imagen secundaria desde la ruta: {secondaryImagePath}");
        }

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
        EventController.Instance.SetTriviaAnswered(currentTriviaId);
    }
}
