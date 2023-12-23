using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OptionsContextMenu;


public class FlipPhone_PhotoIndividualState : FlipPhone_BaseState
{
    public string pageBelongingToState = "PhotoIndividual";

    public override void EnterState(FlipPhoneManager flipPhone)
    {
        Debug.Log("----------You are in Photo Individual State: " + pageBelongingToState + " is active.");
        //Sets every phone page except THIS one to inactive
        IEnumerable<GameObject> objectsExceptOne = flipPhone.GetObjectsExceptOne(pageBelongingToState);
        foreach (GameObject obj in objectsExceptOne)
        {
            obj.SetActive(false);
        }

        //Reactivates the Gallery Page so information can be received from "PhotoExpandOpener.cs"
        var galleryPage = flipPhone.GetObject("GalleryPage");
        if (galleryPage != null)
        {
            galleryPage.SetActive(true);
        }
        else
        {
            Debug.LogWarning(galleryPage.name + " is null.");
        }

        ///////
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
        //////////////
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
        //When the Photo Individual page closes, we go back to the gallery and we refresh that grid.
        var galleryPage = flipPhone.GetObject("GalleryPage");
        var gridInstantiator = galleryPage.transform.GetChild(4);
        if(gridInstantiator != null)
        {
            gridInstantiator.gameObject.SetActive(false);
            gridInstantiator.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("PhotoIndividualState could not find the Gallery Page Instantiator on Child #4 of 'GalleryPage'. Please make sure this lines up.");
        }

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
