using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoExpandOpener : MonoBehaviour
{
    [Header("Highlighted Photo")]
    public TextMeshProUGUI picTitle_txt;
    private FlipPhoneManager flipPhone;
    // Define three different colors
    Color nothingSelected = new Color(0.75f, 0.75f, 0.75f, 1f);      // Grey
    Color importantColour = new Color(1f, 0.45f, 0f, 1f);    // Red/Orange
    Color uncategorizedColour = new Color(1f, 1f, 1f, 1f);     // White
    // Use this for initialization

    [Header("Currently Selected Strings")]

    public string selPhotoName;

    public string selPhotoTitle;
    public string selPhotoTime;
    public string selPhotoLocation;
    public string selPhotoItems;

    private void OnEnable()
    {
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        picTitle_txt.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        picTitle_txt.text = selPhotoTitle;
    }

    public void OpenPhotoExpanded()
    {
        flipPhone.SwitchState(flipPhone.photoIndividualState);

    }
}