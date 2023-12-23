using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneNavi : MonoBehaviour
{
    [SerializeField]
    private FlipPhoneManager flipPhone;
    FlipPhone_BaseState flipPhoneState;

    [SerializeField]
    TextMeshProUGUI buttonText;


    // Phone controls:
    // ARROW KEYS (or point and click for now), CONFIRM, CANCEL (BACK)
    //
    // SHORTCUTS
    // "STRAIGHT TO CAMERA" - KEY SHORTCUT THAT GOES STRAIGHT TO THE CAMERA
    // "EXIT PHONE" - KEY SHORTCUT THAT PUTS AWAY PHONE/EXITS FIRST PERSON PHONE VIEW

    // BUTTONS
    // Back, Options, Main Menu

    // BACK - RETURNS TO MAIN MENU
    // OPTIONS - BRINGS UP ADDITIONAL CONTEXT OPTIONS, I.E. 'DELETE'
    // MAIN MENU - THE APPS

    // STATES
    // Home Screen, Camera, Apps (Main Menu), Gallery, Photo (Individual), Map (Fast Travel List), Settings

    // HOME SCREEN - PHONE BACKGROUND WITH TIME AND TEXT STRING 'HOME SCREEN'
    // --- Navi Buttons: "Options / Main Menu"
    // CAMERA - FOR TAKING PICS AND SHOWS THE VIEWFINDER
    // --- Navi Buttons: "Options / Back"
    // APPS (MAIN MENU) - A GRID OF THE APPS AVAILABLE ON THE PHONE (CAMERA, GALLERY, MAP)
    // --- Navi Buttons: "Options / Back"
    // GALLERY - SHOW PHOTO GRID, SCROLLS FROM TOP TO BOTTOM, NEWEST IMAGES AT TOP.
    // --- Navi Buttons: "Options / Back"
    // PHOTO (INDIVIDUAL) - WHEN THE PLAYER SELECTS A PHOTO FROM THE GALLERY AND LOOKS AT IT + THE METADATA
    // --- Navi Buttons: "Options / Back"
    // MAP (FAST TRAVEL LIST) - FOR NOW, JUST A LIST OF LOCATIONS (by string) WITH A MAP "BACKGROUND" IMAG
    // --- Navi Buttons: "Options / Back"
    // SETTINGS - ANY GAME SETTINGS OR PHONE SETTINGS ON A LIST
    // --- Navi Buttons: "Options / Save"

    // Start is called before the first frame update
    public enum whichNaviButton
    {
        Options,
        MainMenu,
        Back,
        Save
    }
    public whichNaviButton button;

    void Awake()
    {
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        flipPhoneState = flipPhone.currentState;

    }

    // Update is called once per frame
    void Update()
    {
        switch (button)
        {
            case whichNaviButton.Options:
                buttonText.text = "Options";
                break;
            case whichNaviButton.MainMenu:
                buttonText.text = "Main Menu";
                break;
            case whichNaviButton.Back:
                buttonText.text = "Back";
                break;
            case whichNaviButton.Save:
                buttonText.text = "Save";
                break;
        }
    }

    public void ClickNaviButton(FlipPhoneManager flipPhone)
    {
        switch (button)
        {
            case whichNaviButton.Options:
                Debug.Log("you clicked on 'Options'");
                AudioManager.instance.Play("Boop1");
                // if the Options context menu is already open and player hits the button again, it will be set to inactive
                if (flipPhone.options.activeSelf)
                {
                    flipPhone.options.SetActive(false);
                }
                else
                {
                    flipPhone.options.SetActive(true);
                }
                break;
            case whichNaviButton.MainMenu:
                Debug.Log("you clicked on 'Main Menu'");
                AudioManager.instance.Play("Boop1");
                flipPhone.SwitchState(flipPhone.mainMenuState);
                break;
            case whichNaviButton.Back:
                Debug.Log("you clicked on 'Back'");
                flipPhoneState = flipPhone.currentState;
                AudioManager.instance.Play("Boop2");
                Debug.Log("previous state recorded is " + flipPhone.currentState);
                if (flipPhone.appMenu.activeSelf)
                {
                    flipPhone.SwitchState(flipPhone.homeScreenState);
                    flipPhone.appMenu.SetActive(false);
                }
                else if (flipPhoneState == flipPhone.photoIndividualState)
                {
                    flipPhone.SwitchState(flipPhone.galleryState);
                }
                else
                {
                    Debug.Log(flipPhone.currentState + "; none of the conditional statements for the 'Back' button are true, returning to Main Menu");
                    flipPhone.SwitchState(flipPhone.mainMenuState);
                }


                //      { 
                //          hide options;
                //      }
                // else
                // {
                break;
            case whichNaviButton.Save:
                Debug.Log("You clicked on 'Save'");
                break;
        }
    }
}
