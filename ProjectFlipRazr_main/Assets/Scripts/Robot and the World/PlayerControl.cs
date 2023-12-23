using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerControl : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float baseSpeed = 6.0f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float currentSpeed = 6.0f;
    [SerializeField] private float gravityValue = -9.81f;
    public Animator animator;
    //[SerializeField] private float controllerDeadzone = 0.1f;
    //[SerializeField] private float gamepadRotateSmoothing = 1000f;
    [Header("Input Type")]
    [SerializeField] public bool isController;

    private Gamepad gamepad;

    public bool canTeleport = true;

    private Vector2 movement;
    private Vector3 playerVelocity;

    public bool inCutscene = false;


    private CharacterController controller;
    private PlayerInput playerInput; // This is a reference to the Player Input component in our scene.

    public AudioSource movementNoise;

    public Vector3 move;


    // All of these values are for the gradual movement pitch change.
    public float pitchStartValue = 0.25f; // The initial value
    public float pitchEndValue = 1.0f;    // The desired value
    public float pitchChangeDuration = 1.0f; // Duration of the transition

    private float pitchTimer = 0.0f;

    bool isMoving;

    public InputActionAsset moveInputManager;
    public InputAction moveAction;

    public CinemachineBrain cinemachineBrain;
    public Transform currentActiveCam;
    public Transform storedActiveCam;


    private void Awake()
    {
        gamepad = Gamepad.current;
        
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveInputManager = playerInput.actions;
        moveAction = moveInputManager.FindAction("Move", false);
        Debug.Log("moveAction is " + moveAction);
        moveAction.started += OnMovementStarted;
        moveAction.performed += OnMovementPerformed;
        moveAction.canceled += OnMovementCanceled;
        GetActiveCamera();


    }

    public void Update()
    {
        GetActiveCamera();   
    }
    public void GetActiveCamera()
    {
        if (cinemachineBrain != null)
        {
            CinemachineVirtualCamera virtualCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;

            if (virtualCamera != null)
            {
                currentActiveCam = virtualCamera.transform;
            }
        }
    }
    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        Debug.Log("MovementStarted");
        // Change forward to direction relative to current active Cinemachine Camera.
        storedActiveCam = currentActiveCam;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsIdle", false);
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("MovementPerformed");
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("MovementCanceled");
        // Change forward to direction relative to current active Cinemachine Camera.
        storedActiveCam = currentActiveCam;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);

    }

    private void OnEnable()
    {
        moveAction.Enable();
        animator.SetBool("IsIdle", true);
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }


    private void FixedUpdate()
    {
        if (!inCutscene)  // check for knockback 
        {
            HandleInput();
            HandleRotation();
            GetActiveCamera();
            HandleMovement();
        }

        currentSpeed = CalculateSpeed();
    }

    private void LateUpdate()
    {

    }

    void HandleInput()
    {
        move = new Vector3(movement.x, 0, movement.y);


        if (move != Vector3.zero)
        {
            //Debug.Log("*Clop*");
            //if (!movementNoise.isPlaying)
            //{
            //    movementNoise.pitch = pitchStartValue;
            //    movementNoise.Play(0);
            //    pitchTimer = 0.0f;
            //    isMoving = true;
            //}

            //if (isMoving && movementNoise.pitch < 1.05f)
            //{
            //    pitchTimer += Time.deltaTime * 0.05f;
            //    movementNoise.pitch += pitchTimer;
            //}
        }

        else
        {
            if (isMoving)
            {
                isMoving = false;
                pitchTimer = 0.0f;
            }

            if (!isMoving && movementNoise.pitch > 0.25f)
            {
                pitchTimer += Time.deltaTime * 0.4f;
                movementNoise.pitch -= pitchTimer;
            }
            else if (!isMoving && movementNoise.pitch <= 0.25)
            {
                movementNoise.Stop();
            }
        }
    }
 
    public void OnMove(InputValue value)
    {
        //if ()
        //
            //Change forward direction relative to active camera
        //
        movement = value.Get<Vector2>();
    }

    void HandleMovement()
    {
        if(storedActiveCam != null)
        {
            move = Quaternion.Euler(0, storedActiveCam.eulerAngles.y, 0) * move;
        }
        controller.Move(move * Time.deltaTime * currentSpeed);
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleRotation()
    {
        if(storedActiveCam != null)
        {
            Vector3 playerDirection = Quaternion.Euler(0, storedActiveCam.eulerAngles.y, 0) * move;
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = playerRotation;
            }
        }

    }
    //Vector3 playerDirection = Vector3.right * movement.x + Vector3.forward * movement.y;
    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        isController = playerInput.currentControlScheme.Equals("Controller") ? true : false;
    }

    public float CalculateSpeed()
    {
        currentSpeed = baseSpeed;
         
        return baseSpeed;
    }

}
