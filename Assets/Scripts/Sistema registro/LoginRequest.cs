using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class LoginRequest : MonoBehaviour
{
    // URL del endpoint para login
    private string loginUrl = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/signup";  // Reemplaza con tu URL real

    // API Key que vas a mandar en el header
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    // Referencias a los campos de entrada para correo y contraseña
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI responseText;  // Para mostrar el resultado en la UI

    // Método que se llamará al presionar el botón de login
    public void OnLoginButtonPressed()
    {
        // Obtener los valores ingresados por el usuario
        string email = emailInput.text;
        string password = passwordInput.text;

        // Validar que los campos no estén vacíos
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            responseText.text = "Por favor ingrese el correo y la contraseña.";
            return;
        }

        // Iniciar el proceso de login
        Login(email, password);
    }

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
        UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apiKey ", apiKey);

        request.downloadHandler = new DownloadHandlerBuffer();

        Debug.Log("Enviando solicitud a: " + loginUrl);
        Debug.Log("Datos de solicitud: " + jsonData);
        Debug.Log("Encabezado de autorización: apiKey " + apiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error en la petición: " + request.error);
            responseText.text = "Error: " + request.error;
        }
        else
        {
            Debug.Log("Respuesta recibida: " + request.downloadHandler.text);
            responseText.text = "Login exitoso: " + request.downloadHandler.text;
        }
    }



    // Método para permitir el ingreso
    private void AccesoPermitido()
    {
        Debug.Log("Acceso permitido, redirigiendo...");
        // Aquí puedes cambiar de escena, desbloquear un menú, etc.
        // Por ejemplo: SceneManager.LoadScene("NombreDeLaEscena");
    }

    // Clase para los datos del login
    [System.Serializable]
    public class LoginData
    {
        public string email;
        public string password;
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
