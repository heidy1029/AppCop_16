using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectionBook : MonoBehaviour
{
    public GameObject collectionPagePrefab; // Prefab para una página de colección
    public Transform collectionContent; // Contenedor para las páginas en el libro
    private List<Sprite> collectedImages = new List<Sprite>();

    public void AddImageToCollection(Sprite modelImage)
    {
        // Verifica si ya se ha añadido la imagen
        if (!collectedImages.Contains(modelImage))
        {
            collectedImages.Add(modelImage);

            // Instancia una nueva página de colección y agrega la imagen
            GameObject newPage = Instantiate(collectionPagePrefab, collectionContent);
            Image imageComponent = newPage.GetComponentInChildren<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = modelImage;
            }
        }
    }
}
