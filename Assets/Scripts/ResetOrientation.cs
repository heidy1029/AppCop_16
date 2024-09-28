using UnityEngine;

public class ResetOrientation : MonoBehaviour
{
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; // Cambiar a vertical
    }
}
