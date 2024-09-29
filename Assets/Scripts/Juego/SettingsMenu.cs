using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenuUI;
    public Slider musicSlider; // Slider de música
    public Slider soundSlider; // Slider de efectos de sonido
    public AudioSource musicSource;
    // public AudioSource soundSource;

    private bool isPaused = false;

    void Start()
    {

        musicSlider.value = musicSource.volume;
        //soundSlider.value = soundSource.volume;

        // Asigna funciones a los sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        // soundSlider.onValueChanged.AddListener(SetSoundVolume);

        settingsMenuUI.SetActive(false);
    }

    void Update()
    {
        // Abre el menú de configuraciones al presionar Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pausa el juego y muestra el menú
    public void PauseGame()
    {
        settingsMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void ResumeGame()
    {
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Función para ajustar el volumen de la música
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    /*public void SetSoundVolume(float volume)
    {
        soundSource.volume = volume;
    }*/


    public void CloseSettingsMenu()
    {
        ResumeGame();
    }
}
