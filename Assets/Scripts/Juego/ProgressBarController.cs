using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;
    private int _totalModelsCollected = 0;
    private int _totalModels;

    // Método para inicializar el progreso
    public void InitializeProgressBar(int totalModels)
    {
        _totalModels = totalModels;
        _progressBar.maxValue = totalModels;
        _progressBar.value = 0;
    }

    // Método para actualizar el progreso
    public void UpdateProgress()
    {
        _totalModelsCollected++;
        _progressBar.value = _totalModelsCollected;
    }
}
