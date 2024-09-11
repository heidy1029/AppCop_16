using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public float mouseSensitivity = 2f;
    public float defaultFOV = 60f;
    public bool isZoomed = false;
    public GameObject CanvasTrivia;
    private GameObject player;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        playerCamera.fieldOfView = defaultFOV;

        // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (CanvasTrivia != null)
        {
            CanvasTrivia.SetActive(false); // Inicia con el Canvas desactivado
        }

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Solo permite el control de la cámara si el Canvas de Trivia está inactivo
        if (!CanvasTrivia.activeSelf && player != null)
        {
            // Movimiento de la cámara usando el mouse
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationY += mouseX;

            // Limitar la rotación vertical para evitar que la cámara gire completamente
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);

            // Control de Zoom
            if (isZoomed)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, defaultFOV / 2, Time.deltaTime * 10);
            }
            else
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, defaultFOV, Time.deltaTime * 10);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Método para finalizar la trivia y reactivar el control del jugador
    public void EndTrivia()
    {
        if (CanvasTrivia != null)
        {
            CanvasTrivia.SetActive(false);
        }

        if (player != null)
        {
            FirstPersonController controller = player.GetComponent<FirstPersonController>();
            if (controller != null)
            {
                controller.canMove = true;
            }
        }

        // Restaurar el control del cursor y ocultarlo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}