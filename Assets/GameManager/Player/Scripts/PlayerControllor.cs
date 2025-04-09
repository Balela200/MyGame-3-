using UnityEditor;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    Animator anim;
    public static PlayerControllor playerControllor;

    [Header("Movement")]
    public CharacterController characterController;
    public float walkSpeed = 5f;
    public float runSpeed = 12f;
    private float activeSpeed;
    private Vector3 moveDirection;

    public bool isCanMove = true;
    public bool isAttacking = false;

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

    [Header("Roll")]
    public float dodgeSpeed = 5f;
    public float dodgeDuration = 0.5f;

    public bool isDodging = true;
    private Vector3 dodgeDirection;
    private float dodgeTimer;

    [Header("Attack")]
    private int comboStep = 0;
    private float lastAttackTime = 0f;
    public float comboResetTime = 1f;
    private bool comboQueued = false;
    public float attackCooldown = 0.5f;
    public float comboWindow = 0.3f;

    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;

        originalCamPos = cam.transform.localPosition;

        Cursor.lockState = CursorLockMode.Locked;

        playerControllor = this;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (isAttacking)
        {
            if (stateInfo.normalizedTime >= 0.9f && !comboQueued)
            {
                Invoke("ResetCombo", 0.1f);
            }
            else if (comboQueued && Time.time - lastAttackTime <= comboWindow && comboStep < 2)
            {
                comboStep++;
                anim.SetInteger("ComboStep", comboStep);
                anim.SetTrigger("Attack");
                comboQueued = false;
                lastAttackTime = Time.time;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                StartCombo();
            }
            else if (isAttacking && comboStep < 2 && Time.time - lastAttackTime < comboWindow)
            {
                comboQueued = true;
            }
        }

        HandleCamera();
        HandleMovement();
        Dodging();

        //if (Input.GetMouseButtonDown(0) && !isAttacking && isCanMove)
        //{
        //    Attack();
        //}


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

        if (inputDir.magnitude > 0.1f && isCanMove)
        {
            activeSpeed = (Input.GetKey(KeyCode.LeftShift) && isGrounded) ? runSpeed : walkSpeed;
        }
        else
        {
            activeSpeed = 0f;
        }

        Vector3 horizontalMove = move * activeSpeed;

        anim.SetBool("Run", activeSpeed == runSpeed);
        anim.SetBool("Walk", activeSpeed == walkSpeed);

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

    void Dodging()
    {
        if (!isDodging)
        {
            dodgeTimer -= Time.deltaTime;

            if (dodgeTimer > 0f)
            {
                Vector3 dodgeMove = dodgeDirection * dodgeSpeed;
                characterController.Move(dodgeMove * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isDodging)
            {
                StartDodge();

                isDodging = false;
                isCanMove = false;
                isAttacking = false;
            }
        }
    }

    void StartDodge()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if (inputDirection.magnitude > 0.1f)
        {
            Vector3 camForward = new Vector3(viewPoint.forward.x, 0f, viewPoint.forward.z).normalized;
            Vector3 camRight = new Vector3(viewPoint.right.x, 0f, viewPoint.right.z).normalized;

            dodgeDirection = (camForward * inputDirection.z + camRight * inputDirection.x).normalized;
        }
        else
        {
            dodgeDirection = new Vector3(viewPoint.forward.x, 0f, viewPoint.forward.z).normalized;
        }

        transform.rotation = Quaternion.LookRotation(dodgeDirection);

        //isDodging = false;
        //isCanMove = false;
        dodgeTimer = dodgeDuration;

        anim.SetTrigger("Dodgeforward");
    }

    //void Attack()
    //{
    //    isCanMove = false;
    //    isDodging = false;
    //    isAttacking = false;

    //    Vector3 lookDirection = viewPoint.forward;
    //    lookDirection.y = 0f;
    //    lookDirection.Normalize();

    //    if (lookDirection.magnitude > 0.1f)
    //    {
    //        transform.rotation = Quaternion.LookRotation(lookDirection);
    //    }

    //    anim.SetTrigger("Attack");
    //}
    void StartCombo()
    {
        isAttacking = true;
        isDodging = false;
        isCanMove = false;
        comboStep = 1;
        anim.SetInteger("ComboStep", comboStep);
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
        comboQueued = false;

        Vector3 lookDirection = viewPoint.forward;
        lookDirection.y = 0f;
        if (lookDirection.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void ResetCombo()
    {
        if (!comboQueued)
        {
            isAttacking = false;
            isDodging = true;
            isCanMove = true;
            comboStep = 0;
            anim.SetInteger("ComboStep", 0);
            comboQueued = false;
        }
    }

    void LateUpdate()
    {
        float moveAmount = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

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
            cam.transform.localPosition = originalCamPos;
        }

        Vector3 targetPos = playerBody.position - viewPoint.forward * cameraDistance + Vector3.up * cameraHeight;
        cam.transform.position = targetPos;
        cam.transform.LookAt(playerBody.position + Vector3.up * 1.5f);
    }
}
