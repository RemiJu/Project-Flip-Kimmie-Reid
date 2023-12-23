using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppButton : MonoBehaviour
{
    public FlipPhoneManager flipPhone;
    FlipPhone_BaseState flipPhoneState;
    public string appStateToRun; // make sure it's the naming convention of 'mainMenuState', 'galleryState', etc.
    public TextMeshProUGUI appName_txt;

    // Start is called before the first frame update

    void Awake()
    {
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        flipPhoneState = flipPhone.currentState;
        appName_txt.text = appStateToRun;

        // Attach the OnClick listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        flipPhone.ChangeStates(appStateToRun, flipPhone);
        AudioManager.instance.Play("Boop3");

    }
}
