// Assets/Scripts/Juego/CollectionBook.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBook : MonoBehaviour
{
    public Image[] collectionImages; // Array de imágenes en el libro de colección

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
}
