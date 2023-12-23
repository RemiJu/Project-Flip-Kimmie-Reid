using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROTOTYPE_CameraFrameMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform cameraFrame;

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        input = Quaternion.Euler(0, cameraFrame.eulerAngles.y, 0) * input;

        transform.position += input * Time.deltaTime * 5;
    }
}
