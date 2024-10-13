using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameProgress : MonoBehaviour
{
    private string baseUrl = "https://supabase.com/dashboard/project/vwlkdjpcfcdiimmkqxrx/api?resource=users"; // Reemplaza <tu-project-id> con tu ID real
    private string apiUrlLevel = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1/levels_progress";
    private string apiUrlCards = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1/collected_cards";
    private string apiUrlLanguage = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/rest/v1/user_settings";
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";
    // Método para guardar el progreso del nivel
    public void SaveLevelProgress(int currentLevel, bool completed)
    {
        StartCoroutine(EnviarProgreso(currentLevel, completed));
    }

    // Método para guardar cartas recolectadas
    public void SaveCollectedCard(int cardIndex, int currentLevel)
    {
        StartCoroutine(EnviarCarta(cardIndex, currentLevel));
    }

    // Método para guardar la selección de idioma
    public void SaveLanguageSetting(string language)
    {
        StartCoroutine(EnviarIdioma(language));
    }

    // Coroutine para guardar el progreso del nivel
    IEnumerator EnviarProgreso(int currentLevel, bool completed)
    {
        string userId = PlayerPrefs.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("UserId no encontrado en PlayerPrefs.");
            yield break;
        }

        string jsonData = $"{{\"user_id\":\"{userId}\",\"current_level\":{currentLevel},\"completed\":{completed.ToString().ToLower()}}}";
        Debug.Log("JSON enviado: " + jsonData); // Debug JSON antes de enviarlo

        UnityWebRequest request = new UnityWebRequest(apiUrlLevel, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al guardar progreso: " + request.error + " - Respuesta: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Progreso guardado correctamente. Respuesta: " + request.downloadHandler.text);
        }

    }

    // Coroutine para guardar cartas recolectadas con nivel
    IEnumerator EnviarCarta(int cardIndex, int currentLevel)
    {
        string userId = PlayerPrefs.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("UserId no encontrado en PlayerPrefs.");
            yield break;
        }

        string jsonData = $"{{\"user_id\":\"{userId}\",\"card_index\":{cardIndex},\"level\":{currentLevel}}}";

        UnityWebRequest request = new UnityWebRequest(apiUrlCards, "PATCH");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al guardar carta: " + request.error);
        }
        else
        {
            Debug.Log("Carta guardada correctamente.");
        }
    }

    // Coroutine para guardar la selección de idioma
    IEnumerator EnviarIdioma(string language)
    {
        string userId = PlayerPrefs.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("UserId no encontrado en PlayerPrefs.");
            yield break;
        }

        string jsonData = $"{{\"user_id\":\"{userId}\",\"language\":\"{language}\"}}";

        UnityWebRequest request = new UnityWebRequest(apiUrlLanguage, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al guardar el idioma: " + request.error);
        }
        else
        {
            Debug.Log("Idioma guardado correctamente.");
        }
    }
}