using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameProgress : MonoBehaviour
{
    private string apiUrlLevel = "https://tu-supabase-url.supabase.co/rest/v1/level_progress";
    private string apiUrlCards = "https://tu-supabase-url.supabase.co/rest/v1/collected_cards";
    private string apiUrlLanguage = "https://tu-supabase-url.supabase.co/rest/v1/user_settings";
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3bGtkanBjZmNkaWltbWtxeHJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyNjgxNzMsImV4cCI6MjA0Mzg0NDE3M30.rEzatvw8q--aFLcx86SQsSlYsZHYVQTUPkVh2VJxWCU";

    // Método para guardar el progreso del nivel
    public void SaveLevelProgress(int currentLevel, bool completed)
    {
        StartCoroutine(EnviarProgreso(currentLevel, completed));
    }

    // Método para guardar cartas recolectadas
    public void SaveCollectedCard(string cardId)
    {
        StartCoroutine(EnviarCarta(cardId));
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
        string jsonData = $"{{\"user_id\":\"{userId}\",\"current_level\":{currentLevel},\"completed\":{completed.ToString().ToLower()}}}";

        UnityWebRequest request = new UnityWebRequest(apiUrlLevel, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al guardar progreso: " + request.error);
        }
        else
        {
            Debug.Log("Progreso guardado correctamente.");
        }
    }

    // Coroutine para guardar cartas recolectadas
    IEnumerator EnviarCarta(string cardId)
    {
        string userId = PlayerPrefs.GetString("UserId");
        string jsonData = $"{{\"user_id\":\"{userId}\",\"card_id\":\"{cardId}\"}}";

        UnityWebRequest request = new UnityWebRequest(apiUrlCards, "POST");
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
