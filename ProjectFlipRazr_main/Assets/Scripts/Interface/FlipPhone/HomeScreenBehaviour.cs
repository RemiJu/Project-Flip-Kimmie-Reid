using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenBehaviour : MonoBehaviour
{
    public string[] homeScreenOptions; //create the names of Options that will be available on the home screen
    public OptionsContextMenu optionsMenu;
    //comtents
    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.HomeScreen;
    }

    private void OnEnable()
    {
        optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.HomeScreen;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
