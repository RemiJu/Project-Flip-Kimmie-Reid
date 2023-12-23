using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapList : MonoBehaviour
{
    // Public dictionary to store child objects by name
    public Dictionary<string, GameObject> locListEntry = new Dictionary<string, GameObject>();
    public GlobalPlaytestSettings progressionSingleton;
    public string currentlySelected;
    public int numberOfLocations; // this is a value that keeps track of the number of locations that are listed while the MapList is open


    void OnEnable()
    {
        CollectlocListEntry();
        CheckLocationUnlocks();
        DisplayCurrentLocation(progressionSingleton.currentLocation);
    }

    void OnDisable()
    {
        numberOfLocations = 0;
    }

    void CollectlocListEntry()
    {
        // Clear the dictionary before collecting child objects to avoid duplicates
        locListEntry.Clear();

        // Loop through each child GameObject
        foreach (Transform child in transform)
        {
            // Add the child GameObject to the dictionary using its name as the key
            locListEntry[child.name] = child.gameObject;
        }

        // Log the collected child objects for debugging
        LogDictionaryItems(locListEntry);
    }

    void LogDictionaryItems<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        string logString = "Map Location Dictionary Items:\n";

        foreach (var kvp in dictionary)
        {
            logString += $"{kvp.Key}: {kvp.Value}\n";
        }

        Debug.Log(logString);
    }

    void CheckLocationUnlocks()
    {
        progressionSingleton = FindAnyObjectByType<GlobalPlaytestSettings>();

        //Checks Progression for Kimmie's House unlock boolean
        if(progressionSingleton.hasKimmiesHouse && progressionSingleton.currentLocation != "Kimmie's House")
        {
            ActivateLocationOption("House");
        }
        else
        {
            DeactivateLocationOption("House");
        }

        //Checks Progression for Studio unlock boolean
        if (progressionSingleton.hasStudio && progressionSingleton.currentLocation != "Studio Warehouse")
        {
            ActivateLocationOption("Studio");
        }
        else
        {
            DeactivateLocationOption("Studio");
        }

        //Checks Progression for Villas unlock boolean
        if(progressionSingleton.hasVillas && progressionSingleton.currentLocation != "Villas")
        {
            ActivateLocationOption("Villas");
        }
        else
        {
            DeactivateLocationOption("Villas");
        }
    }

    void ActivateLocationOption(string objectName)
    {
        // Check if the objectName is in the dictionary
        if (locListEntry.ContainsKey(objectName))
        {
            // Set the associated GameObject to active
            locListEntry[objectName].SetActive(true);
            numberOfLocations += 1;
        }
        else
        {
            Debug.LogWarning($"Object with the name {objectName} not found in the dictionary.");
        }
    }

    void DeactivateLocationOption(string objectName)
    {
        // Check if the objectName is in the dictionary
        if (locListEntry.ContainsKey(objectName))
        {
            // Set the associated GameObject to inactive
            locListEntry[objectName].SetActive(false);
            numberOfLocations -= 1;
        }
        else
        {
            Debug.LogWarning($"Object with the name {objectName} not found in the dictionary.");
        }
    }

    void DisplayCurrentLocation(string currentLocationName)
    {
        if(locListEntry.ContainsKey("CurrentLocation"))
        {
            if(progressionSingleton.currentLocation != null)
            {
                locListEntry["CurrentLocation"].SetActive(true);
                locListEntry["CurrentLocation"].GetComponentInChildren<TextMeshProUGUI>().text = currentLocationName;  
            }
            else
            {
                locListEntry["CurrentLocation"].SetActive(false);
            }
        }
        else
        {
            locListEntry["CurrentLocation"].SetActive(false);
        }
    }

    public GameObject FindFirstActiveChildAfterIndex(GameObject parent, int startIndex)
    {
        // Check if the parent object is valid
        if (parent != null)
        {
            // Start from the specified index and iterate through the child objects
            for (int i = startIndex; i < parent.transform.childCount; i++)
            {
                Transform child = parent.transform.GetChild(i);
                // Check if the current child is active
                if (child.gameObject.activeSelf)
                {
                    return child.gameObject;
                }
            }
        }
        // Return null if no active child is found after the specified index
        return null;
    }

}
