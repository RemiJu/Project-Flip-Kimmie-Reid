using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PhoneNaviInputSystem : MonoBehaviour
{

    public Button uiButton; // Assign this in the Inspector
    public InputActionReference buttonPressAction; // Assign your input action here

    private void OnEnable()
    {
        buttonPressAction.action.Enable();
        buttonPressAction.action.performed += OnButtonPress;
    }

    private void OnDisable()
    {
        buttonPressAction.action.performed -= OnButtonPress;
        buttonPressAction.action.Disable();
    }

    private void OnButtonPress(InputAction.CallbackContext context)
    {
        uiButton.onClick.Invoke();
    }
}
