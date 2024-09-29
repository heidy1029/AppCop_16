using UnityEngine;
using UnityEngine.UI;

public class OpenCatalogButton : MonoBehaviour
{
    public Button openCatalogButton;
    public CollectionBook collectionBook;

    private void Start()
    {
        openCatalogButton.onClick.AddListener(OpenCatalog);
    }

    private void OpenCatalog()
    {
        if (collectionBook != null)
        {
            collectionBook.ShowCollectionBook();
        }
        else
        {
            Debug.LogError("No se ha asignado el CollectionBook.");
        }
    }
    public void CloseCatalog()
    {
        collectionBook.HideCollectionBook();
    }
}
