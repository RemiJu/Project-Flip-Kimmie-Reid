using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextureHolder : MonoBehaviour
{
    public enum WhichPath
    {
        Editor,
        Build
    }
    public Texture2D[] textures;
    public PhotoInfoDatabase photoDatabase;
    public WhichPath whichPath;
    public string imagePath;
    public string snapshotSavePath;

    public void RefreshGallery()
    {
        textures = new Texture2D[photoDatabase.photos.Count];

        for (int i = 0; i < photoDatabase.photos.Count; i++)
        {
            imagePath = Path.Combine(Application.persistentDataPath, snapshotSavePath, photoDatabase.photos[i].photoName);
            if (File.Exists(imagePath))
            {
                // Load the image as a Texture2D
                byte[] fileData = File.ReadAllBytes(imagePath);
                Texture2D loadedTexture = new Texture2D(128, 128); // Create an empty Texture2D
                loadedTexture.LoadImage(fileData); // Load the image data into the Texture2D
                textures[i] = loadedTexture;


            }
            else
            {
                Debug.LogError("Image file not found: " + imagePath);
            }


        }


    }


    public void ReloadImagesInGallery()
    {

        textures = new Texture2D[photoDatabase.photos.Count];
        Debug.Log(photoDatabase.photos.Count + " is the number of photos being counted from photoDatabase.");
        photoDatabase = FindAnyObjectByType<PhotoInfoDatabase>();
        for (int i = 0; i < photoDatabase.photos.Count; i++)
        {
            imagePath = Path.Combine(Application.persistentDataPath, snapshotSavePath, photoDatabase.photos[i].photoName);
            Debug.Log(i + ": " + imagePath);
            if (File.Exists(imagePath))
            {
                // Load the image as a Texture2D
                byte[] fileData = File.ReadAllBytes(imagePath);
                Texture2D loadedTexture = new Texture2D(128, 128); // Create an empty Texture2D
                loadedTexture.LoadImage(fileData); // Load the image data into the Texture2D
                textures[i] = loadedTexture;
            }
            else
            {
                Debug.LogError("Image file not found: " + imagePath);
                // make a new one
            }
        }
    }
}
