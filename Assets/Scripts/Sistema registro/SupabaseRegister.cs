using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Necesario para usar TMP_InputField
using UnityEngine.UI; // Necesario para usar Button
using SimpleJSON;
using UnityEngine.SceneManagement;



public class SupabaseRegister : MonoBehaviour
{
    private string apiUrl = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/signup";
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    // Referencias a los campos de entrada de correo, contraseña y nombre
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField nameInput;

    // Referencia al texto para mostrar la respuesta
    public TextMeshProUGUI responseText;

    // Referencia al botón de registro
    public Button registerButton;

    void Start()
    {
        // Asignamos la función OnRegisterButtonClicked al botón de registro
        registerButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    void OnRegisterButtonClicked()
    {
        // Al hacer clic en el botón, iniciamos la coroutine para enviar los datos
        StartCoroutine(EnviarDatos());
    }

    IEnumerator EnviarDatos()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string name = nameInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
        {
            responseText.text = "Por favor, llena todos los campos.";
            yield break;
        }

        string jsonData = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"name\":\"{name}\"}}";

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            responseText.text = "Error en la petición: " + request.error;
            Debug.LogError("Error en la petición: " + request.error);
        }
        else
        {
            // Parsear la respuesta JSON para obtener la información del usuario
            string serverResponse = request.downloadHandler.text;
            Debug.Log("Respuesta del servidor: " + serverResponse);
            SceneManager.LoadScene("LoginScene");

            // Convertir la respuesta JSON a un objeto dinámico para acceder a los campos
            /* var json = SimpleJSON.JSON.Parse(serverResponse);

             // Verificar si el email ha sido confirmado
             bool emailVerified = json["user_metadata"]["email_verified"].AsBool;

             if (emailVerified)
             {
                 responseText.text = "¡Inicio de sesión exitoso!";
                 // Cambiar a la siguiente escena
                 SceneManager.LoadScene("LoginScene");  // Reemplaza con el nombre de la escena a la que deseas ir
             }
             else
             {
                 responseText.text = "Por favor, verifica tu correo antes de iniciar sesión.";
             }*/
        }

    }
}
