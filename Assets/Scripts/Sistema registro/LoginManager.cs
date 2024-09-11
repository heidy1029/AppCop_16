using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Asegúrate de usar el namespace de TextMeshPro
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameField; // Cambia a TMP_InputField
    public TMP_InputField passwordField; // Cambia a TMP_InputField
    public Button loginButton;
    public Button registerButton;

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        registerButton.onClick.AddListener(() => SceneManager.LoadScene("RegisterScene"));
    }

    void OnLoginButtonClicked()
    {
        // Aquí va la lógica de autenticación
        string username = usernameField.text;
        string password = passwordField.text;

        if (username == "test" && password == "1234") // Ejemplo simple de autenticación
        {
            Debug.Log("Inicio de sesión exitoso");
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.Log("Usuario o contraseña incorrectos");
        }
    }
}
