using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class StickyButton : MonoBehaviour
{
    bool selected = false;
    bool alreadySelected = false;
    public Button btn;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.grey;
    public ListTargetBehaviour targetBehaviour;
    public OptionsContextMenu optionsMenu;
    public PhotoInfo containedPhotoInfo;

    private FlipPhoneManager flipPhone;



    private ColorBlock colors;

    public enum whatTypeOfListTarget
    {
        Location,
        Photo,
        Settings,
        SaveQuit
    }
    public whatTypeOfListTarget optionType;

    public void Awake()
    {
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        btn = gameObject.GetComponent<Button>();
        colors = btn.colors;
        optionsMenu = FindObjectOfType<OptionsContextMenu>();
    }

    public void ToggleSelected()
    {
        selected = !selected;
        if (alreadySelected)
        {
            // makes all buttons selectable again after clicking the already-selected button
            if(targetBehaviour != null)
            {
                targetBehaviour.ResetButtonBehaviour();
                targetBehaviour.currentlySelected = null;
            }
            else
            {
                Debug.Log("targetBehaviour is null");
            }

            optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Nothing;

            OptionScreenToggle();

        }

        if (selected)
        {
            //var colors = btn.colors;
            colors.normalColor = selectedColor;
            colors.selectedColor = selectedColor;
            btn.colors = colors;

            //Target 'Options' script and give it information regarding this button

            string currentlySelected = gameObject.name;
            if(targetBehaviour != null)
            {
                targetBehaviour.currentlySelected = currentlySelected;
                targetBehaviour.DeselectAllButOne(currentlySelected);
            }
            else
            {
                Debug.Log("targetBehaviour is null");
            }
            alreadySelected = true;
            switch (optionType)
            {
                case whatTypeOfListTarget.Location:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Location;

                    OptionScreenToggle();

                    //feed location string from this button
                    break;
                case whatTypeOfListTarget.Photo:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Photo;

                    OptionScreenToggle();
                    //OptionsContextMenu.cs - enable Photo choices
                    //feed photo name string/identifier
                    break;
                case whatTypeOfListTarget.Settings:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Settings;

                    OptionScreenToggle();
                    //if(correspondingSettingID != null)
                    //  tell OptionsContextMenu.cs what setting ID this list item is supposed to be from GlobalPlaytestSettings.cs script
                    //  in MasterSettings script define what options should appear/what they do for each setting ID
                    break;
                case whatTypeOfListTarget.SaveQuit:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.SaveQuit;

                    OptionScreenToggle();
                    //options menu enable SaveQuit choices
                    //
                    break;
            }

        }
        else
        {
            //var colors = btn.colors;
            colors.normalColor = normalColor;
            colors.selectedColor = normalColor;
            btn.colors = colors;
        }
    }
    
    void OptionScreenToggle()
    {
        optionsMenu.SetButtonsVisibility();
        flipPhone.options.SetActive(false);
        //flipPhone.options.SetActive(true);
    }

}