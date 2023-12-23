using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 movement;
    private CharacterController controller;
    private Gamepad gamepad;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gamepad = Gamepad.current;
    }

    void Update()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Use gamepad joystick if connected
        if (gamepad != null)
        {
            moveHorizontal = gamepad.leftStick.x.ReadValue();
            moveVertical = gamepad.leftStick.y.ReadValue();
        }

        // Use keyboard keys as alternative
        moveHorizontal += Input.GetAxis("Horizontal");
        moveVertical += Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        controller.Move(movement * speed * Time.deltaTime);
    }
}