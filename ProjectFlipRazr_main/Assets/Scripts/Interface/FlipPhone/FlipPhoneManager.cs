using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class FlipPhoneManager : MonoBehaviour
{
    public FlipPhone_BaseState currentState;

    [Header("Array of Apps")]
    public string[] objectNames = { "Home", "PhoneViewfinder", "Gallery", "PhotoIndividual", "AppMenu", "Map", "SocialMedia", "Settings", "SaveQuit" };

    //Game Object Dictionary (Phone Pages)
    public Dictionary<string, GameObject> phonePages = new Dictionary<string, GameObject>();

    //State Machine Dictionary (Phone Pages)
    public Dictionary<string, Type> stateMap = new Dictionary<string, Type>();

    [Header("Options Context Menu")]
    public GameObject options;

    [Header("Phone Navigation Buttons")]
    public GameObject lButton;
    public GameObject rButton;
    public PhoneNavi phoneNaviL;
    public PhoneNavi phoneNaviR;

    [Header("Booleans")]
    public bool cameraOpen;

    //States
    public FlipPhone_HomeScreenState homeScreenState = new FlipPhone_HomeScreenState();
    public FlipPhone_CameraState cameraState = new FlipPhone_CameraState();
    public FlipPhone_GalleryState galleryState = new FlipPhone_GalleryState();
    public FlipPhone_PhotoIndividualState photoIndividualState = new FlipPhone_PhotoIndividualState();
    public FlipPhone_MainMenuState mainMenuState = new FlipPhone_MainMenuState();
    public FlipPhone_MapState mapState = new FlipPhone_MapState();
    public FlipPhone_SocialMediaState socialMediaState = new FlipPhone_SocialMediaState();
    public FlipPhone_SettingsState settingsState = new FlipPhone_SettingsState();
    public FlipPhone_SaveQuitState saveQuitState = new FlipPhone_SaveQuitState();

    [Header("App Page Objects")]
    public GameObject appMenu;
    void Start()
    {
        // Add UI GameObjects to phone pages dictionary
        foreach (string name in objectNames)
        {
            AddObject(name, GameObject.Find(name));
        }

        // Add state types to the dictionary
        //stateMap = GetStates();

        // prints all dictionary entries gathered above for debugging purposes in debug.log
        foreach (var kvp in stateMap)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        //LogDictionaryItems(phonePages);


        //starting state for the state machine
        currentState = homeScreenState;
        currentState.EnterState(this);
    }

    void Update()
    {
        //currentState.UpdateState(this);
    }

    public void SwitchState(FlipPhone_BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }

    // Dictionary business
    private void AddObject(string name, GameObject obj)
    {
        if (!phonePages.ContainsKey(name))
        {
            phonePages.Add(name, obj);
        }
        else
        {
            Debug.LogWarning("Object with the name " + name + " already exists in the dictionary.");
        }
    }

    // Method to get a GameObject by name from the dictionary
    public GameObject GetObject(string name)
    {
        if (phonePages.ContainsKey(name))
        {
            return phonePages[name];
        }
        else
        {
            Debug.LogWarning("Object with the name " + name + " does not exist in the dictionary.");
            return null;
        }
    }

    public void NavigationButton_L(PhoneNavi.whichNaviButton buttonName)
    {
        phoneNaviL.button = buttonName;

    }

    public void NavigationButton_R(PhoneNavi.whichNaviButton buttonName)
    {
        phoneNaviR.button = buttonName;

    }

    public IEnumerable<GameObject> GetObjectsExceptOne(string excludedObjectName)
    {
        return phonePages
            .Where(pair => pair.Key != excludedObjectName)
            .Select(pair => pair.Value);
    }

    public void ChangeStates(string stateName, FlipPhoneManager flipPhone)
    {
        FlipPhone_BaseState newState = null;

        // Using a switch statement to select the state based on the stateName
        switch (stateName)
        {
            case "FlipPhone_HomeScreenState":
                newState = flipPhone.homeScreenState;
                break;
            case "FlipPhone_CameraState":
                newState = flipPhone.cameraState;
                break;
            case "FlipPhone_GalleryState":
                newState = flipPhone.galleryState;
                break;
            case "FlipPhone_PhotoIndividualState":
                newState = flipPhone.photoIndividualState;
                break;
            case "FlipPhone_MainMenuState":
                newState = flipPhone.mainMenuState;
                break;
            case "FlipPhone_MapState":
                newState = flipPhone.mapState;
                break;
            case "FlipPhone_SocialMediaState":
                newState = flipPhone.socialMediaState;
                break;
            case "FlipPhone_SettingsState":
                newState = flipPhone.settingsState;
                break;
            case "FlipPhone_SaveQuitState":
                newState = flipPhone.saveQuitState;
                break;
                // Add more cases for other states
        }

        if (newState != null)
        {
            // Switch to the new state
            SwitchState(newState);
        }
        else
        {
            Debug.LogError("State not found: " + stateName);
        }
        //    // Check if the state name exists in the dictionary
        //    if (stateMap.ContainsKey(stateName))
        //    {
        //        Debug.Log("weewoo " + stateName + " is the statename being pulled into ChangeStates()");
        //        // Get the type from the dictionary
        //        Type stateType = stateMap[stateName];

        //        // Check if the type is derived from FlipPhone_BaseState
        //        if (typeof(FlipPhone_BaseState).IsAssignableFrom(stateType))
        //        {
        //            // Create an instance of the state type
        //            FlipPhone_BaseState newState = (FlipPhone_BaseState)Activator.CreateInstance(stateType);

        //            // Check if the state has the required method signature
        //            MethodInfo enterMethod = stateType.GetMethod("EnterState", new Type[] { typeof(FlipPhoneManager) });
        //            if (enterMethod != null)
        //            {
        //                // Invoke the EnterState method with the FlipPhoneManager parameter
        //                enterMethod.Invoke(newState, new object[] { flipPhone });
        //                currentState = newState;
        //                Debug.Log("currentState is " + currentState + " and newState is " + newState);
        //            }
        //            else
        //            {
        //                Debug.LogError("State does not have the required EnterState method signature.");
        //            }
        //        }
        //        else
        //        {
        //            Debug.LogError("Invalid state type: " + stateType);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("State not found: " + stateName);
        //    }
        //}

        Dictionary<string, Type> GetStates()
        {
            // Use reflection to find all types that derive from the FlipPhone_BaseState class
            Type baseType = typeof(FlipPhone_BaseState);
            IEnumerable<Type> stateTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p != baseType);

            // Instantiate each state type and add to the dictionary
            Dictionary<string, Type> stateMap = new Dictionary<string, Type>();
            foreach (Type type in stateTypes)
            {
                stateMap.Add(type.Name, type);
            }

            LogDictionaryItems(stateMap);
            return stateMap;
        }

        void LogDictionaryItems<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            string logString = "Dictionary Items:\n";

            foreach (var kvp in dictionary)
            {
                logString += $"{kvp.Key}: {kvp.Value}\n";
            }

            Debug.Log(logString);
        }
    }
}
