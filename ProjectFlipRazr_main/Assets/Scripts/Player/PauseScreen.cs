using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    //First Person
    private InputSystemFirstPersonControls firstPersonInput;
    private InputAction firstPersonAction;

    //Third Person
    private InputActionAsset thirdPersonControlManager;
    private PlayerInput thirdPersonPlayerInput;
    private InputAction thirdPersonAction;

   [SerializeField] private GameObject pauseUI;
    public bool isPaused;

    // Start is called before the first frame update
    void Awake()
    {
        //Retrieve First Person Control Script component
        firstPersonInput = new InputSystemFirstPersonControls();
        // retrieve Player Input component for third person mode
        thirdPersonPlayerInput = GetComponent<PlayerInput>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //FIRST PERSON  
        firstPersonAction = firstPersonInput.FPSController.PauseMenu;

        //THIRD PERSON
        // retrieve the Action Map from the InputActionAsset assigned to the PlayerInput component
        thirdPersonControlManager = thirdPersonPlayerInput.actions;
        // retrieve the 'PauseToggle' action from the Third Person Control scheme/InputSystem Action Map Asset
        thirdPersonAction = thirdPersonControlManager.FindAction("PauseToggle", false);

        //"Subscribes" this script to listen for the desired firstPersonAction, to then run the corresponding function.
        firstPersonAction.performed += Pause;
        //Subscribing the script to the third person equivalent
        thirdPersonAction.performed += Pause;


    }

    private void OnDisable()
    {
        
    }

    void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0; // freeze all the shit
        AudioListener.pause = true; // pause and silence all the shit
        //Cursor.lockState = CursorLockMode.None; for when the final controls are figured out
        pauseUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseUI.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked; for when the final controls are figured out
        isPaused = false;
    }
}
