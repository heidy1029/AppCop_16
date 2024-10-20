using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private const string TOKEN_EXPIRY_KEY = "TokenExpiryTime";
    private const string USER_ID_KEY = "UserId";
    private const int EXPIRY_WARNING_MINUTES = 30;
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;

    private void Awake()
    {
#if UNITY_EDITOR
        emailInputField.text = "test@gmail.com";
        passwordInputField.text = "111111";
#endif
    }

    public async void onLoginButtonClick()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        string url = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/token?grant_type=password";
        string jsonBody = "{\"email\": \"" + email + "\", \"password\": \"" + password + "\"}";

        // Note: We're passing false for requiresAuth since this is a login request
        string response = await Request.SendRequest(url, "POST", jsonBody, false, false);



        if (response != null)
        {
            // Save the access token and other details
            SaveAccessToken(response);

            // Retrieve or create user settings
            await DataController.Instance.RetrieveOrCreateUserSettings(false);

            SceneManager.LoadScene("MainScene");
        }
    }

    private void SaveAccessToken(string response)
    {
        try
        {
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(response);

            DataController.Instance.SaveAccessToken(loginResponse.access_token);
            Request.SaveAccessToken(loginResponse.access_token);

            // Save expiry time
            long expiryTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + loginResponse.expires_in;
            DataController.Instance.SaveTokenExpiryTime(expiryTime);
            PlayerPrefs.SetString(TOKEN_EXPIRY_KEY, expiryTime.ToString());

            // Save user ID
            DataController.Instance.SaveUserId(loginResponse.user.id);
            PlayerPrefs.SetString(USER_ID_KEY, loginResponse.user.id);

            PlayerPrefs.Save();

            Debug.Log($"Token will expire at: {DateTimeOffset.FromUnixTimeSeconds(expiryTime).LocalDateTime}");
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving access token: " + e.Message);
        }
    }

    public static string GetUserId()
    {
        return PlayerPrefs.GetString(USER_ID_KEY, "");
    }

    // Método para ser llamado por un botón
    public void CheckTokenExpiryButtonClick()
    {
        if (CheckTokenExpiry())
        {
            Debug.Log("Token is still valid.");
        }
        else
        {
            Debug.Log("Token has expired or is about to expire. Clearing all storage.");
            ClearAllStorage();
        }
    }

    private bool CheckTokenExpiry()
    {
        string expiryTimeStr = PlayerPrefs.GetString(TOKEN_EXPIRY_KEY, "");
        if (!string.IsNullOrEmpty(expiryTimeStr) && long.TryParse(expiryTimeStr, out long expiryTime))
        {
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long timeUntilExpiry = expiryTime - currentTime;

            return timeUntilExpiry > EXPIRY_WARNING_MINUTES * 60;
        }
        return false; // Si no hay tiempo de expiración guardado, consideramos que el token ha expirado
    }

    private void ClearAllStorage()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All storage has been cleared due to token expiration.");
        // Aquí podrías añadir lógica adicional, como redirigir al usuario a la pantalla de login
    }

    [System.Serializable]
    private class LoginResponse
    {
        public string access_token;
        public int expires_in;
        public long expires_at;
        public string refresh_token;
        public User user;
    }

    [System.Serializable]
    private class User
    {
        public string id;
        public string email;
        // Add other user properties as needed
    }
}
