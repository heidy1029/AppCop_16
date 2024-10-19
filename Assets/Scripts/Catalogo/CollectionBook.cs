using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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

    public void AddImageToCollection(string imagePath, int cardIndex, int currentLevel)
    {
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
    }

    public void ToggleCollectionBook()
    {
        isCatalogVisible = !isCatalogVisible;
        collectionBookPanel.SetActive(isCatalogVisible);
    }

    public void InitializeCollectionBook()
    {
        // Deserializar la colección de tarjetas desde JSON
        string json = PlayerPrefs.GetString("CollectionBook", "[]");
        cards = JsonConvert.DeserializeObject<List<Card>>(json);

        UpdateCollectionImages();
        // Actualizar la vista
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
        for (int i = 0; i < collectionImages.Length; i++)
        {
            if (i < cards.Count)
            {
                Sprite sprite = Resources.Load<Sprite>(cards[i].imagePath);
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
}

[Serializable]
public class Card
{
    public string imagePath;
    public int cardIndex;
    public int currentLevel;
}
