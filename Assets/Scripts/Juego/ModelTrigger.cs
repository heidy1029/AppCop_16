using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTrigger : MonoBehaviour
{
    public int modelIndex;
    public Sprite modelImage; // Imagen del modelo para el libro
    private TriviaManager triviaManager;

    private void Start()
    {
        triviaManager = FindObjectOfType<TriviaManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triviaManager.LoadQuestions(modelIndex); // Inicia la trivia con el modelIndex correcto
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