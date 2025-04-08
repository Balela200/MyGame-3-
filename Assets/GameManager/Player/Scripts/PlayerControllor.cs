using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController characterController;
    public float walkSpeed = 5f;
    public float runSpeed = 12f;
    private float activeSpeed;
    private Vector3 moveDirection;

    [Header("Jump & Gravity")]
    public float jumpForce = 12f;
    public float gravityMultiplier = 3f;
    public Transform groundCheckPoint;
    public LayerMask groundLayers;
    private bool isGrounded;

    [Header("Camera")]
    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    private float verticalRotation;
    private float rotationX;
    private Vector2 mouseInput;
    public bool invertLook;
    private Camera cam;
    private Vector3 camOffset;

    [Header("Camera Shake")]
    public float walkShakeAmount = 0.02f;
    public float runShakeAmount = 0.05f;
    private float shakeTimer;
    public float walkShakeSpeed = 10f;
    public float runShakeSpeed = 18f;

    private Vector3 originalCamPos;

    [Header("Third Person View")]
    public Transform playerBody;
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;

        // ???? ???? ???????? ?????? ???? ???? ?? ??? ????????
        originalCamPos = cam.transform.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleCamera();
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }

    void HandleMovement()
    {
        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.25f, groundLayers);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 move = Vector3.zero;
        if (inputDir.magnitude > 0.1f)
        {
            Vector3 camForward = Quaternion.Euler(0f, rotationX, 0f) * Vector3.forward;
            Vector3 camRight = Quaternion.Euler(0f, rotationX, 0f) * Vector3.right;

            move = (camForward * vertical + camRight * horizontal).normalized;

            Quaternion toRotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        activeSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 horizontalMove = move * activeSpeed;

        if (isGrounded)
        {
            if (moveDirection.y < 0)
                moveDirection.y = -2f;

            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        moveDirection.x = horizontalMove.x;
        moveDirection.z = horizontalMove.z;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleCamera()
    {
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

        rotationX += mouseInput.x;
        verticalRotation += invertLook ? mouseInput.y : -mouseInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);

        viewPoint.rotation = Quaternion.Euler(verticalRotation, rotationX, 0f);
    }

    void LateUpdate()
    {
        float moveAmount = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // ???? ???????? ????? ??? ??????
        float shakeAmount = moveAmount > 0.1f ?
            (isRunning ? runShakeAmount : walkShakeAmount) : 0f;

        float shakeSpeed = isRunning ? runShakeSpeed : walkShakeSpeed;

        if (shakeAmount > 0)
        {
            shakeTimer += Time.deltaTime * shakeSpeed;
            float yShake = Mathf.Sin(shakeTimer) * shakeAmount;
            float xShake = Mathf.Cos(shakeTimer * 0.5f) * (shakeAmount / 2f);
            cam.transform.localPosition = originalCamPos + new Vector3(xShake, yShake, 0f);
        }
        else
        {
            shakeTimer = 0;
            cam.transform.localPosition = originalCamPos; // ?????? ?????? ?????? ??? ???? ??????
        }

        // ????? ???????? ??? ??????
        Vector3 targetPos = playerBody.position - viewPoint.forward * cameraDistance + Vector3.up * cameraHeight;
        cam.transform.position = targetPos;
        cam.transform.LookAt(playerBody.position + Vector3.up * 1.5f);
    }
}
