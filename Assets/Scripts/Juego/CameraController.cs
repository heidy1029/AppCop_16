using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private FirstPersonController _firstPersonController;
    public Camera playerCamera;
    public float mouseSensitivity = 2f;
    public float defaultFOV = 60f;
    public bool isZoomed = false;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        playerCamera.fieldOfView = defaultFOV;
    }

    void Update()
    {
        if (_firstPersonController == null) return;

        if (_firstPersonController.canMove)
        {
            // Movimiento de la cámara usando el mouse
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotación vertical (movimiento hacia arriba y hacia abajo)
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limitar la rotación vertical

            // Rotación horizontal (movimiento de izquierda a derecha)
            rotationY += mouseX;

            // Aplicar la rotación en los ejes correspondientes
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

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
    }
}
