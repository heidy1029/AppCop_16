using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private int[] _birdTypes;

    private void Start()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int currentLevel = _birdTypes[i];
            _buttons[i].onClick.AddListener(() => OnButtonPressed(currentLevel));
        }

        StartCoroutine(LoadCurrentLevel());
    }

    private IEnumerator LoadCurrentLevel()
    {
        DataController.Instance.GetCurrentLevelOnStart();

        while (!DataController.Instance.IsInitialized)
        {
            yield return null;
        }

        int currentLevelFromData = DataController.Instance.GetCurrentLevel();
        Debug.Log("Current level: " + currentLevelFromData);
        for (int i = 0; i < _buttons.Length; i++)
        {
            int currentLevel = _birdTypes[i];

            // Si el botón está bloqueado, activarlo
            if (currentLevel <= currentLevelFromData)
            {
                _buttons[i].interactable = true;
            }
        }
    }

    private void OnButtonPressed(int birdType)
    {
        DataController.Instance.SaveCurrentLevel(birdType, false);
        EventController.Instance.SetTriviaStarted(birdType);

        SceneManager.LoadScene("GameLevel");
    }
}
