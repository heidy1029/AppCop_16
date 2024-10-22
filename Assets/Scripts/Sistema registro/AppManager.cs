using UnityEngine;

public class AppManager : MonoBehaviour
{
    // Función que será llamada al hacer clic en el botón para cerrar la aplicación
    public void QuitApp()
    {
        #if UNITY_EDITOR
            // Esto solo funciona en el editor de Unity para cerrar el modo de juego
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Cerrar la aplicación en un dispositivo móvil
            Application.Quit();

            // Para asegurar el cierre en Android
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        #endif
    }
}
