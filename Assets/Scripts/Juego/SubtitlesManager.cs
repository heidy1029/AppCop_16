using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitlesManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public Canvas subtitlesCanvas;
    private Coroutine subtitleCoroutine;

    void Start()
    {

        subtitlesCanvas.gameObject.SetActive(true);
        StartTutorial();
    }

    void StartTutorial()
    {

        ShowSubtitle("Bienvenido al tutorial de Biotour", 3);
        StartCoroutine(ShowSubtitleSequence());
    }

    IEnumerator ShowSubtitleSequence()
    {
        yield return new WaitForSeconds(5);
        ShowSubtitle("Muevete por el escenario con el joystick izaquierdo", 5);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Gira la camara con el joystick derecho", 4);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Salta dandole click en el boton A", 4);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Encuentra todas las cartas Esparcidas en el mundo", 4);

        yield return new WaitForSeconds(5);
        ShowSubtitle("Contesta las trivias  ", 4);

        yield return new WaitForSeconds(4);
        ShowSubtitle("¡Buena suerte!", 3);

        yield return new WaitForSeconds(3);


        subtitlesCanvas.gameObject.SetActive(false);
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
