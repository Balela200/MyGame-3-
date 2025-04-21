using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    public Animator anim;
    public static PlayerControllor playerControllor;

    [Header("Movement")]
    public CharacterController characterController;
    public float walkSpeed = 5f;
    public float runSpeed = 12f;
    private float activeSpeed;
    private Vector3 moveDirection;

    public bool isCanMove = true;
    public bool isCanRotation = true;
    public bool isAttacking = true;

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

    public bool isCanRotationCamera = true;

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
    public float attackCooldown = 0.2f;
    public float comboWindow = 0.3f;

    public float searchRadius = 5f;
    public LayerMask enemyLayer;

    public GameObject BoxAttack;

    public Transform[] enemies;
    private Transform targetEnemy;

    [Header("Shield")]
    public bool isShield = false;

    [Header("Camp")]
    public bool isCamp = false;
    CampSystem CampOther;
    public bool isSit = false;
    public AudioSource lightFire;

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

        if (comboQueued && !isAttacking && comboStep < 2)
        {
            comboStep++;
            anim.SetInteger("ComboStep", comboStep);
            anim.SetTrigger("Attack");
            comboQueued = false;
            lastAttackTime = Time.time;
            isAttacking = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking && StaminaSystem.staminaSystem.Stamina >= 5)
            {
                StartCombo();
                StaminaSystem.staminaSystem.StaminaLoss(5);
            }
            else if (isAttacking && comboStep < 2)
            {
                comboQueued = true;
            }
        }

        if (comboQueued && !isAttacking && comboStep < 2)
        {
            ExecuteNextCombo();
        }

        HandleCamera();
        HandleMovement();
        Dodging();
        InputPlayer();
    }

    void HandleMovement()
    {
        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.25f, groundLayers);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 move = Vector3.zero;
        if (inputDir.magnitude > 0.1f && isCanRotation)
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
        if (isCanRotationCamera)
        {
            mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

            rotationX += mouseInput.x;
            verticalRotation += invertLook ? mouseInput.y : -mouseInput.y;
            verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);

            viewPoint.rotation = Quaternion.Euler(verticalRotation, rotationX, 0f);
        }
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
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isDodging && StaminaSystem.staminaSystem.Stamina >= 30)
            {
                StartDodge();

                StaminaSystem.staminaSystem.StaminaLoss(30);

                isDodging = false;
                isCanMove = false;
                isCanRotation = false;
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

        dodgeTimer = dodgeDuration;

        anim.SetTrigger("Dodgeforward");
    }
    void StartCombo()
    {
        isAttacking = true;
        isDodging = false;
        isCanMove = false;
        isCanRotation = false;

        comboStep = 1;
        anim.SetInteger("ComboStep", comboStep);
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
        comboQueued = false;

        RotateTowardsTarget();
    }

    Transform GetClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, searchRadius, enemyLayer);

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }

    public void EndAttack()
    {
        isAttacking = false;
        isDodging = true;
        isCanMove = true;
        isCanRotation = true;

        if (comboStep >= 2 || !comboQueued)
        {
            comboStep = 0;
            anim.SetInteger("ComboStep", 0);
            comboQueued = false;
        }
    }

    void ExecuteNextCombo()
    {
        comboStep++;
        anim.SetInteger("ComboStep", comboStep);
        anim.SetTrigger("Attack");
        comboQueued = false;
        lastAttackTime = Time.time;
        isAttacking = true;
    }


    void RotateTowardsTarget()
    {
        Transform targetEnemy = GetClosestEnemy();
        Vector3 direction = targetEnemy != null ?
            (targetEnemy.position - transform.position).normalized :
            viewPoint.forward;

        direction.y = 0f;
        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Camp"))
        {
            CampOther = other.gameObject.GetComponent<CampSystem>();
            isCamp = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Camp"))
        {
            isCamp = false;

            CampOther = null;
        }
    }

    void InputPlayer()
    {
        if (Input.GetKeyUp(KeyCode.E) && isCamp)
        {
            bool isActive = CampManager.campManager.CampUIGameObject.activeSelf;

            if (isActive)
            {
                CampManager.campManager.CampUIGameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;

                isCanMove = true;
                isAttacking = false;
                isDodging = true;
                isCanRotation = true;

                // Can Rotation Camera
                isCanRotationCamera = true;

                CampManager.campManager.travelUI.SetActive(false);
            }
            else
            {
                CampManager.campManager.CampUIGameObject.SetActive(true);

                Cursor.lockState = CursorLockMode.None;

                isCanMove = false;
                isAttacking = true;
                isDodging = false;
                isCanRotation = false;

                //  Dont Rotation Camera
                isCanRotationCamera = false;


                if (CampOther.Camp1 == true)
                {
                    CampManager.isCamp1 = true;
                }
                else if (CampOther.Camp2 == true)
                {
                    CampManager.isCamp1 = false;
                    CampManager.isCamp2 = true;
                }

                if(CampManager.isCamp1 == false)
                {
                    CampManager.campManager.AudioCamp1.Play();
                }
                else if(CampManager.isCamp2 == false)
                {
                    CampManager.campManager.AudioCamp2.Play();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            bool isActive = GameManager.gameManager.UIEsc.activeSelf;

            if(isActive)
            {
                GameManager.gameManager.UIEsc.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;

                isCanMove = true;
                isAttacking = false;
                isDodging = true;
                isCanRotation = true;

                // Can Rotation Camera  
                isCanRotationCamera = true;
            }
            else
            {
                GameManager.gameManager.UIEsc.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

                isCanMove = false;
                isAttacking = true;
                isDodging = false;
                isCanRotation = false;

                //  Dont Rotation Camera
                isCanRotationCamera = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.X) && isSit)
        {
            anim.SetBool("Sit", false);
            isSit = false;
            CampManager.campManager.CampUIGameObject.SetActive(false);

            //  Can Rotation Camera
            isCanRotationCamera = true;
            Cursor.lockState = CursorLockMode.Locked;

            CampManager.campManager.isHeal = false;
            CampManager.campManager.travelUI.SetActive(false);
        }

        if(Input.GetMouseButtonDown(1) && StaminaSystem.staminaSystem.Stamina >= 20)
        {
            anim.SetBool("Shield", true);

            isShield = true;

            isCanMove = false;
            isAttacking = true;
            isCanRotation = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("Shield", false);

            isShield = false;

            isCanMove = true;
            isAttacking = false;
            isCanRotation = true;
        }
        else if(StaminaSystem.staminaSystem.Stamina < 20)
        {
            anim.SetBool("Shield", false);

            isShield = false;

            isCanMove = true;
            isAttacking = false;
            isCanRotation = true;
        }

    }

    public void AttackBox()
    {
        BoxAttack.SetActive(true);
    }

    public void EndAttackBox()
    {
        BoxAttack.SetActive(false);
    }
}
