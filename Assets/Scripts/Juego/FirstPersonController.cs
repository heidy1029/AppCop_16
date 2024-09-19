using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    private float xRotation = 0f;

    // Sensibilidad del mouse
    public float mouseSensitivity = 100f;

    // Referencia a la cámara
    public Camera playerCamera; // Agrega una referencia a la cámara aquí

    // Variable para controlar si el jugador puede moverse
    public bool canMove = true;

    #region Movement Variables
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;
    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;

    private bool isSprinting = false;
    private float sprintRemaining;
    private bool isSprintCooldown = false;
    private float sprintCooldownReset;

    public bool enableJump = true;
    public float mouseSensitivityX = 100f; // Sensibilidad para el movimiento horizontal
    public float mouseSensitivityY = 100f; // Sensibilidad para el movimiento vertical

    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;
    private bool isGrounded = false;

    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchHeight = .75f;
    public float speedReduction = .5f;
    private bool isCrouched = false;
    private Vector3 originalScale;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }

        // Asegúrate de que playerCamera esté asignada
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Si no está asignada, busca la cámara principal
        }

        if (playerCamera == null)
        {
            Debug.LogError("No se encontró la cámara principal. Asigna la cámara manualmente en el Inspector.");
        }
    }

    private void Update()
    {
        if (canMove) // Solo permitir movimiento si canMove es verdadero
        {
            UpdateSprint();
            HandleJumpInput();
            HandleCrouchInput();
            HandleMouseLook();
        }

        CheckGround();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (isCrouched)
        {
            targetVelocity = transform.TransformDirection(targetVelocity) * (walkSpeed * speedReduction);
        }
        else
        {
            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;
        }

        if (isSprinting)
        {
            targetVelocity = transform.TransformDirection(targetVelocity) * sprintSpeed;
        }

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        if (enableSprint && Input.GetKey(sprintKey) && !isSprinting && sprintRemaining > 0 && !isSprintCooldown)
        {
            StartSprint();
        }

        if (enableSprint && Input.GetKeyUp(sprintKey) && isSprinting)
        {
            StopSprint();
        }
    }

    private void HandleJumpInput()
    {
        if (enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    private void HandleCrouchInput()
    {
        if (enableCrouch)
        {
            if (Input.GetKeyDown(crouchKey))
            {
                Crouch();
            }

            if (holdToCrouch && Input.GetKeyUp(crouchKey))
            {
                StopCrouch();
            }
        }
    }
    private void HandleMouseLook()
    {


        if (playerCamera == null)
        {
            return; // Si la cámara no está asignada, no intentamos rotarla
        }

        // Rotación horizontal (control del personaje)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical (control de la cámara)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    private void Crouch()
    {
        transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
        isCrouched = true;
    }

    private void StopCrouch()
    {
        transform.localScale = originalScale;
        isCrouched = false;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void UpdateSprint()
    {
        if (enableSprint)
        {
            if (isSprinting)
            {
                if (!unlimitedSprint)
                {
                    sprintRemaining -= Time.deltaTime;
                    if (sprintRemaining <= 0)
                    {
                        isSprinting = false;
                        isSprintCooldown = true;
                    }
                }
            }
            else
            {
                sprintRemaining = Mathf.Clamp(sprintRemaining += Time.deltaTime, 0, sprintDuration);
            }

            if (isSprintCooldown)
            {
                sprintCooldown -= Time.deltaTime;
                if (sprintCooldown <= 0)
                {
                    isSprintCooldown = false;
                }
            }
            else
            {
                sprintCooldown = sprintCooldownReset;
            }
        }
    }

    private void StartSprint()
    {
        isSprinting = true;
        walkSpeed = sprintSpeed;
    }

    private void StopSprint()
    {
        isSprinting = false;
        walkSpeed = walkSpeed / sprintSpeed; // Restablecer la velocidad de caminar
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
