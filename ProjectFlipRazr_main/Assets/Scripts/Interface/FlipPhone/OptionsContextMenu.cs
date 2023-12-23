using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionsContextMenu : MonoBehaviour
{
    public enum ButtonType
    {
        Nothing,
        Location,
        Photo,
        Settings,
        SaveQuit,
        AppMenu,
        HomeScreen,
    }

    public ButtonType currentButtonType; // Don't touch this, this is getting communicated from StickyButtons' code

    // Array to store all child buttons
    public Button[] buttons;

    public int activeButtonCount; // Adjust this value based on your requirements
    public string selectedLocation; // don't touch this, determined automatically by MapList's ListTargetBehaviour's currently selected

    public HomeScreenBehaviour HomeListTargetBehaviour;
    public AppList AppListBehaviour;
    public MapList MapListTargetBehaviour;
    public GalleryListBehaviour PhotoListTargetBehaviour;
    public ListTargetBehaviour SettingsListTargetBehaviour;
    public ListTargetBehaviour SaveQuitListTargetBehaviour;

    public void Awake()
    {
        //CollectButtons();
    }
    public void OnEnable()
    {
        CollectButtons(); // this is collecting the four OptionsButton gameobjects as part of the context menu, from 1-4 (as designed)
        SetButtonsVisibility();
        foreach (Button button in buttons)
        {
            button.GetComponent<OptionButtonBehaviour>().SetText();
            //Debug.Log("Set Text on button " + button.name);

        }

    }

    public void OnDisable()
    {
        //When the player closes the phone, this will reset the Options Button Menu to "Nothing" so options don't linger (i.e. 'Delete')
        currentButtonType = ButtonType.Nothing;
        SetButtonsVisibility();
    }

    void CollectButtons()
    {
        // Assuming buttons are direct children of this GameObject
        // Array.Clear(buttons, 0, buttons.Length);
        buttons = GetComponentsInChildren<Button>();
        


    }

    // Update is called once per frame
    public void SetButtonsVisibility()
    {



        // Set all buttons inactive initially
        //foreach (Button button in buttons)
        //{
        //    button.gameObject.SetActive(false);
        //}


        // Activate a certain quantity of buttons based on the currentButtonType
        switch (currentButtonType)
        {
            case ButtonType.Nothing:
                Debug.Log("Nothing is selected");
                activeButtonCount = 0;
                ActivateButtons(0, 0);
                if (activeButtonCount == 0)
                {
                    var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Null;
                    thisButton = buttons[1].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Null;
                    thisButton = buttons[2].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Null;
                    thisButton = buttons[3].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Null;
                }

                break;


            case ButtonType.Location:
                Debug.Log("Successfully picking up that this should show options relevant to a Location.");
                if (MapListTargetBehaviour != null && MapListTargetBehaviour.currentlySelected != null)
                {
                    activeButtonCount = 1;
                    ActivateButtons(0, activeButtonCount);
                    var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Travel;
                    selectedLocation = MapListTargetBehaviour.currentlySelected;
                    Debug.Log("You are viewing the button to travel to " + selectedLocation);
                }
                //if (LocationListTargetBehaviour != null && LocationListTargetBehaviour.currentlySelected != null)
                //{
                //    activeButtonCount = 1;
                //    ActivateButtons(0, activeButtonCount);
                //    var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                //    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Travel;
                //    selectedLocation = LocationListTargetBehaviour.currentlySelected; // this is how the Options Menu will pick up where the 'Travel' button sends the player
                //    Debug.Log("You are viewing the button to travel to " +  selectedLocation);
                //}
                break;




            case ButtonType.Photo:
                Debug.Log("Successfully picking up that this should show options relevant to a Photo.");
                if (PhotoListTargetBehaviour != null && PhotoListTargetBehaviour.currentlySelected != null)
                {
                    activeButtonCount = 2;
                    ActivateButtons(0, activeButtonCount);
                    var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Expand;
                    thisButton = buttons[1].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.Delete;
                }
                break;



            case ButtonType.Settings:
                Debug.Log("Successfully picking up that this should show options relevant to a Setting.");
                if (SettingsListTargetBehaviour != null && SettingsListTargetBehaviour.currentlySelected != null)
                {
                    activeButtonCount = 1;
                    ActivateButtons(0, activeButtonCount);
                    var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.EnableDebug;
                }

                //get settingID from ListTargetBehaviour's 'currentlySelected' and put it as the activeButtonCount
                // if(targetBehaviour.currentlySelected = "EnableDebug")
                // { Find "GlobalPlayTestSettings.cs"
                // SettingID = 1;
                //
                // GlobalPlayTestSettings.AssignSettingChange(SettingID);
                // }
                // within GlobalPlayTestSettings.cs:
                // public void AssignSettingChange(int SettingID)
                // {
                //  if (SettingID == 1)
                // {
                //      string TargetFunctionName = "EnableDebug";
                // }
                //
                // back here in OptionsContextMenu.cs:
                //
                //     string FunctionToRunOnClick = GlobalPlayTestSettings.TargetFunctionName
                //     buttons[0].FunctionToRunOnClick(FunctionToRunOnClick);
                // 
                break;


            case ButtonType.SaveQuit:
                Debug.Log("Successfully picking up that this should show options relevant to a SaveQuit button.");
                if (SaveQuitListTargetBehaviour != null && SaveQuitListTargetBehaviour.currentlySelected != null)
                {
                    ActivateButtons(1, activeButtonCount);

                    if (SaveQuitListTargetBehaviour.currentlySelected == "SaveQuitButton")
                    {
                        var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                        thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.SaveAndQuit;
                    }
                    else if(SaveQuitListTargetBehaviour.currentlySelected == "ExitGameButton")
                    {
                        var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                        thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.ExitWithoutSaving;
                    }

                }
                break;



            case ButtonType.HomeScreen:
                Debug.Log("Successfully picking up that this should show options relevant to the Home Screen.");
                
                activeButtonCount = HomeListTargetBehaviour.homeScreenOptions.Length;
                ActivateButtons(0, activeButtonCount);
                if(HomeListTargetBehaviour != null)
                {
                    var thisButton = buttons[0].gameObject.GetComponent<OptionButtonBehaviour>();
                    thisButton.buttonfunction = OptionButtonBehaviour.buttonFunction.ChangeWallpaper;
                }
                break;



            default:
                Debug.LogError("Invalid ButtonType");
                break;
        }
    }

    void ActivateButtons(int startIndex, int count)
    {

        return;
    }


}
