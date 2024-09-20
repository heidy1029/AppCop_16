using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    private float xRotation = 0f;

    public Camera playerCamera; // Reference to the camera
    public bool canMove = true;

    #region Movement Variables
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;

    private bool isSprinting = false;
    private bool isGrounded = false;

    // Sprinting variables
    public bool enableSprint = true;
    public float sprintDuration = 5f;
    private float sprintRemaining;
    private bool isSprintCooldown = false;
    public float sprintCooldown = 0.5f;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = playerCamera ?? Camera.main;

        if (playerCamera == null)
        {
            Debug.LogError("No main camera found. Assign camera manually in the Inspector.");
        }

        sprintRemaining = sprintDuration;
    }

    private void Update()
    {
        if (canMove)
        {
            HandleInput();
            CheckGround();
            DetectPlaneAccess(); // Check access to planes
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded) Jump();

        if (enableSprint)
        {
            if (Input.GetKey(sprintKey) && !isSprinting && sprintRemaining > 0 && !isSprintCooldown) StartSprint();
            if (Input.GetKeyUp(sprintKey)) StopSprint();
        }

        HandleMouseLook();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 targetVelocity = transform.TransformDirection(new Vector3(moveX, 0, moveZ)) * (isSprinting ? sprintSpeed : walkSpeed);

        Vector3 velocityChange = targetVelocity - rb.velocity;
        velocityChange.y = 0; // No change in vertical velocity
        rb.AddForce(Vector3.ClampMagnitude(velocityChange, 10f), ForceMode.VelocityChange);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void DetectPlaneAccess()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            if (hit.collider.CompareTag("LockedPlane"))
            {
                // Detener el movimiento hacia adelante
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0f); // Detiene el movimiento en la dirección Z
                Debug.Log("Cannot advance into locked plane!");
            }
        }
    }

    private void UpdateSprint()
    {
        if (isSprinting)
        {
            sprintRemaining -= Time.deltaTime;
            if (sprintRemaining <= 0) StopSprint();
        }
        else
        {
            sprintRemaining = Mathf.Clamp(sprintRemaining + Time.deltaTime, 0, sprintDuration);
        }
    }

    private void StartSprint()
    {
        isSprinting = true;
    }

    private void StopSprint()
    {
        isSprinting = false;
        sprintRemaining = Mathf.Clamp(sprintRemaining + sprintCooldown, 0, sprintDuration);
    }
}
