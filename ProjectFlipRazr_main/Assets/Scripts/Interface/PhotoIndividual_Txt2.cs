using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class PhotoIndividual_Txt2 : MonoBehaviour
{
    public enum whichDescriptor
    {
        Title,
        Items,
        Location,
        Date,
        Filename,
        Image,
    }
    public whichDescriptor descriptorProperty;
    public TextMeshProUGUI targetText;
    private Image enlargedImage;
    // Define three different colors
    Color nothingSelected = new Color(0.75f, 0.75f, 0.75f, 1f);      // Grey
    Color importantColour = new Color(1f, 0.45f, 0f, 1f);    // Red/Orange
    Color uncategorizedColour = new Color(1f, 1f, 1f, 1f);     // White

    // Start is called before the first frame update
    void OnEnable()
    {
        var selPhotoProperties = FindAnyObjectByType<PhotoExpandOpener>();
        enlargedImage = GetComponent<Image>();
        switch (descriptorProperty)
        {
            case whichDescriptor.Title:
                if(selPhotoProperties.selPhotoTitle != null) targetText.text = selPhotoProperties.selPhotoTitle;
                break;
            case whichDescriptor.Items:
                if(selPhotoProperties.selPhotoItems != null) targetText.text = selPhotoProperties.selPhotoItems;
                break;
            case whichDescriptor.Location:
                if(selPhotoProperties.selPhotoLocation != null) targetText.text = selPhotoProperties.selPhotoLocation;
                break;
            case whichDescriptor.Date:
                if (selPhotoProperties.selPhotoTime != null) targetText.text = selPhotoProperties.selPhotoTime;
                break;
            case whichDescriptor.Filename:
                if (selPhotoProperties.selPhotoName != null) targetText.text = selPhotoProperties.selPhotoName;
                break;
            case whichDescriptor.Image:
                if (selPhotoProperties.selPhotoName != null)
                {
                    var snapshotSavePath = "Resources/Snapshots";
                    var imagePath = Path.Combine(Application.persistentDataPath, snapshotSavePath, selPhotoProperties.selPhotoName);
                    if (File.Exists(imagePath))
                    {
                        // Load the image as a Texture2D
                        byte[] fileData = File.ReadAllBytes(imagePath);
                        Texture2D loadedTexture = new Texture2D(128, 128); // Create an empty Texture2D
                        loadedTexture.LoadImage(fileData); // Load the image data into the Texture2D
                        var expandTexture2D = loadedTexture;
                        Sprite sprite = Sprite.Create(expandTexture2D, new Rect(0, 0, expandTexture2D.width, expandTexture2D.height), Vector2.one * 0.5f);
                        enlargedImage = enlargedImage.GetComponent<Image>();
                        enlargedImage.sprite = sprite;
                        Debug.Log(selPhotoProperties.selPhotoName + " is the image being displayed by PhotoIndividual_Txt2.cs");
                    }
                    else
                    {
                        Debug.Log("shit not found");
                    }
                }
                break;
        }
    }

    private void OnDisable()
    {
        if (targetText != null) targetText.text = "";
        if (enlargedImage != null) enlargedImage.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
