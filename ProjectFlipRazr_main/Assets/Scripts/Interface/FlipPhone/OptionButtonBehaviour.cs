using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButtonBehaviour : MonoBehaviour
{
    private TextMeshProUGUI displayedText;
    public OptionsContextMenu contextMenu;
    private GameObject ListBehaviourSelected;
    private PhotoInfo selectedPhotoInfo;
    public PhotoInfoDatabase photoInfoDatabase;
    public FlipPhoneManager flipPhone;

    //Possible buttons
    public enum buttonFunction
    {

        Null,

        //Location
        Travel,

        //Camera

        //Gallery
        Expand,
        Delete,

        //Settings
        EnableDebug,

        // SaveQuit
        SaveAndQuit,
        ExitWithoutSaving,

        //Home Screen
        ChangeWallpaper,

    }
    // public OptionsContextMenu contextMenu;
    public buttonFunction buttonfunction;

    // Start is called before the first frame update
    public void Awake()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }
    public void OnEnable()
    {
        contextMenu = FindObjectOfType<OptionsContextMenu>();
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        displayedText = GetComponentInChildren<TextMeshProUGUI>();
 
        SetText();
    }

    public void OnDisable()
    {
            displayedText = null;
    }

    public void OnClick()
    {
        switch (buttonfunction)
        {
            case buttonFunction.Null:
                break;

            //Location
            case buttonFunction.Travel:
                string selectedLocation = contextMenu.selectedLocation; 
                if (string.IsNullOrEmpty(selectedLocation))
                {
                    Debug.LogWarning("contextMenu.selectedLocation is undefined");
                    return;
                }
                else
                {
                    FindAnyObjectByType<SceneChanger>().TravelToScene(selectedLocation);
                }
                break;

            //Photo
            case buttonFunction.Expand:
                ListBehaviourSelected = GameObject.Find(contextMenu.PhotoListTargetBehaviour.currentlySelected);
                selectedPhotoInfo = ListBehaviourSelected.GetComponent<PhotoStickyButton>().containedPhotoInfo;
                Debug.Log(selectedPhotoInfo.photoName + " " + selectedPhotoInfo.gameLocation);
                for (int i = 0; i < selectedPhotoInfo.photoItems.Length; i++)
                {
                    Debug.Log(selectedPhotoInfo.photoItems[i]);
                }
                flipPhone.SwitchState(flipPhone.photoIndividualState);
                
                //Insert FlipPhoneStateManager reference, change phone state to FlipPhone_PhotoIndividualState
                //      show selected gallery image but bigger + with metadata displayed in text for the player to read
                break;

            case buttonFunction.Delete:
                ListBehaviourSelected = GameObject.Find(contextMenu.PhotoListTargetBehaviour.currentlySelected);
                selectedPhotoInfo = ListBehaviourSelected.GetComponent<PhotoStickyButton>().containedPhotoInfo;
                photoInfoDatabase.photos.Remove(selectedPhotoInfo);
                FindAnyObjectByType<TextureHolder>().RefreshGallery();
                FindAnyObjectByType<PopulateGallery>().ShowImagesInAlbum();
                FindAnyObjectByType<PuzzleManager>().CheckPhotoItemInHand();
                //if (PhotoInfoDatabase.photos contains selectedPhotoInfo) {
                // append selectedPhotoInfo from List PhotoInfoDatabase
                //}
                

                //Insert FlipPhoneStateManager reference, change phone state to FlipPhone_PhotoIndividualState
                //      show selected gallery image but bigger + with metadata displayed in text for the player to read,
                //      overlayed with "Delete pic? Yes/No" pop-up, then handle accordingly in a DeletePhoto function
                //      which should likely reside in a script that can manage PhotoInfoDatabase.
                break;

            //Settings
            case buttonFunction.EnableDebug:
                SceneManager.LoadScene("Title");
                break;

            //SaveQuit
            case buttonFunction.SaveAndQuit:
                Debug.Log("Saved and Quit.");
                photoInfoDatabase.Save();
                //Insert code to actually write the images to the HDD NOW (when that functionality exists)
                SceneManager.LoadScene("Title");
                //Run Toma's PhotoInfo saving and return to Title Screen using SceneManager reference
                //      SceneManager.TravelToScene(string sceneName);
                break;
            case buttonFunction.ExitWithoutSaving:
                photoInfoDatabase.RemoveAllPhoto();
                Invoke("InvokeApplicationQuit", 1.0f);
                //Close game application without doing anything
                // WARNING: We will likely need to create a middleman in the game to hold onto the images before writing them to the disk during 'SaveAndQuit'
                break;

            //HomeScreen
            case buttonFunction.ChangeWallpaper:
                //a whole buncha shit to change wallpaper (TBD)
                break;
        }

    }

    public void SetText()
    {
        displayedText = GetComponentInChildren<TextMeshProUGUI>();
        Button button = GetComponent<Button>();
        switch (buttonfunction)
        {

            case buttonFunction.Null:
                displayedText.text = "";
                // this is where you set the button's hoverability to be inactive
                break;

            //Location
            case buttonFunction.Travel:
                displayedText.text = "Travel";
                break;

            //Photo
            case buttonFunction.Expand:
                displayedText.text = "Expand";
                break;

            case buttonFunction.Delete:
                displayedText.text = "Delete";
                break;

            //Settings
            case buttonFunction.EnableDebug:
                displayedText.text = "Return to Title";
                break;

            //SaveQuit
            case buttonFunction.SaveAndQuit:
                displayedText.text = "Confirm";
                break;
            case buttonFunction.ExitWithoutSaving:
                displayedText.text = "Exit Game";
                break;

            //HomeScreen
            case buttonFunction.ChangeWallpaper:
                var homeScreen = FindObjectOfType<HomeScreenBehaviour>();
                if(homeScreen != null)
                {
                    homeScreen.GetComponent<HomeScreenBehaviour>();
                    string stringToDisplay = homeScreen.homeScreenOptions[0];
                    displayedText.text = stringToDisplay;
                }
                else
                {
                    Debug.LogWarning("homeScreenis NULLLLLLL");
                }
                break;
        }
    }

    public void InvokeApplicationQuit()
    {
        //Make sure all the shit you want done is done before this happens <3
        FlipPhone_SaveQuitState.QuitGame();
    }
}
