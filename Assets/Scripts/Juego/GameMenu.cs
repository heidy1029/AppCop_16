using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private int[] _birdTypes;

    private void Start()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int birdType = _birdTypes[i];
            _buttons[i].onClick.AddListener(() => OnButtonPressed(birdType));

            if (birdType <= EventController.Instance.GetCurrentBirdType())
            {
                _buttons[i].interactable = true;
            }
        }
    }

    private void OnButtonPressed(int birdType)
    {
        EventController.Instance.SetTriviaStarted(birdType);

        SceneManager.LoadScene("Nivel 1");
    }
}
