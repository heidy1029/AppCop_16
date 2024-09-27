using UnityEngine;

public class ModelTrigger : MonoBehaviour
{
    [SerializeField] private int _birdType;
    [SerializeField] private int _modelIndex;
    public Sprite modelImage; // Imagen del modelo para el libro

    private void Start()
    {
        TriviaManager.Instance.AddModel(_birdType, _modelIndex); // Agrega el modelo al diccionario
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject); // Destruye el trigger
            TriviaManager.Instance.LoadQuestions(_modelIndex); // Inicia la trivia con el modelIndex correcto
        }
    }

    public void OnTriviaCompleted()
    {
        CollectionBook collectionBook = FindObjectOfType<CollectionBook>();
        if (collectionBook != null && modelImage != null)
        {
            collectionBook.AddImageToCollection(modelImage);
        }
    }
}