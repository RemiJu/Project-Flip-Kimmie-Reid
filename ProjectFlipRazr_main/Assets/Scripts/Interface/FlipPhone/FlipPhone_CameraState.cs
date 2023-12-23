using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static OptionsContextMenu;


public class FlipPhone_CameraState : FlipPhone_BaseState 
{
    public string pageBelongingToState = "PhoneViewfinder";

    public override void EnterState(FlipPhoneManager flipPhone)
    {
        //Sets every phone page except THIS one to inactive
        flipPhone.cameraOpen = true;
        IEnumerable<GameObject> objectsExceptOne = flipPhone.GetObjectsExceptOne(pageBelongingToState);
        foreach (GameObject obj in objectsExceptOne)
        {
            obj.SetActive(false);
        }
        ///////
        ///
        /*
        if (flipPhone.options != null)
        {
            var contextMenu = flipPhone.options.GetComponentInChildren<OptionsContextMenu>();
            if (contextMenu != null)
            {
                contextMenu.currentButtonType = ButtonType.Nothing;
            }
            else
            {
                Debug.LogError("OptionsContextMenu component not found on flipPhone.options");
            }
        }
        else
        {
            Debug.LogError("flipPhone.options is null");
        }
        */
        //////////////
        //Gets rid of the Options context menu if it's open
        flipPhone.options.SetActive(false);

        //Sets this phone page as active
        flipPhone.GetObject(pageBelongingToState).SetActive(true);

        flipPhone.NavigationButton_L(PhoneNavi.whichNaviButton.Options);
        flipPhone.NavigationButton_R(PhoneNavi.whichNaviButton.Back);
        // L Button: "Options"
        // R Button: "Back"

        //UnfreezeMovement
        GameObject.FindAnyObjectByType<PhoneSwitcher>().UnFreezeMovement();

        if (SceneManager.GetActiveScene().name == "Kimmie's House")
        {
            var dramaManager = GameObject.FindAnyObjectByType<DramaManager>();
            if (dramaManager.questProgressions[dramaManager.dramaSystem.quests[0]] == 1)
            {
                dramaManager.UpdateQuestProgression(dramaManager.dramaSystem.quests[0]);
            }
        }
    }

    public override void UpdateState(FlipPhoneManager flipPhone)
    {

    }

    public override void ExitState(FlipPhoneManager flipPhone)
    {
        GameObject.FindAnyObjectByType<PhoneSwitcher>().FreezeMovement();
        flipPhone.cameraOpen = false;
    }
}
