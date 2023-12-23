using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppList : MonoBehaviour
{
    // Public dictionary to store child objects by name
    public Dictionary<string, GameObject> AppListEntry = new Dictionary<string, GameObject>();
    public GlobalPlaytestSettings progressionSingleton;
    public OptionsContextMenu optionsMenu;
    public string[] availableApps; // name these exactly what the child GameObject of AppList parent is called

    void OnEnable()
    {
        CollectAppListEntry();
        CheckAppUnlocks();
        optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Nothing;
    }


    void Update()
    {

    }

    void CollectAppListEntry()
    {
        // Clear the dictionary before collecting child objects to avoid duplicates
        AppListEntry.Clear();

        // Loop through each child GameObject
        foreach (Transform child in transform)
        {
            // Add the child GameObject to the dictionary using its name as the key
            AppListEntry[child.name] = child.gameObject;
        }

        // Log the collected child objects for debugging
        LogDictionaryItems(AppListEntry);
    }

    void LogDictionaryItems<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        string logString = "Phone App Dictionary Items:\n";

        foreach (var kvp in dictionary)
        {
            logString += $"{kvp.Key}: {kvp.Value}\n";
        }

        Debug.Log(logString);
    }

    void CheckAppUnlocks()
    {
        progressionSingleton = FindAnyObjectByType<GlobalPlaytestSettings>();

        //Checks Progression for Camera unlock boolean
        if (progressionSingleton.hasCamera)
        {
            ActivateApplicationOption(availableApps[0]);
        }
        else
        {
            DeactivateApplicationOption(availableApps[0]);
        }

        //Checks Progression for Gallery unlock boolean
        if (progressionSingleton.hasGallery)
        {
            ActivateApplicationOption(availableApps[1]);
        }
        else
        {
            DeactivateApplicationOption(availableApps[1]);
        }

        //Checks Progression for Map unlock boolean
        if (progressionSingleton.hasMap)
        {
            ActivateApplicationOption(availableApps[2]);
        }
        else
        {
            DeactivateApplicationOption(availableApps[2]);
        }

        //Checks Progression for Social Media boolean
        if (progressionSingleton.hasSocialMedia)
        {
            ActivateApplicationOption(availableApps[3]);
        }
        else
        {
            DeactivateApplicationOption(availableApps[3]);
        }

        //Checks Progression for Settings boolean
        if (progressionSingleton.hasSettings)
        {
            ActivateApplicationOption(availableApps[4]);
        }
        else
        {
            DeactivateApplicationOption(availableApps[4]);
        }

        if (progressionSingleton.hasSaveQuit)
        {
            ActivateApplicationOption(availableApps[5]);
        }
        else
        {
            DeactivateApplicationOption(availableApps[5]);
        }

    }

    void ActivateApplicationOption(string objectName)
    {
        // Check if the objectName is in the dictionary
        if (AppListEntry.ContainsKey(objectName))
        {
            // Set the associated GameObject to active
            AppListEntry[objectName].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Object with the name {objectName} not found in the dictionary.");
        }
    }

    void DeactivateApplicationOption(string objectName)
    {
        // Check if the objectName is in the dictionary
        if (AppListEntry.ContainsKey(objectName))
        {
            // Set the associated GameObject to inactive
            AppListEntry[objectName].SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Object with the name {objectName} not found in the dictionary.");
        }
    }
}

