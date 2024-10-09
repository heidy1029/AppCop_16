using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitlesManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public Canvas subtitlesCanvas;
    private Coroutine subtitleCoroutine;
    private static bool tutorialCompleted = false; // Variable para saber si ya se completó el tutorial

    void Start()
    {
        if (!tutorialCompleted) // Si el tutorial no se ha completado, lo muestra
        {
            subtitlesCanvas.gameObject.SetActive(true);
            StartTutorial();
        }
        else
        {
            subtitlesCanvas.gameObject.SetActive(false); // Si ya se completó, no muestra los subtítulos
        }
    }

    void StartTutorial()
    {
        ShowSubtitle("Bienvenido al tutorial de Biotour", 3);
        StartCoroutine(ShowSubtitleSequence());
    }

    IEnumerator ShowSubtitleSequence()
    {
        yield return new WaitForSeconds(5);
        ShowSubtitle("Muevete por el escenario con el joystick izquierdo", 5);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Gira la cámara con el joystick derecho", 4);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Salta dando clic en el botón A", 4);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Encuentra todas las cartas esparcidas en el mundo", 4);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Contesta las trivias", 4);

        yield return new WaitForSeconds(4);
        ShowSubtitle("¡Buena suerte!", 3);

        yield return new WaitForSeconds(3);

        subtitlesCanvas.gameObject.SetActive(false);
        tutorialCompleted = true; // Marca que el tutorial ha sido completado
    }

    public void ShowSubtitle(string text, float duration)
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }
        subtitleCoroutine = StartCoroutine(ShowSubtitleForDuration(text, duration));
    }

    IEnumerator ShowSubtitleForDuration(string text, float duration)
    {
        subtitleText.text = text;
        yield return new WaitForSeconds(duration);
        subtitleText.text = "";
    }
}
