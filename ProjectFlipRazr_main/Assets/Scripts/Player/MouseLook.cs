using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    float cameraX, cameraY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, cameraX * Time.deltaTime);
        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput (Vector2 lookInput)
    {
        cameraX = lookInput.x * sensitivityX;
        cameraY = lookInput.y * sensitivityY;
    }
}
