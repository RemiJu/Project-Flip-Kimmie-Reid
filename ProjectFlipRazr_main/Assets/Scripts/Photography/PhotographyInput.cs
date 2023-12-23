using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotographyInput : MonoBehaviour
{
    // public Photography snapCam;
    //public CaptureScreen captureScreen;
    public CaptureScreen2 captureScreen;
    public PopulateGallery populateGallery;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // snapCam.CallTakeSnapshot();
            Debug.Log("you pressed space!");
            captureScreen.Capture();
        }
    }
}
