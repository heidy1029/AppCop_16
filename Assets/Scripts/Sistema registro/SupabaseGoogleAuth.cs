using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SupabaseGoogleAuth : MonoBehaviour
{
    private string supabaseUrl = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/authorize";
    private string clientId = "TU_GOOGLE_CLIENT_ID";
    private string redirectUri = "https://vwlkdjpcfcdiimmkqxrx.supabase.co/auth/v1/callback";
    private string apiKey = "TU_API_KEY_DE_SUPABASE";

    public Button googleLoginButton;

    void Start()
    {
        googleLoginButton.onClick.AddListener(OnGoogleLoginButtonClicked);
    }

    void OnGoogleLoginButtonClicked()
    {
        // Crear la URL de autenticación para redirigir al usuario
        string authUrl = $"{supabaseUrl}?provider=google&redirect_to={redirectUri}&client_id={clientId}&response_type=token&apikey={apiKey}";

        // Abrir el navegador web predeterminado en la URL de Google para la autenticación
        Application.OpenURL(authUrl);
    }
    void HandleGoogleAuthResponse(string token)
    {
        // Guardar el token recibido
        PlayerPrefs.SetString("AuthToken", token);
        PlayerPrefs.Save();

        // Redirigir a la pantalla principal
        SceneManager.LoadScene("MainMenu");
    }

}
