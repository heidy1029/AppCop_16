using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private int[] _buttonIds;

    private void Start()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int buttonId = _buttonIds[i];
            _buttons[i].onClick.AddListener(() => OnButtonPressed(buttonId));
        }
    }

    private void OnButtonPressed(int buttonId)
    {
        EventController.Instance.SetTriviaStarted(buttonId);

        SceneManager.LoadScene("Nivel 1");
    }
}
