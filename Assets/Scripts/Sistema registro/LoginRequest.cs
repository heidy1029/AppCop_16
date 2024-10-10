using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SupabaseAuth : MonoBehaviour
{
    private string apiUrl = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/token?grant_type=password";
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    void Start()
    {
        // Iniciamos la coroutine para hacer el POST
        StartCoroutine(EnviarDatos());
    }

    IEnumerator EnviarDatos()
    {
        string email = "";
        string password = "";

        // Crear el JSON con email y password usando interpolación de cadenas
        string jsonData = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";

        // Creamos la solicitud POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Agregamos el header de Authorization con el apikey
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        // Enviamos la solicitud y esperamos la respuesta
        yield return request.SendWebRequest();

        // Verificamos si hubo errores
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la petición: " + request.error);
        }
        else
        {
            Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);
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
