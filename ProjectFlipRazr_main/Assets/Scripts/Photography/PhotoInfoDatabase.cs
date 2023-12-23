using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoInfoDatabase : MonoBehaviour
{
    public int photoMemoryCount = 20;
    public List<PhotoInfo> photos;

    private void Awake()
    {
        //Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Showinfo();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RemoveAllPhoto();
        }
    }

    public void AddPhoto(PhotoInfo photoData)
    {
        if (photos.Count == photoMemoryCount)
        {
            Debug.Log("Photo memory is full");
        }
        else
        {
            photos.Add(photoData);
        }
    }

    public void RemovePhoto(PhotoInfo photoData)
    {
        photos.Remove(photoData);
    }

    public void RemoveAllPhoto()
    {
        photos.Clear();
    }

    public void Showinfo()
    {
        if (photos.Count > 0)
        {
            foreach (PhotoInfo photo in photos)
            {
                Debug.Log(photo.fileLocation + " " + photo.photoName + " " + photo.gameLocation + " " + photo.photoItems.Length + " " + photo.photoTime);
                if (photo.photoItems.Length > 0)
                {
                    for (int i = 0; i < photo.photoItems.Length; i++)
                    {
                        Debug.Log(photo.photoItems[i]);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Photos are empty");
        }
    }

    public void Save()
    {
        SaveSystem.SavePhotos(photos);
        Debug.Log("Photos saved");
    }

    public void Load()
    {
        RemoveAllPhoto();
        List<PhotoInfo> data = SaveSystem.LoadPhotos();

        foreach (PhotoInfo photo in data)
        {
            if (photo == null) Debug.Log("*burp* i messed up :)");
            photos.Add(photo);
        }
        Debug.Log("PhotosLoaded");
        FindAnyObjectByType<TextureHolder>().ReloadImagesInGallery();
    }
}
