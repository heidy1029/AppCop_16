using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBook : MonoBehaviour
{
    public Image[] collectionImages; // Imágenes del catálogo
    public GameObject collectionBookPanel; // El panel o canvas del catálogo

    // Método para agregar imágenes a la colección
    public void AddImageToCollection(Sprite image)
    {
        foreach (Image img in collectionImages)
        {
            if (img.sprite == null)
            {
                img.sprite = image;
                img.gameObject.SetActive(true);
                Debug.Log("Imagen agregada al catálogo de colección.");
                return;
            }
        }
        Debug.LogWarning("La colección de imágenes está completa.");
    }

    // Método para mostrar el catálogo
    public void ShowCollectionBook()
    {
        if (collectionBookPanel != null)
        {
            collectionBookPanel.SetActive(true); // Muestra el panel del catálogo
        }
        else
        {
            Debug.LogError("No se ha asignado el panel del catálogo.");
        }
    }

    // Método para ocultar el catálogo
    public void HideCollectionBook()
    {
        if (collectionBookPanel != null)
        {
            collectionBookPanel.SetActive(false); // Oculta el panel del catálogo
        }
    }
}
