using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OptionsContextMenu;


public class FlipPhone_GalleryState : FlipPhone_BaseState
{
    public string pageBelongingToState = "GalleryPage";

    public override void EnterState(FlipPhoneManager flipPhone)
    {
        Debug.Log("----------You are in Gallery State: " + pageBelongingToState + " is active.");
        //Sets every phone page except THIS one to inactive
        IEnumerable<GameObject> objectsExceptOne = flipPhone.GetObjectsExceptOne(pageBelongingToState);
        foreach (GameObject obj in objectsExceptOne)
        {
            obj.SetActive(false);
        }
        //Gets rid of the Options context menu if it's open
        flipPhone.options.SetActive(false);

        //Sets this phone page as active
        flipPhone.GetObject(pageBelongingToState).SetActive(true);
        flipPhone.NavigationButton_L(PhoneNavi.whichNaviButton.Options);
        flipPhone.NavigationButton_R(PhoneNavi.whichNaviButton.Back);
        // L Button: "Options"
        // R Button: "Back"
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
        }*/
    }
}
