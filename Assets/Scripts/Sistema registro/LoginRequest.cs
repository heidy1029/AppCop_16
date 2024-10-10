using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Necesario para usar TMP_InputField
using UnityEngine.UI; // Necesario para usar Button

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
            // Mostrar la respuesta del servidor
            string serverResponse = request.downloadHandler.text;
            responseText.text = "Respuesta del servidor: " + serverResponse;
            Debug.Log("Respuesta del servidor: " + serverResponse);
        }
    }
}



/*public class LoginRequest : MonoBehaviour
{
    // URL del endpoint para login
    private string loginUrl = "https://tudominio.com/api/login";

    // API Key que vas a mandar en el header
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI responseText;
    // Método para hacer la petición POST
    public void Login(string email, string password)
    {
        // Crear un diccionario con los datos del login
        var loginData = new LoginData
        {
            email = email,
            password = password
        };

        // Convertir los datos a JSON
        string jsonData = JsonUtility.ToJson(loginData);

        // Iniciar la corrutina para la petición
        StartCoroutine(MakePostRequest(jsonData));
    }

    // Corrutina que maneja la petición POST
    IEnumerator MakePostRequest(string jsonData)
    {
        // Crear un objeto UnityWebRequest para una petición POST
        UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");

        // Convertir los datos del cuerpo a un array de bytes
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // Configurar el tipo de contenido a JSON
        request.SetRequestHeader("Content-Type", "application/json");

        // Agregar el header de la API key
        request.SetRequestHeader("Authorization", "apiKey " + apiKey);

        // Configurar el manejador de descargas para recibir la respuesta
        request.downloadHandler = new DownloadHandlerBuffer();

        // Enviar la petición y esperar la respuesta
        yield return request.SendWebRequest();

        // Verificar si ocurrió algún error
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error en la petición: " + request.error);
        }
        else
        {
            Debug.Log("Respuesta recibida: " + request.downloadHandler.text);
        }
    }

    // Clase para los datos del login
    [System.Serializable]
    public class LoginData
    {
        public string email;
        public string password;
    }
}*/
