using UnityEngine;

public class SetOrientation : MonoBehaviour
{
    void Start()
    {
        // Cambiar la orientación a horizontal al iniciar la escena de juego
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
}
