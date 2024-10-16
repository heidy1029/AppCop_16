using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvas1;  // Asigna tu primer canvas desde el Inspector
    public GameObject canvas2;  // Asigna tu segundo canvas desde el Inspector
    public Button buttonShowCanvas1; // Botón para mostrar el Canvas1
    public Button buttonShowCanvas2; // Botón para mostrar el Canvas2

    void Start()
    {
        // Asigna los métodos a los botones
        buttonShowCanvas1.onClick.AddListener(ShowCanvas1);
        buttonShowCanvas2.onClick.AddListener(ShowCanvas2);
    }

    // Mostrar Canvas1 y ocultar Canvas2
    public void ShowCanvas1()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }

    // Mostrar Canvas2 y ocultar Canvas1
    public void ShowCanvas2()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }
}
