using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class CaptureScreen2 : MonoBehaviour
{
    public static CaptureScreen2 instance;
    public TextureHolder textureHolder;
    public string filePath;
    public string photoNameVariable;
    public bool isBuild;
    public Camera targetCamera;
    public FlipPhoneManager flipPhoneManager;
    public int snapResWidth = 1600;
    public int snapResHeight = 1200;

    private string newID;

    private LayerMask rayIgnore = (1 << 13);

    //public PopulateGallery populateGallery;
    public PhoneSwitcher phoneSwitcher;

    //Changes the angle of where the photo item needs to be to be accepted as an item
    public float photoItemAngleCheckX = 20;
    public float photoItemAngleCheckY = 20;

    public Transform directionReference;
    public List<GameObject> photoItemsInRange;
    public List<PhotoInfo.PhotoItem> photoItemsToRecord;

    [Header("Gizmos")]
    public float lineLength;

    [Header("Booleans")]
    public bool requirePuzzleManager;

    [Header("Dialogue")]
    public GameObject dialoguePrefab;

    void Awake()
    {
        instance = this;

        // populateGallery = FindAnyObjectByType<PopulateGallery>();
    }

    public void Capture()
    {
        if (phoneSwitcher.isFirstPersonMode == true && flipPhoneManager.cameraOpen == true && !phoneSwitcher.dialogueIsActive)
        {
            CheckItemsInPhoto();
            StartCoroutine(AsyncCapture());
        }
    }

    IEnumerator AsyncCapture()
    {
        yield return new WaitForEndOfFrame();

        // Create a temporary RenderTexture for capturing the screenshot
        var captureRT = new RenderTexture(snapResWidth, snapResHeight, 0, RenderTextureFormat.ARGB32);
        captureRT.Create();

        // Store the current active RenderTexture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the temporary RenderTexture as the active RenderTexture
        RenderTexture.active = captureRT;

        // Copy the content of the camera's target RenderTexture to the temporary RenderTexture
        Graphics.Blit(targetCamera.GetComponent<Camera>().targetTexture, captureRT);

        // Reset the active RenderTexture to the original one
        RenderTexture.active = currentActiveRT;

        // Perform AsyncGPUReadback on the temporary RenderTexture
        AsyncGPUReadback.Request(captureRT, 0, TextureFormat.RGBA32, OnCompleteReadback);

        // Release the temporary RenderTexture
        captureRT.Release();
    }



    void OnCompleteReadback(AsyncGPUReadbackRequest asyncGPUReadbackRequest)
    {
        if (asyncGPUReadbackRequest.hasError)
        {
            Debug.LogError("Error Capturing Screenshot: With AsyncGPUReadbackRequest.");
            return;
        }

        var rawData = asyncGPUReadbackRequest.GetData<byte>();
        var width = snapResWidth;
        var height = snapResHeight;

        var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        var processedData = texture.GetRawTextureData<byte>();

        // Copy the data directly without flipping
        rawData.CopyTo(processedData);

        // Save the screenshot using Guid as the name
        newID = photoNameVariable + Guid.NewGuid().ToString();

        string savePath = isBuild ? Application.persistentDataPath : Application.dataPath;
        File.WriteAllBytes(savePath + filePath + newID + ".png", ImageConversion.EncodeToPNG(texture));
        Debug.Log((isBuild ? "BUILD: " : "EDITOR: ") + "Capture written! To " + savePath + filePath + newID + ".png");

        Destroy(texture);

        RecordPhotoInfo();
        textureHolder.RefreshGallery();
        AudioManager.instance.Play("CameraSnap");
    }


    public void RecordPhotoInfo() // Adds a PhotoInfo to the list in the database with the info inside
    {
        PhotoInfo photoInfo = new PhotoInfo();

        //Record photoName
        photoInfo.photoName = newID + ".png";
        photoInfo.name = newID + ".png";

        //Record fileLocation
        photoInfo.fileLocation = Application.dataPath.ToString() + filePath.ToString();

        //Record gameLocation
        if (SceneManager.GetActiveScene().name == "Kimmie's House") { photoInfo.gameLocation = PhotoInfo.Location.KimmiesHouse; }
        else if (SceneManager.GetActiveScene().name == "Studio Warehouse") { photoInfo.gameLocation = PhotoInfo.Location.StudioStage56; }
        else if (SceneManager.GetActiveScene().name == "Villas") { photoInfo.gameLocation = PhotoInfo.Location.Villas; }
        else { photoInfo.gameLocation = PhotoInfo.Location.Unknown; }

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

    private void OnDrawGizmos()
    {
        if (directionReference == null)
        {
            Debug.LogError("Direction Reference not assigned. Please assign it in the inspector.");
            return;
        }

        Gizmos.color = Color.red;

        // Visualize rotation around the local X-axis
        DrawAngleLine(Vector3.right, photoItemAngleCheckX);

        Gizmos.color = Color.green;

        // Visualize rotation around the local Y-axis
        DrawAngleLine(Vector3.up, photoItemAngleCheckY);
    }

    private void DrawAngleLine(Vector3 axis, float angle)
    {
        Vector3 startPoint = directionReference.position;
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        Vector3 endPoint = directionReference.position + rotation * directionReference.forward;

        Gizmos.DrawLine(startPoint, endPoint);
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
                    //Debug.Log("Y is clear");
                    if (transform.eulerAngles.x <= directionReference.eulerAngles.x + photoItemAngleCheckX || transform.eulerAngles.x >= directionReference.eulerAngles.x - photoItemAngleCheckX)
                    {
                        //Debug.Log("X is clear");

                        Ray ray = new Ray(transform.position, (item.transform.position - transform.position).normalized);
                        if (Physics.Raycast(ray, out RaycastHit hit, 10f, ~(rayIgnore), QueryTriggerInteraction.Collide))
                        {
                            Debug.Log(hit.collider.name);
                            if (hit.collider.gameObject == item.gameObject)
                            {
                                if (item.GetComponent<PhotoItem>() != null)
                                {
                                    var photoItem = item.GetComponent<PhotoItem>();
                                    photoItemsToRecord.Add(photoItem.photoItem);
                                    if(requirePuzzleManager)
                                    {
                                        FindAnyObjectByType<PuzzleManager>().CheckPhotoItemProgression(item.GetComponent<PhotoItem>().photoItem);
                                    }
                                    if (photoItem.SpawnDialogue != null)
                                    {
                                        dialoguePrefab.GetComponent<DialogueManager>().dialogueSystem = item.GetComponent<PhotoItem>().SpawnDialogue;
                                        Instantiate(dialoguePrefab);
                                    }
                                    if (photoItem.progressQuest && !photoItem.questProgressionDone)
                                    {
                                        var dramaManager = FindAnyObjectByType<DramaManager>();
                                        dramaManager.UpdateQuestProgression(dramaManager.dramaSystem.quests[photoItem.questNumberToProgress]);
                                        photoItem.questProgressionDone = true;
                                    }
                                    if (photoItem.destroyIfPhotoTaken)
                                    {
                                        if (transform.parent != null)
                                        {
                                            item.transform.parent.gameObject.SetActive(false);
                                            //Destroy(item.transform.parent.gameObject);
                                        }
                                        else
                                        {
                                            item.SetActive(false);
                                            //Destroy(item.gameObject);
                                        }
                                    }
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
