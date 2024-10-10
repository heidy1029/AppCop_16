using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class SupabaseGetRequest : MonoBehaviour
{
    // URL del endpoint para hacer el GET de los usuarios
    private string apiUrl = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1/levels_progress?select=*";

    // API Key para la autenticación
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    // Token de autorización
    private string authToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    public Button button;
    public TextMeshProUGUI responseText; // Para mostrar la respuesta en pantalla

    void Start()
    {
        // Asociamos el botón a la función de obtener usuarios
        button.onClick.AddListener(ObtenerUsuarios);
    }

    // Método que será llamado cuando se presione el botón
    public void ObtenerUsuarios()
    {
        StartCoroutine(GetUsers());
    }

    // Corrutina para hacer la petición GET
    IEnumerator GetUsers()
    {
        // Crear el request tipo GET
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Configuramos los headers de autenticación
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Authorization", authToken);

        // Enviamos la solicitud y esperamos la respuesta
        yield return request.SendWebRequest();

        // Verificamos si hubo algún error en la petición
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la petición: " + request.error);
        }
        else
        {
            // Mostramos la respuesta en el TextMeshProUGUI
            responseText.text = "Respuesta del servidor: " + request.downloadHandler.text;
            Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);
        }
    }
}