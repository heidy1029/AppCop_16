using UnityEngine;

public class SocialMediaLinks : MonoBehaviour
{
    // Métodos para abrir cada red social
    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com");
        //Application.OpenURL("https://www.instagram.com/tuEmpresa");
    }

    public void OpenFacebook()
    {
        Application.OpenURL("https://www.facebook.com");
    }

    public void OpenPinterest()
    {
        Application.OpenURL("https://www.pinterest.com");
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://riskfreegame.com/");
    }
}
