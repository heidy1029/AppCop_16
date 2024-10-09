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
    public Image[] collectionImages; // Array de imágenes en el libro de colección
    public GameObject collectionBookPanel; // El panel del catálogo de colección
    private bool isCatalogVisible = false; // Controla si el catálogo está visible o no

    void Start()
    {
        Debug.Log("Inicializando el libro de colección...");
        InitializeCollectionBook();
    }

    void Update()
    {
        // Detecta si se presiona la tecla F para alternar el catálogo
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleCollectionBook();
        }
    }

    // Método para agregar imágenes a la colección
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
                img.sprite = sprite; // Asigna el sprite
                img.gameObject.SetActive(true); // Activa la imagen solo cuando se coleccione
                Debug.Log("Imagen agregada al catálogo de colección.");
                return;
            }
        }

        Debug.LogWarning("La colección de imágenes está completa.");
    }


    // Alterna la visibilidad del catálogo
    public void ToggleCollectionBook()
    {
        isCatalogVisible = !isCatalogVisible;

        if(!LevelManager.Instance.IsMobile)
        {
            if(isCatalogVisible)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        collectionBookPanel.SetActive(isCatalogVisible);
    }

    // Oculta las imágenes no coleccionadas al inicio
    public void InitializeCollectionBook()
    {
        foreach (Image img in collectionImages)
        {
            // Oculta todas las imágenes al iniciar el juego
            img.gameObject.SetActive(false);
            Debug.Log("Imagen oculta al iniciar.");
        }
    }

}*/
