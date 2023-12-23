using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class FolderManagement : MonoBehaviour
{
    private string folderName = "Resources";
    private string folderName2 = "Snapshots";
    public bool isBuild;

    public void Start()
    {
        if (isBuild)
        {
            CreateFolders();
            //creates blank pdata (maybe overwrites? idk)
            SaveSystem.SavePhotos(FindObjectOfType<PhotoInfoDatabase>().photos);
        }
    }

    public void CreateFolders()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        // Check for RESOURCES folder
        if (!Directory.Exists(folderPath))
        {
            // If the folder doesn't exist, create it
            Directory.CreateDirectory(folderPath);
            Debug.Log("Folder created at: " + folderPath);
        }
        else
        {
            Debug.Log("'Resources' Folder already exists at: " + folderPath);
        }

        string folderPath2 = Path.Combine(Application.persistentDataPath, folderName, folderName2);

        // Check for SNAPSHOTS folder
        if (!Directory.Exists(folderPath2))
        {
            Directory.CreateDirectory(folderPath2);
            Debug.Log("Folder created at: " + folderPath);
        }
        else
        {
            Debug.Log("'Snapshots' Folder already exists at: " + folderPath2);
        }
    }
}



