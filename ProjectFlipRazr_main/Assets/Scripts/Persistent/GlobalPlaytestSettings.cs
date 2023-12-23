using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalPlaytestSettings : MonoBehaviour
{

    //REFERENCE -- SCRIPT EXECUTION ORDER
    // void Awake -- Initialization, happens 1 time in a script instance's lifetime.
    // void Start -- Runs after the first frame update, delayed until all void Awake 'activity' is complete.
    // void OnSceneLoaded using UnityEngine.SceneMangement -- This happens once the scene is completely loaded, which includes waiting for void Start to be done.
    //      IMPORTANT NOTE: OnSceneLoaded needs to be subscribed to PRIOR to the scene you want it to work in. So it must either be subscribed in a scene prior to the one
    //      you want it to work in, or you must subscribe to it in a 'void Awake'. An example is provided in this script for referencing the Player GameObject.


    // Start is called before the first frame update
    public static GlobalPlaytestSettings instance;

    [Header("PhoneApps")]
    // PHONE APPS
    public bool hasCamera;
    public bool hasGallery;
    public bool hasMap;
    public bool hasSettings;
    public bool hasSocialMedia;
    public bool hasSaveQuit;

    [Header("Locations")]
    // LOCATIONS
    public bool hasKimmiesHouse;
    public bool hasStudio;
    public bool hasVillas;

    [Header("Kimmie Reid")]
    //Reference to Player GameObject so we don't need a million scripts doing GameObject.Find for 'Player'
    public GameObject player;

    [Header("StudioPhotoItems")]
    public bool hasRingImprint;
    public bool hasOfficeKey;
    public bool hasDirectorPhotoFrame;

    [Header("Public Strings")]
    public string currentLocation;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event immediately.
        DontDestroyOnLoad(this);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This will run every time a new scene is loaded.
        player = GameObject.Find("Player"); // searches for the Player parent GameObject when the script is loaded
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
