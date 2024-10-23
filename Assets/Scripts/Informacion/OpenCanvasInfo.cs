using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCanvasInfo : MonoBehaviour
{
    public GameObject FaunaDagma;
    public GameObject InfoAves; 
    public GameObject SabiasQue;
    public GameObject Trafico1; 
    public GameObject Trafico2; 
    public void ActivarCanvasFauna()
    {
        FaunaDagma.SetActive(true);  // Activar el Canvas

    }
    public void CerrarCanvasFauna()
    {
        FaunaDagma.SetActive(false);  // Desactivar el Canvas
    }
    public void ActivarCanvasAves()
    {
        InfoAves.SetActive(true);  // Activar el Canvas

    }
    public void CerrarCanvasAves()
    {
        InfoAves.SetActive(false);  // Desactivar el Canvas
    }
     public void ActivarCanvasSabiasQue()
    {
        SabiasQue.SetActive(true);  // Activar el Canvas

    }
    public void CerrarCanvasSabiasQue()
    {
        SabiasQue.SetActive(false);  // Desactivar el Canvas
    }
    public void ActivarCanvasTrafico1()
    {
        Trafico1.SetActive(true);  // Activar el Canvas

    }
    public void CerrarCanvasTrafico1()
    {
        Trafico1.SetActive(false);  // Desactivar el Canvas
    }
    public void ActivarCanvasTrafico2()
    {
        Trafico2.SetActive(true);  // Activar el Canvas

    }
    public void CerrarCanvasTrafico2()
    {
        Trafico2.SetActive(false);  // Desactivar el Canvas
    }
}
