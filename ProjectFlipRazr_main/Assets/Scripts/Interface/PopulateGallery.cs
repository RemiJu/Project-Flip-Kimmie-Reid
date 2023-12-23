using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine.EventSystems;

public class PopulateGallery : MonoBehaviour
{
    public enum WhichPath
    {
        Editor,
        Build
    }
    public Transform gridParent; // UI Grid parent
    public string resourcePath; // Folder containing PNGs
    public string persistentResourcePath; // Folder containing PNGs (in build version)
    public GameObject imagePrefab; // Prefab for displaying sprites
    public TextureHolder textureHolder;
    public PhotoInfo containedPhotoInfo;
    public PhotoInfoDatabase photoDatabase;
    public GalleryListBehaviour galleryListBehaviour;
    public WhichPath whichPath;
    public string imagePath;
    public string snapshotSavePath;

    private GameObject newestPhoto;



    public void Awake()
    {
        textureHolder = FindAnyObjectByType<TextureHolder>();
        photoDatabase = FindAnyObjectByType<PhotoInfoDatabase>();
        galleryListBehaviour = FindAnyObjectByType<GalleryListBehaviour>();
        ShowImagesInAlbum();
        switch (whichPath)
        {
            case WhichPath.Editor:

                textureHolder.textures = Resources.LoadAll<Texture2D>(resourcePath);

                break;


            case WhichPath.Build:

                break;
        }


    }

    // Since at the moment I cannot click on buttons, I added these as temporary ways to change pages.
    private void Update()
    {

    }

    public void ShowImagesInAlbum()
    {

        if (whichPath == WhichPath.Editor)
            textureHolder.textures = Resources.LoadAll<Texture2D>(resourcePath);


        int startIdx = 0; // this is the first photo in the list of textures from 'textureHolder'.
        int endIdx = textureHolder.textures.Length - 1; // This is total number of photos taken

        for (int i = endIdx; i >= startIdx; i--)
        
        {
            Debug.Log(i);
            Sprite sprite = Sprite.Create(textureHolder.textures[i], new Rect(0, 0, textureHolder.textures[i].width, textureHolder.textures[i].height), Vector2.one * 0.5f);

            // Create UI Image object and add it to the grid
            GameObject imageObject = Instantiate(imagePrefab, gridParent);
            Image image = imageObject.GetComponent<Image>();
            image.sprite = sprite;

            if(i == endIdx)
            {
                newestPhoto = imageObject;
                Debug.Log("PopulateGallery.cs: " + i + ": NewestPhoto is " + photoDatabase.photos[i].photoName + "/" + imageObject.name);
            }

            imageObject.GetComponent<PhotoInfoContainer>().photoName = photoDatabase.photos[i].photoName;
            
            // Create a list to store the item strings
            List<string> itemStrings = new List<string>();

            // Iterate over the photoItems array and get the corresponding strings
            foreach (PhotoInfo.PhotoItem item in photoDatabase.photos[i].photoItems)
            {
                string itemString = photoDatabase.photos[i].GetPhotoItemString(item);
                itemStrings.Add(itemString);
            }
            string concatenatedItems = string.Join(", ", itemStrings);

            if (photoDatabase.photos[i].photoItems.Length >= 3)
            {
                concatenatedItems += ", More...";
            }

            imageObject.GetComponent<PhotoInfoContainer>().photoItems = concatenatedItems;
            imageObject.GetComponent<PhotoInfoContainer>().photoLocation = photoDatabase.photos[i].GetLocationString();
            imageObject.GetComponent<PhotoInfoContainer>().photoTime = photoDatabase.photos[i].photoTime.ToString();
        }

        //ASSIGN THE FIRST SELECTABLE PHOTO TO THE EVENT SYSTEM
        if(newestPhoto != null)
        {
            EventSystem.current.SetSelectedGameObject(newestPhoto);
            Debug.Log(newestPhoto.gameObject.name + " is becoming the first selectable through the " + EventSystem.current);
        }
        //TODO: 
        //You are to make a new version of 'PhotoStickyButton.cs' but it doesn't need to be sticky, it just needs to open the Options Context Menu and bring the focus over there.
        //The back button should ideally close the OptionsContextMenu and then have the photo in the gallery that was selected... still selected. If that's too hard, try to make it
        //so at the very least the gallery reloads.
    }
}
