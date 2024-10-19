using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class CollectionBook : MonoBehaviour
{
    public Image[] collectionImages;
    public GameObject collectionBookPanel;
    private bool isCatalogVisible = false;

    // Referencia al script de paginación
    public Rotacion paginacionScript;

    // Lista de tarjetas en la colección
    private List<Card> cards = new List<Card>();

    void Start()
    {
        InitializeCollectionBook();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleCollectionBook();
        }
    }

    public async void AddImageToCollection(string imagePath, int cardIndex, int currentLevel)
    {
        if (string.IsNullOrEmpty(imagePath) || cardIndex < 0 || currentLevel < 0)
        {
            Debug.LogError("Invalid card data. Cannot add to collection.");
            return;
        }

        Sprite sprite = Resources.Load<Sprite>(imagePath);

        if (sprite == null)
        {
            Debug.LogError("No se pudo cargar la imagen desde la ruta: " + imagePath);
            return;
        }

        Card newCard = new Card
        {
            imagePath = imagePath,
            cardIndex = cardIndex,
            currentLevel = currentLevel
        };

        cards.Add(newCard);
        UpdateCollectionImages();
        Debug.Log("Imagen agregada al catálogo de colección.");
        // Actualizar vista
        paginacionScript.ActualizarCartas();

        // Crear o actualizar cartas en la base de datos
        await CreateOrUpdateCards();
    }

    public void ToggleCollectionBook()
    {
        isCatalogVisible = !isCatalogVisible;
        collectionBookPanel.SetActive(isCatalogVisible);
    }

    public async void InitializeCollectionBook()
    {
        await GetCards();

        UpdateCollectionImages();
        paginacionScript.ActualizarCartas();
    }

    public void SaveCollectionBook()
    {
        // Serializar la colección de tarjetas a JSON
        string json = JsonConvert.SerializeObject(cards);
        PlayerPrefs.SetString("CollectionBook", json);
        PlayerPrefs.Save();
    }

    public void UpdateCollectionBook(List<Card> updatedCards)
    {
        cards = updatedCards;
        SaveCollectionBook();
        UpdateCollectionImages();
        paginacionScript.ActualizarCartas();
    }

    public List<Card> GetCollection()
    {
        return cards;
    }

    private void UpdateCollectionImages()
    {
        int currentLevel = DataController.Instance.GetCurrentLevel();

        for (int i = 0; i < collectionImages.Length; i++)
        {
            // Filtrar las cartas por el nivel actual y el índice de la carta
            Card card = cards.Find(c => c.currentLevel == currentLevel && c.cardIndex == i);

            if (card != null)
            {
                Sprite sprite = Resources.Load<Sprite>(card.imagePath);
                collectionImages[i].sprite = sprite;
                collectionImages[i].gameObject.SetActive(true);
            }
            else
            {
                collectionImages[i].sprite = null;
                collectionImages[i].gameObject.SetActive(false);
            }
        }
    }

    private async Task CreateOrUpdateCards()
    {
        await DataController.Instance.CreateOrUpdateCards(cards);
    }

    public async Task GetCards()
    {
        List<Card> fetchedCards = await DataController.Instance.GetCards();
        if (fetchedCards != null && fetchedCards.Count > 0)
        {
            Debug.Log("Cards loaded from database.");
            cards = fetchedCards;
        }
        else
        {
            Debug.LogWarning("No se encontraron cartas en la base de datos.");
        }
    }
}

[Serializable]
public class Card
{
    public string imagePath;
    public int cardIndex;
    public int currentLevel;
}
