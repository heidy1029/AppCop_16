// Assets/Scripts/Juego/CollectionBook.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBook : MonoBehaviour
{
    public Image[] collectionImages; // Array de imágenes en el libro de colección

    /// <summary>
    /// Agrega una imagen a la colección.
    /// </summary>
    /// <param name="image">Sprite de la imagen a agregar.</param>
    public void AddImageToCollection(Sprite image)
    {
        foreach (Image img in collectionImages)
        {
            if (img.sprite == null)
            {
                img.sprite = image;
                img.gameObject.SetActive(true);
                return;
            }
        }

        Debug.LogWarning("La colección de imágenes está completa.");
    }
}
