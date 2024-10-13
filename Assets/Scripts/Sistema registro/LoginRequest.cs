using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Necesario para usar TMP_InputField
using UnityEngine.UI; // Necesario para usar Button
//using Newtonsoft.Json; // Ejemplo si usas Newtonsoft
using SimpleJSON;
using UnityEngine.SceneManagement;



public class SupabaseAuth : MonoBehaviour
{
    private string apiUrl = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/token?grant_type=password";
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    // Referencias a los campos de entrada de correo y contraseña
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    // Referencia al texto para mostrar la respuesta
    public TextMeshProUGUI responseText;

    // Referencia al botón de ingresar
    public Button loginButton;

    void Start()
    {
        // Asignamos la función OnLoginButtonClicked al botón de ingresar
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    void OnLoginButtonClicked()
    {
        // Al hacer clic en el botón, iniciamos la coroutine para enviar los datos
        StartCoroutine(EnviarDatos());
    }
    IEnumerator EnviarDatos()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            responseText.text = "Por favor, llena todos los campos.";
            yield break;
        }

        string jsonData = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";

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
            // Parsear la respuesta JSON
            // Parsear la respuesta JSON
            string serverResponse = request.downloadHandler.text;
            Debug.Log("Respuesta completa del servidor: " + serverResponse);

            // Usar SimpleJSON o cualquier otra librería para parsear el JSON
            var json = SimpleJSON.JSON.Parse(serverResponse);
            string userId = json["user"]["id"];  // Acceder al campo "id" dentro de "user"

            // Verificar si se obtuvo el UserId
            if (!string.IsNullOrEmpty(userId))
            {
                // Guardar el userId en PlayerPrefs
                PlayerPrefs.SetString("UserId", userId);
                PlayerPrefs.Save();
                Debug.Log("UserId guardado correctamente: " + userId);

                // Mostrar mensaje amigable
                responseText.text = "¡Inicio de sesión exitoso!";

                // Redirigir a la escena principal
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                Debug.LogError("Error: UserId no encontrado en la respuesta del servidor.");
                responseText.text = "Error al iniciar sesión. Intenta nuevamente.";
            }


        }

        /* IEnumerator EnviarDatos()
         {
             // Obtener el correo y la contraseña ingresados
             string email = emailInput.text;
             string password = passwordInput.text;

             // Verificar si los campos están llenos
             if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
             {
                 responseText.text = "Por favor, llena todos los campos.";
                 yield break; // Detener la ejecución si no están llenos
             }

             // Crear el JSON con email y password
             string jsonData = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";

             // Crear la solicitud POST
             UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
             byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
             request.uploadHandler = new UploadHandlerRaw(bodyRaw);
             request.downloadHandler = new DownloadHandlerBuffer();

             // Agregar el header de autorización con el API Key
             request.SetRequestHeader("apikey", apiKey);
             request.SetRequestHeader("Content-Type", "application/json");

             // Enviar la solicitud y esperar la respuesta
             yield return request.SendWebRequest();

             // Verificar si hubo errores en la solicitud
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
                 SceneManager.LoadScene("MainScene");

                 // Convertir la respuesta JSON a un objeto dinámico para acceder a los campos
                 /*var json = SimpleJSON.JSON.Parse(serverResponse);

                 // Verificar si el email ha sido confirmado
                 bool emailVerified = json["user_metadata"]["email_verified"].AsBool;

                 if (emailVerified)
                 {
                     responseText.text = "¡Inicio de sesión exitoso!";
                     // Cambiar a la siguiente escena
                     SceneManager.LoadScene("MainScene");  // Reemplaza con el nombre de la escena a la que deseas ir
                 }
                 else
                 {
                     responseText.text = "Por favor, verifica tu correo antes de iniciar sesión.";
                 }*/
    }
}


