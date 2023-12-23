using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInputManager : MonoBehaviour
{
    [SerializeField] MovePlayer movement;
    [SerializeField] MouseLook cameraLook;
    DefaultControls inputActions;
    DefaultControls.PlayerActions groundMovement;


    Vector2 horizontalInput;
    Vector2 lookInput;

    //adding in values for third person on the input manager
    [Header("Third Person Values")]
    ThirdPersonControls thirdPersonControls;

    public Vector2 movementInput;
    public float inputHorizontal;
    public float inputVertical;


    private void Awake()
    {
        inputActions = new DefaultControls();
        groundMovement = inputActions.Player;
        groundMovement.Movement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        groundMovement.LookX.performed += ctx => lookInput.x = ctx.ReadValue<float>();
        groundMovement.LookY.performed += ctx => lookInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        //I COMMENTED THIS OUT SINCE IT WOULD GIVE ME AN ERROR
        //movement.ReceiveInput(horizontalInput);
        //cameraLook.ReceiveInput(lookInput);
    }
    private void OnEnable()
    {
        inputActions.Enable();

        //checking for third person
        if(thirdPersonControls == null)
        {
            thirdPersonControls = new ThirdPersonControls();

            thirdPersonControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        thirdPersonControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        thirdPersonControls.Disable();
    }

    void HandleThirdPersonInput()
    {
        inputVertical = movementInput.y;
        inputHorizontal = movementInput.x;
    }

    public void HandleAllInputs()
    {
        HandleThirdPersonInput();
        //HandleJumpInput();
        //HandleActionInput();
    }
}
