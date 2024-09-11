using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button catalogButton;
    public Button placeMammalsButton;

    void Start()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        catalogButton.onClick.AddListener(() => SceneManager.LoadScene("CatalogScene"));
        placeMammalsButton.onClick.AddListener(() => SceneManager.LoadScene("PlacementScene"));
    }
}

