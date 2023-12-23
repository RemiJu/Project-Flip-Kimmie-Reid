using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Photography : MonoBehaviour
{
    Camera snapCam; // the camera parent separate from the main game's camera
    public CinemachineVirtualCamera photographyCamera; // the Cinemachine Vcam that will be used for the capture

    int resWidth = 1600; // currently set to 2 megapixels (Razr 2 v9x photo quality)
    int resHeight = 1200;  // currently set to 2 megapixels (Razr 2 v9x photo quality)

    void Awake()
    {
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        photographyCamera.Priority = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallTakeSnapshot()
    {
        photographyCamera.Priority = 10;
    }

    public void LateUpdate() // we want the photo to be taken after the other stuff (updates) are done (the camera becomes active)
    {
        if (photographyCamera.Priority == 10) // render/capture the screenshot:
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false); // RGB 24 is the RGB colour depth, we like that. the final argument is mipmapping which is 'false')
            snapCam.Render();

            RenderTexture currentActive = RenderTexture.active;

            RenderTexture.active = snapCam.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0); // controls the space captured by the snapped pic
            byte[] bytes = snapshot.EncodeToPNG(); // creates a byte array to store the existence of captured image
            string fileName = SnapshotName(); // naming the output PNG
            System.IO.File.WriteAllBytes(fileName, bytes); // writing the file to disk
            Debug.Log("Snapshot taken! Uwu");
            photographyCamera.Priority = 0; // Photography camera finishes its job
            RecordPhotoInfo();
            RenderTexture.active = currentActive;
        }
    }

    string SnapshotName() // decides naming rules for output PNG
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath, // might want to change this to persistent data path when building
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void RecordPhotoInfo() // Adds a PhotoInfo to the list in the database with the info inside
    {
        PhotoInfo photoInfo = new PhotoInfo();
        //Record gameLocation
        //if (SceneManager.GetActiveScene().name == "House") { photoInfo.gameLocation = PhotoInfo.Location.House; }
        //else if (SceneManager.GetActiveScene().name == "Warehouse") { photoInfo.gameLocation = PhotoInfo.Location.Warehouse; }
        //else { photoInfo.gameLocation = PhotoInfo.Location.Unknown; }
        FindAnyObjectByType<PhotoInfoDatabase>().AddPhoto(photoInfo);
        Debug.Log("Oh Yeah it's runnin");
    }
}
