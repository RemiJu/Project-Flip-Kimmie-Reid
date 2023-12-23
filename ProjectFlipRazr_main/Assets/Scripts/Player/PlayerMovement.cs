using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    MoveInputManager moveInputManager;

    Vector3 moveDirection;

    public Transform cameraObject;
    public Transform playerObject;

    Rigidbody playerRigid;

    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    public CharacterController characterController;

    public void Awake()
    {
        moveInputManager = GetComponent<MoveInputManager>();
        playerRigid = GetComponent<Rigidbody>();
        //cameraObject = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
    }
    private void ControlMovement()
    {
        moveDirection = transform.forward * Input.GetAxis("Vertical") * movementSpeed;

        characterController.Move(moveDirection * Time.deltaTime - Vector3.up * 0.1f);

        /*moveDirection = cameraObject.forward * moveInputManager.inputVertical;
        //moveDirection = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * moveInputManager.inputVertical;

        moveDirection = moveDirection + cameraObject.right * moveInputManager.inputHorizontal;
        //moveDirection = new Vector3(cameraObject.right.x, 0f, cameraObject.right.z) * moveInputManager.inputHorizontal;

        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigid.velocity = movementVelocity;*/
    }

    private void ControlRotation()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
       // transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);

        /*Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * moveInputManager.inputVertical;
        targetDirection = targetDirection + cameraObject.right * moveInputManager.inputHorizontal;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;*/
    }

    public void HandleAllMovement()
    {
        ControlMovement();
        ControlRotation();
    }
}