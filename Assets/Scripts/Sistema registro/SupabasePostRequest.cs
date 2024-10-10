using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class SupabasePostRequest : MonoBehaviour
{
    // URL del endpoint para hacer el POST a la tabla levels_progress
    private string apiUrl = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1/levels_progress";

    // API Key para la autenticación
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    // Token de autorización
    private string authToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    public TextMeshProUGUI responseText; // Para mostrar la respuesta en pantalla

    void Start()
    {
        // Llamamos directamente al método para enviar el progreso
        EnviarProgreso();
    }

    // Método que será llamado para enviar los datos
    public void EnviarProgreso()
    {
        StartCoroutine(PostProgress());
    }

    // Corrutina para hacer la petición POST
    IEnumerator PostProgress()
    {
        // Definimos las variables de prueba que se van a enviar
        bool completed = true; // Ejemplo de valor booleano para 'completed'
        string userId = "123"; // Ejemplo de valor para 'user_id'
        string levelId = "456"; // Ejemplo de valor para 'level_id'

        // Crear el JSON con los datos de prueba
        string jsonData = $"{{\"completed\": {completed.ToString().ToLower()}, \"user_id\": \"{userId}\", \"level_id\": \"{levelId}\"}}";

        // Crear el request tipo POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");

        // Convertir el JSON a un array de bytes
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Configurar los headers necesarios
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Authorization", authToken);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Prefer", "return=minimal"); // Para obtener una respuesta mínima

        // Enviar la solicitud y esperar la respuesta
        yield return request.SendWebRequest();

        // Verificar si ocurrió algún error
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la petición: " + request.error);
        }
        else
        {
            // Mostramos la respuesta en el TextMeshProUGUI (puede ser un mensaje vacío con el header "Prefer: return=minimal")
            responseText.text = "Datos enviados correctamente.";
            Debug.Log("Datos enviados correctamente.");
        }
    }
}