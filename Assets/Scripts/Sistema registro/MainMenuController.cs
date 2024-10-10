using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;

    void Start()
    {
        // Cargar el nombre o correo del usuario desde PlayerPrefs
        string userEmail = PlayerPrefs.GetString("UserEmail", "Usuario");
        welcomeText.text = "¡Bienvenido, " + userEmail + "!";
        // Guardar el correo del usuario


    }
}
