using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button registerButton;

    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    void OnRegisterButtonClicked()
    {
        // Lógica de registro
        // Si el registro es exitoso, cargar la escena de inicio de sesión
        SceneManager.LoadScene("LoginScene");
    }
}
