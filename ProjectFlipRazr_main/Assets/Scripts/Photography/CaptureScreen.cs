using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class CaptureScreen : MonoBehaviour
{
    public static CaptureScreen instance;
    public string filePath;
    public string photoNameVariable;
    public bool isBuild;

    public int snapResWidth = 1600;
    public int snapResHeight = 1200;

    private string newID;

    //public PopulateGallery populateGallery;

    //Changes the angle of where the photo item needs to be to be accepted as an item
    public float photoItemAngleCheckX = 20;
    public float photoItemAngleCheckY = 20;

    public Transform directionReference;
    public List<GameObject> photoItemsInRange;
    public List<PhotoInfo.PhotoItem> photoItemsToRecord;

    void Awake()
    {
        instance = this;

        //populateGallery = FindAnyObjectByType<PopulateGallery>();
    }

    public void Capture()
    {
        CheckItemsInPhoto();
        StartCoroutine(AsyncCapture());
    }

    IEnumerator AsyncCapture()
    {
        yield return new WaitForEndOfFrame();
        //var rt = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
        var rt = RenderTexture.GetTemporary(snapResWidth, snapResHeight, 0, RenderTextureFormat.ARGB32);
        ScreenCapture.CaptureScreenshotIntoRenderTexture(rt);
        AsyncGPUReadback.Request(rt, 0, TextureFormat.RGBA32, OnCompleteReadback);
        RenderTexture.ReleaseTemporary(rt);
    }

    void OnCompleteReadback(AsyncGPUReadbackRequest asyncGPUReadbackRequest)
    {
        // get screenshot data as nativearray or handle error
        if (asyncGPUReadbackRequest.hasError)
        {
            Debug.LogError("Error Capturing Screenshot: With AsyncGPUReadbackRequest.");
            return;
        }
        var rawData = asyncGPUReadbackRequest.GetData<byte>();
        // Grab screen dimensions
        //var width = Screen.width;
        //var height = Screen.height;
        var width = snapResWidth;
        var height = snapResHeight;
        var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        var processedData = texture.GetRawTextureData<byte>();
        // now flip vertical pixels
        for (int i = 0; i < rawData.Length; i += 4)
        {
            var arrayIndex = i / 4;
            var x = arrayIndex % width;
            var y = arrayIndex / width;
            var flippedY = (height - 1 - y);
            var flippedIndex = x + flippedY * width;
            // flip the data
            processedData[i] = rawData[flippedIndex * 4];
            processedData[i + 1] = rawData[flippedIndex * 4 + 1];
            processedData[i + 2] = rawData[flippedIndex * 4 + 2];
            processedData[i + 3] = rawData[flippedIndex * 4 + 3];
        }
        // create texture and save as png using Guid as name
		newID = photoNameVariable + Guid.NewGuid().ToString();
        if (isBuild)
        {
            File.WriteAllBytes(Application.persistentDataPath + filePath + newID + ".png", ImageConversion.EncodeToPNG(texture));
            Debug.Log("BUILD: Capture written! To " + Application.persistentDataPath + filePath + newID + ".png");
        }
        else
        {
            File.WriteAllBytes(Application.dataPath + filePath + newID + ".png", ImageConversion.EncodeToPNG(texture));
            Debug.Log("EDITOR: Capture written! To " + Application.dataPath + filePath + newID + ".png");
        }
        //capture the information from (string path) above, send it to the entry in the class for fileLocation/fileName)
        Debug.Log("Capture written! To " + filePath);
        Destroy(texture);
        RecordPhotoInfo();
        //populateGallery.RefreshGallery();
    }

    public void RecordPhotoInfo() // Adds a PhotoInfo to the list in the database with the info inside
    {
        PhotoInfo photoInfo = new PhotoInfo();

        //Record photoName
        photoInfo.photoName = newID + ".png";

        //Record fileLocation
        photoInfo.fileLocation = Application.dataPath.ToString() + filePath.ToString();

        //Record gameLocation
        //if (SceneManager.GetActiveScene().name == "House") { photoInfo.gameLocation = PhotoInfo.Location.House; }
        //else if (SceneManager.GetActiveScene().name == "Warehouse") { photoInfo.gameLocation = PhotoInfo.Location.Warehouse; }
        //else if (SceneManager.GetActiveScene().name == "Studio") { photoInfo.gameLocation = PhotoInfo.Location.Studio; }
        //else if (SceneManager.GetActiveScene().name == "Villas") { photoInfo.gameLocation = PhotoInfo.Location.Villas; }
        //else { photoInfo.gameLocation = PhotoInfo.Location.Unknown; }

        //Record photoItems
        photoInfo.photoItems = photoItemsToRecord.ToArray();

        //Record photoTime
        photoInfo.photoTime = DateTime.Now;

        //Add Photo
        FindAnyObjectByType<PhotoInfoDatabase>().AddPhoto(photoInfo);
        Debug.Log("PhotoInfo added to list");
    }

    private void OnTriggerEnter(Collider other)
    {
        photoItemsInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        photoItemsInRange.Remove(other.gameObject);
    }

    public void CheckItemsInPhoto()
    {
        photoItemsToRecord.Clear();
        if (photoItemsInRange.Count > 0)
        {
            foreach (GameObject item in photoItemsInRange)
            {
                directionReference.LookAt(item.transform);
                if (directionReference.localEulerAngles.y <= photoItemAngleCheckY || directionReference.localEulerAngles.y >= 360 - photoItemAngleCheckY)
                {
                    Debug.Log("Y is clear");
                    if (transform.eulerAngles.x <= directionReference.eulerAngles.x + photoItemAngleCheckX || transform.eulerAngles.x >= directionReference.eulerAngles.x - photoItemAngleCheckX)
                    {
                        Debug.Log("X is clear");

                        Ray ray = new Ray(transform.position, (item.transform.position - transform.position).normalized);
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == item.gameObject)
                            {
                                if (item.GetComponent<PhotoItem>() != null)
                                {
                                    photoItemsToRecord.Add(item.GetComponent<PhotoItem>().photoItem);
                                    Debug.Log("PhotoItem Accepted");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
