using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class PhotoStickyButton : MonoBehaviour
{
    bool selected = false;
    bool alreadySelected = false;
    public Button btn;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.grey;
    public GalleryListBehaviour targetBehaviour;
    public OptionsContextMenu optionsMenu;
    public PhotoInfo containedPhotoInfo;

    [Header("Highlighted Photo")]
    public TextMeshProUGUI picTitle_txt;
    private FlipPhoneManager flipPhone;
    // Define three different colors
    Color nothingSelected = new Color(0.75f, 0.75f, 0.75f, 1f);      // Grey
    Color importantColour = new Color(1f, 0.45f, 0f, 1f);    // Red/Orange
    Color uncategorizedColour = new Color(1f, 1f, 1f, 1f);     // White

    [Header("Expand Page")]
    public Image enlargedPhoto;
    public TextMeshProUGUI items_Txt;
    public TextMeshProUGUI location_Txt;
    public TextMeshProUGUI time_Txt;

    private ColorBlock colors;

    public void Awake()
    {
        picTitle_txt.text = "Select a photo";
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        btn = gameObject.GetComponent<Button>();
        colors = btn.colors;
        optionsMenu = FindObjectOfType<OptionsContextMenu>();
    }
    public void OnEnable()
    {
        btn.interactable = false;
    }
    public void ToggleSelected()
    {
        selected = !selected;
        if (alreadySelected)
        {
            picTitle_txt.color = nothingSelected;
            picTitle_txt.text = "Select a photo";
            // makes all buttons selectable again after clicking the already-selected button
            if(targetBehaviour != null)
            {
                targetBehaviour.ResetGameObjectBehaviour();
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
            Debug.Log("Name: " + containedPhotoInfo.photoName + ", Location: " + containedPhotoInfo.gameLocation + ", Items: " + containedPhotoInfo.photoItems);
            if (containedPhotoInfo.photoItems != null && containedPhotoInfo.photoItems.Length != 0)
            {
                // Make the text the "important colour"
                picTitle_txt.color = importantColour;

                // Initialize strings for picTitle_txt and items_Txt
                string picTitle = "";
                string itemsText = "";

                // Loop through photoItems and build the strings
                for (int i = 0; i < Mathf.Min(containedPhotoInfo.photoItems.Length, 3); i++)
                {
                    string itemString = containedPhotoInfo.GetPhotoItemString(containedPhotoInfo.photoItems[i]);

                    // Add the itemString to both picTitle and itemsText
                    picTitle += itemString + ", ";
                    itemsText += itemString + ", ";
                }

                // Trim trailing comma and space
                picTitle = picTitle.TrimEnd(',', ' ');
                itemsText = itemsText.TrimEnd(',', ' ');

                // Add "More..." if there are more than 3 items
                if (containedPhotoInfo.photoItems.Length > 3)
                {
                    picTitle += ", More...";
                    itemsText += ", More...";
                }

                // Set the text for both UI elements
                picTitle_txt.text = picTitle;
                items_Txt.text = itemsText;
            }

            else
            {
                items_Txt.color = uncategorizedColour;
                items_Txt.text = "Uncategorized";

                //make the text a different colour to distinguish that it's an unimportant photo
                picTitle_txt.color = uncategorizedColour;
                //if there are no items in the photo, change the 'Highlighted Photo' text to the name of the location it was taken in
                picTitle_txt.text = containedPhotoInfo.GetLocationString();
            }
            location_Txt.text = containedPhotoInfo.GetLocationString();
            time_Txt.text = containedPhotoInfo.photoTime.ToString();
            if (containedPhotoInfo.photoName != null)
            {
                var snapshotSavePath = "Resources/Snapshots";
                var imagePath = Path.Combine(Application.persistentDataPath, snapshotSavePath, containedPhotoInfo.photoName);
                if (File.Exists(imagePath))
                {
                    // Load the image as a Texture2D
                    byte[] fileData = File.ReadAllBytes(imagePath);
                    Texture2D loadedTexture = new Texture2D(128, 128); // Create an empty Texture2D
                    loadedTexture.LoadImage(fileData); // Load the image data into the Texture2D
                    var expandTexture2D = loadedTexture;
                    Sprite sprite = Sprite.Create(expandTexture2D, new Rect(0, 0, expandTexture2D.width, expandTexture2D.height), Vector2.one * 0.5f);
                    enlargedPhoto = enlargedPhoto.GetComponent<Image>();
                    enlargedPhoto.sprite = sprite;
                }
                else
                {
                    Debug.Log("shit not found");
                }
            }
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

            optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Photo;

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

    public void MakeInteractable()
    {
        btn.interactable = true;
    }
}