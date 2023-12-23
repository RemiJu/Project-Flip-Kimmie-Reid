using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryPageInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject galleryPrefabUI;
    public FlipPhoneManager flipPhone;
    public FlipPhone_BaseState flipPhoneState;

    public void Awake()
    {
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        flipPhoneState = flipPhone.currentState;
    }

    public void OnEnable()
    {
        Invoke("InstantiatePrefab", 0.1f);
        Debug.Log("Invoking");
    }

    public void OnDisable()
    {
        Debug.Log("GalleryPageInstantiator: Destroying image grid");
        DestroyAllChildren();
    }

    public void InstantiatePrefab()
    {
        flipPhoneState = flipPhone.currentState;
        Debug.Log(flipPhone.currentState + " is the current state WEEWOO - InstantiatePrefab()");
        if (flipPhoneState.GetType() == flipPhone.galleryState.GetType())
        {
            Debug.Log("Gallery state is true, instantiating prefab for gallery");
            if (galleryPrefabUI != null)
            {
                // Get the UI Canvas transform
                Transform canvasTransform = GetComponentInParent<Canvas>().transform;

                // Instantiate the prefab as a child of this object (the one this script is on)
                Instantiate(galleryPrefabUI, transform);
                Debug.Log("Prefab instantiated successfully.");
            }
            else
            {
                Debug.LogError("Prefab is not assigned!");
            }
        }
        else
        {
            Debug.Log("Gallery state is not active.");
        }
    }

    public void DestroyAllChildren()
    {
        // Destroy all child objects of this GameObject
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}