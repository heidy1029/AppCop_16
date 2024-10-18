using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCanvasInfo : MonoBehaviour
{
    public GameObject InfoCanvas;
    public GameObject InfoAves;  // El canvas de la trivia que estará inicialmente desactivado
    public void ActivarCanvas()
    {
        InfoCanvas.SetActive(true);  // Activar el Canvas

    }
    public void CerrarCanvas()
    {
        InfoCanvas.SetActive(false);  // Desactivar el Canvas
    }
    public void ActivarCanvasAves()
    {
        InfoAves.SetActive(true);  // Activar el Canvas

    }
    public void CerrarCanvasAves()
    {
        InfoAves.SetActive(false);  // Desactivar el Canvas
    }
}
