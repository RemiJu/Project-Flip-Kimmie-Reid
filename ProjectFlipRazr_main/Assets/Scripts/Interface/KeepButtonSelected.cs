using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeepButtonSelection : MonoBehaviour
{

    //This script can be attached somewhere in a UI element to make sure that buttons set up in a Navigation group, i.e. Grid Layout Group -- do not lose focus when the player clicks outside of them.
    //It will automatically select the 'lastSelectedButton' (which it finds on its own in void Update) when a button is deselected
    public Button defaultButton;

    private GameObject lastSelectedButton;
    [SerializeField] private GameObject layoutParent;
    [Header("Automatic modes")]
    public bool isGallery;
    public bool isMap;

    void OnEnable()
    {
        // Set the default button as selected initially
        if (isGallery)
        {
            layoutParent = GameObject.Find("PhotoGrid");
            if(layoutParent.transform.childCount > 0) // childCount starts at 1 for the first object...
            {
                defaultButton = layoutParent.transform.GetChild(0).GetComponent<Button>(); // GetChild starts at 0. I hate it.
                if (defaultButton != null)
                {
                    SetSelectedButton(defaultButton.gameObject);
                }
                else
                {
                    Debug.Log("KeepButtonSelection/isGallery: 'defaultButton' is null. This might just mean no photos have been taken.");
                }
            }

        }
        if(isMap)
        {
            layoutParent = GameObject.Find("MapList");
            
            if(layoutParent.transform.childCount > 0)
            {
                var defaultButtonToPassIn = layoutParent.GetComponent<MapList>().FindFirstActiveChildAfterIndex(layoutParent, 1).GetComponent<Button>();
                defaultButton = defaultButtonToPassIn;
                //defaultButton = layoutParent.transform.GetChild(1).GetComponent<Button>(); //this is ONE and not 0 because the Map List begins with 'Current Location' which isn't a button. It's just a display.
                if(defaultButton != null)
                {
                    SetSelectedButton(defaultButton.gameObject);
                }
                else
                {
                    Debug.Log("KeepButtonSelection/isMap: 'defaultButton' is null. Check Global Settings' 'Locations' boolean values.");
                }
            }
        }

    }

    private void OnDisable()
    {
        if (isGallery)
        {
            defaultButton = null;
            lastSelectedButton = null;
        }

        if(isMap)
        {
            defaultButton = null;
            lastSelectedButton = null;
        }
    }
    void Update()
    {
        // Check if no button is currently selected
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            // Re-select the last selected button
            SetSelectedButton(lastSelectedButton);
        }
        else
        {
            // Update the last selected button
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        }
    }

    void SetSelectedButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }
}
