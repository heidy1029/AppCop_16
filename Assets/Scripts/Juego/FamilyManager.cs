using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class FamilyManager : MonoBehaviour
{
    public GameObject levelMenuCanvas;
    public GameObject completeCanvas;
    public CardSpawner cardSpawner;
    public int totalModelsToCollect = 4;
    private int collectedModels = 0;

    private bool levelCompleted = false;
    public Button level1Button;

    void Start()
    {
        level1Button.onClick.AddListener(() => StartLevel1());
        Debug.Log("boton click");
        completeCanvas.SetActive(false);
    }

    public void StartLevel1()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Método StartLevel1 llamado");
        levelMenuCanvas.SetActive(false);
        cardSpawner.ActivateSpawning();
    }

    // Llamar a este método cuando se recolecte un modelo
    public void CollectModel()
    {
        collectedModels++;
        Debug.Log("Modelo recolectado: " + collectedModels);


        if (collectedModels >= totalModelsToCollect && !levelCompleted)
        {
            LevelCompleted();
        }
    }

    // Método para manejar la finalización del nivel
    private void LevelCompleted()
    {
        Debug.Log("¡Nivel completado!");
        levelCompleted = true;
        completeCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectModel();
            gameObject.SetActive(false);
        }
    }


}
