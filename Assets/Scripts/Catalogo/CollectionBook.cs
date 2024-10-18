using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBook : MonoBehaviour
{
    public Image[] collectionImages;
    public GameObject collectionBookPanel;
    private bool isCatalogVisible = false;

    // Referencia al script de paginación
    public Rotacion paginacionScript;

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

        foreach (Image img in collectionImages)
        {
            if (img.sprite == null)
            {
                img.sprite = sprite;
                img.gameObject.SetActive(true);
                Debug.Log("Imagen agregada al catálogo de colección.");
                // Actualizar vista
                paginacionScript.ActualizarCartas();
                return;
            }
        }

        Debug.LogWarning("La colección de imágenes está completa.");
    }

    public void ToggleCollectionBook()
    {
        isCatalogVisible = !isCatalogVisible;
        collectionBookPanel.SetActive(isCatalogVisible);
    }

    public void InitializeCollectionBook()
    {
        foreach (Image img in collectionImages)
        {
            img.gameObject.SetActive(false);
        }
    }
}


/*public class CollectionBook : MonoBehaviour
{
    public Image[] collectionImages;
    public GameObject collectionBookPanel;
    private bool isCatalogVisible = false;
    public GameProgress gameProgress;

    // Referencia al script de paginación
    public Rotacion paginacionScript;

    void Start()
    {
        gameProgress = FindObjectOfType<GameProgress>();

        // Verifica si se encontró el script
        if (gameProgress != null)
        {
            // Ejemplo de guardar progreso al iniciar
            gameProgress.SaveLevelProgress(1, false);
        }
        else
        {
            Debug.LogError("GameProgress no encontrado en la escena.");
        }
        InitializeCollectionBook();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleCollectionBook();
        }
    }

    public void AddImageToCollection(string imagePath)
    {
        Sprite sprite = Resources.Load<Sprite>(imagePath);

        if (sprite == null)
        {
            Debug.LogError("No se pudo cargar la imagen desde la ruta: " + imagePath);
            return;
        }

        foreach (Image img in collectionImages)
        {
            if (img.sprite == null)
            {
                img.sprite = sprite;
                img.gameObject.SetActive(true);
                Debug.Log("Imagen agregada al catálogo de colección.");

                // Llamar al script de paginación para actualizar la vista
                paginacionScript.ActualizarCartas();
                gameProgress.SaveCollectedCard(cardId);
                return;
            }
        }

        Debug.LogWarning("La colección de imágenes está completa.");
    }

    public void ToggleCollectionBook()
    {
        isCatalogVisible = !isCatalogVisible;
        collectionBookPanel.SetActive(isCatalogVisible);
    }

    public void InitializeCollectionBook()
    {
        foreach (Image img in collectionImages)
        {
            img.gameObject.SetActive(false);
        }
    }
}
*/

