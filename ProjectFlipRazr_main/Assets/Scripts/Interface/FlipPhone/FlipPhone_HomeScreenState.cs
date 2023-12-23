using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OptionsContextMenu;

public class FlipPhone_HomeScreenState : FlipPhone_BaseState
{
    public string pageBelongingToState = "Home";

    public override void EnterState(FlipPhoneManager flipPhone)
    {
        //Sets every phone page except THIS one to inactive
        Debug.Log("----------You are in Home Screen State: " + pageBelongingToState + " is active.");
        IEnumerable<GameObject> objectsExceptOne = flipPhone.GetObjectsExceptOne(pageBelongingToState);
        foreach (GameObject obj in objectsExceptOne)
        {
            Debug.Log(obj.name + " is being hidden");
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
        flipPhone.NavigationButton_R(PhoneNavi.whichNaviButton.MainMenu);

        // L Button: Options
        // R Button: Main Menu
    }

    public override void UpdateState(FlipPhoneManager flipPhone)
    {

    }

    public override void ExitState(FlipPhoneManager flipPhone)
    {
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
    }
}