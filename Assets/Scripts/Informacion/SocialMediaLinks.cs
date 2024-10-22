using UnityEngine;

public class SocialMediaLinks : MonoBehaviour
{
    // Métodos para abrir cada red social
    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com");
        //Application.OpenURL("https://www.instagram.com/tuEmpresa");
    }

    public void OpenLinkedin()
    {
        Application.OpenURL("https://www.linkedin.com/login/es?fromSignIn=true&trk=guest_homepage-basic_nav-header-signin");
    }

    public void OpenTiktok()
    {
        Application.OpenURL("https://www.tiktok.com/es/");
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://riskfreegame.com/");
    }
}
