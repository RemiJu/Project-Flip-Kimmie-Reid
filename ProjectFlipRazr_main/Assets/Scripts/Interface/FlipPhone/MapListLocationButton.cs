using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MapListLocationButton : MonoBehaviour
{
    private Button btn;
    public MapList targetBehaviour;
    private OptionsContextMenu optionsMenu;
    public string correspondingScene;

    private FlipPhoneManager flipPhone;

    public void Awake()
    {
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        btn = gameObject.GetComponent<Button>();
        optionsMenu = FindObjectOfType<OptionsContextMenu>();
    }

    
    public void OnButtonSelected()
    {
        optionsMenu.selectedLocation = correspondingScene;
        optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Location;
        //optionsMenu.SetButtonsVisibility();
        //flipPhone.options.SetActive(!flipPhone.options.activeSelf);
        //flipPhone.options.SetActive(true);
    }


}