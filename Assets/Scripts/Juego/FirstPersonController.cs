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
    public float jumpPower = 5f;

    private bool isSprinting = false;

    // Sprinting variables
    public bool enableSprint = true;
    public float sprintDuration = 5f;
    private float sprintRemaining;
    private bool isSprintCooldown = false;
    public float sprintCooldown = 0.5f;
    #endregion

    private Vector3 lastGroundedPosition;
    private float groundCheckRadius = 0.7f;

    public KeyCode sprintKeyDesktop = KeyCode.LeftShift;
    public KeyCode jumpKeyDesktop = KeyCode.Space;

    // Joystick variables
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string sprintKey = "Fire3";
    public string jumpButton = "Jump";

    public string mouseHorizontalAxis = "Mouse X";
    public string mouseVerticalAxis = "Mouse Y";

    private bool _isMobile;

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

    private void Start()
    {
        _isMobile = LevelManager.Instance.IsMobile;

        EventController.OnTriviaStarted += OnTriviaStarted;
        EventController.OnTriviaCompleted += OnTriviaCompleted;
    }

    private void OnTriviaStarted(int triviaId)
    {
        canMove = false;
    }

    private void OnTriviaCompleted(int triviaId, bool visibleCursor)
    {
        canMove = !visibleCursor;
    }

    private Vector3 GetAxisInput()
    {
        if(_isMobile)
        {
            return new Vector3(SimpleInput.GetAxis(horizontalAxis), 0, SimpleInput.GetAxis(verticalAxis));
        }
        else
        {
            return new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis));
        }
    }

    private bool GetSprintInput(bool isDown)
    {
        if (_isMobile)
        {
            return isDown ? SimpleInput.GetButton(sprintKey) : SimpleInput.GetButtonUp(sprintKey);
        }
        else
        {
            return isDown ? Input.GetKey(sprintKeyDesktop) : Input.GetKeyUp(sprintKeyDesktop);
        }
    }

    private bool GetJumpInput()
    {
        if (_isMobile)
        {
            return SimpleInput.GetButtonDown(jumpButton);
        }
        else
        {
            return Input.GetKeyDown(jumpKeyDesktop);
        }
    }

    private Vector2 GetMouseInput()
    {
        if (_isMobile)
        {
            return new Vector2(SimpleInput.GetAxis(mouseHorizontalAxis), SimpleInput.GetAxis(mouseVerticalAxis));
        }
        else
        {
            return new Vector2(Input.GetAxis(mouseHorizontalAxis), Input.GetAxis(mouseVerticalAxis));
        }
    }

    private void Update()
    {
        if (canMove)
        {
            HandleInput();
            UpdateSprint();
            CheckIfBlockedAfterJump();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
        else
        {
            // Frenar suavemente
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    private void HandleInput()
    {
        if(GetJumpInput() && IsGrounded()) Jump();

        if (enableSprint)
        {
            if (GetSprintInput(true) && !isSprinting && sprintRemaining > 0 && !isSprintCooldown) StartSprint();
            if (GetSprintInput(false)) StopSprint();
        }

        HandleMouseLook();
    }

    private void HandleMovement()
    {
        float moveX = GetAxisInput().x;
        float moveZ = GetAxisInput().z;

        Vector3 moveDirection = transform.TransformDirection(new Vector3(moveX, 0, moveZ));

        Vector3 obstacleCheckPosition = GetGroundCheckPosition + moveDirection * 0.5f;
        bool isBlocked = Physics.CheckSphere(obstacleCheckPosition, groundCheckRadius, LayerMask.GetMask("LockedPlane"));

        if (isBlocked)
        {
            moveX = 0f;
            moveZ = 0f;
            Debug.Log("Cannot advance into locked plane!");
        }
        else
        {
            if (IsGrounded()) // Update last grounded position only if the player is on the ground
            {
                lastGroundedPosition = transform.position;
            }
        }

        Vector3 targetVelocity = transform.TransformDirection(new Vector3(moveX, 0, moveZ)) * (isSprinting ? sprintSpeed : walkSpeed);
        Vector3 velocityChange = targetVelocity - rb.velocity;
        velocityChange.y = 0;
        rb.AddForce(Vector3.ClampMagnitude(velocityChange, 10f), ForceMode.VelocityChange);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void CheckIfBlockedAfterJump()
    {
        bool isBlocked = Physics.CheckSphere(GetGroundCheckPosition, groundCheckRadius, LayerMask.GetMask("LockedPlane"));

        if (isBlocked)
        {
            transform.position = lastGroundedPosition; // Respawn to last grounded position
            Debug.Log("Respawned to last grounded position!");
        }
    }

    private Vector3 GetGroundCheckPosition => transform.position + Vector3.down * (groundCheckRadius - 0.1f);

    private bool IsGrounded()
    {
        int layerMask = LayerMask.GetMask("Ground", "UnlockedPlane", "LockedPlane");
        return Physics.CheckSphere(GetGroundCheckPosition, groundCheckRadius, layerMask);
    }

    private void HandleMouseLook()
    {
        float mouseX = GetMouseInput().x * 100f * Time.deltaTime;
        float mouseY = GetMouseInput().y * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
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

    private void OnDestroy()
    {
        EventController.OnTriviaStarted -= OnTriviaStarted;
        EventController.OnTriviaCompleted -= OnTriviaCompleted;
    }
}
