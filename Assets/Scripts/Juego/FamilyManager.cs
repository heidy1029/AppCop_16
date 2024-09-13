using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FamilyManager : MonoBehaviour
{
    public GameObject completeCanvas;
    public FirstPersonController playerController;
    public CameraController cameraController;

    public int totalModelsToCollect = 4;
    private int collectedModels = 0;
    private bool levelCompleted = false;
    public Button level2Button;

    void Start()
    {
        // Cargar progreso del jugador
        int level2Unlocked = PlayerPrefs.GetInt("Level2Unlocked", 0);
        level2Button.interactable = (level2Unlocked == 1); // Desactivar el botón del nivel 2 si no está desbloqueado

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

        // Desbloquear el nivel 2
        PlayerPrefs.SetInt("Level2Unlocked", 1);
        level2Button.interactable = true; // Habilitar el botón del nivel 2
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
