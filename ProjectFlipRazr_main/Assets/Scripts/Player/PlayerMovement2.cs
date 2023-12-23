using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    ThirdPersonControls moveInputManager;
    public CinemachineBrain cinemachineBrain;
    public Animator animator;

    [Header("Transform References")]
    Vector3 moveDirection;
    public Transform playerObject;
    public Transform activeCameraObject;

    [Header("Movement Parameters")]
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    public CharacterController characterController;

    private void Awake()
    {
        moveInputManager = new ThirdPersonControls();
        moveInputManager.Enable();
        cinemachineBrain = cinemachineBrain.GetComponent<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            CinemachineVirtualCameraBase virtualCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;

            if (virtualCamera != null)
            {
                activeCameraObject = virtualCamera.transform;
            }
        }
        characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        // Get the active virtual camera

        if (cinemachineBrain != null)
        {
            // Get the live camera's transform
            CinemachineVirtualCameraBase virtualCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;

            if (virtualCamera != null)
            {
                activeCameraObject = virtualCamera.transform;

            }
        }

     }
  
    private void OnEnable()
    {
        moveInputManager.Enable();
        // Subscribe to the events for movement controls
        moveInputManager.PlayerMovement.Movement.started += OnMovementStarted;
            moveInputManager.PlayerMovement.Movement.performed += OnMovementPerformed;
            moveInputManager.PlayerMovement.Movement.canceled += OnMovementCanceled;
        animator.SetBool(2, true);
        UpdateForwardDirection(GetActiveCameraTransform());
    }

    private void OnDisable()
    {
        moveInputManager.Disable();
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        UpdateForwardDirection(GetActiveCameraTransform());
        animator.SetBool(1, true);
        animator.SetBool(2, false);
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {

    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // Movement controls are released
        // Call the function to update forward direction
        Debug.Log("Movement controls are released.");
        animator.SetBool(1, false);
        animator.SetBool(2, true);
    }

    private void ControlMovement()
    {
        Vector2 inputVector = moveInputManager.PlayerMovement.Movement.ReadValue<Vector2>();

        Vector3 viewRelativeInputVector = new Vector3(inputVector.x, 0, inputVector.y);
        viewRelativeInputVector = currentViewFrame.InverseTransformDirection(viewRelativeInputVector);

        moveDirection =  viewRelativeInputVector * movementSpeed;

        characterController.Move(moveDirection * Time.deltaTime - Vector3.up * 0.1f);
    }

    private void ControlRotation()
    {
        Vector2 inputVector = moveInputManager.PlayerMovement.Movement.ReadValue<Vector2>();
        playerObject.Rotate(Vector3.up, inputVector.x * rotationSpeed * Time.deltaTime);
    }

    public void HandleAllMovement()
    {
        ControlMovement();
        ControlRotation();
    }

    Transform currentViewFrame;
    public void UpdateForwardDirection(Transform newViewFrame)
    {
        currentViewFrame = newViewFrame;

        //playerObject.forward = newViewFrame.forward;

        Debug.Log("UpdateForwardDirection" + " newForward.forward is " + newViewFrame.forward);
    }

    private Transform GetActiveCameraTransform()
    {
        // Get the active Cinemachine Virtual Camera's transform
        return activeCameraObject.transform;
    }
}
