using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class GalleryListBehaviour : MonoBehaviour
{
    public int listItemCount;
    public Dictionary<string, GameObject> ListTargetEntries = new Dictionary<string, GameObject>();
    public string currentlySelected;
    public TextureHolder textureHolder;
    public GameObject galleryPage;

    void OnEnable()
    {
        ListTargetEntries.Clear();
        foreach (Transform child in transform)
        {
            ListTargetEntries[child.gameObject.name] = child.gameObject;
        }
        listItemCount = ListTargetEntries.Count();
        LogDictionaryItems(ListTargetEntries);
        ResetGameObjectBehaviour();
    }

    // Update is called once per frame
    void OnDisable()
    {
        DeselectAll();
    }

    public void DeselectAllButOne(string currentlySelected)
    {
        IEnumerable<GameObject> objectsExceptOne = GetObjectsExceptOne(currentlySelected);
        foreach (GameObject gameObject in objectsExceptOne)
        {
            var gameObjectsButton = gameObject.GetComponent<Button>();
            gameObjectsButton.interactable = false;
        }
    }

    public void ResetGameObjectBehaviour()
    {
        foreach (GameObject gameObject in ListTargetEntries.Values)
        {
            var gameObjectsButton = gameObject.GetComponent<Button>();
            //gameObjectsButton.interactable = true;
        }
        RefreshButtonsInAlbum();
    }

    public void RefreshButtonsInAlbum()
    {
        galleryPage.SetActive(false);
        galleryPage.SetActive(true);
    }

    public void DeselectAll()
    {
        foreach (GameObject gameObject in ListTargetEntries.Values)
        {
            var gameObjectsButton = gameObject.GetComponent<Button>();
            gameObjectsButton.interactable = false;
        }
    }
    //public GameObject GetObject(string name)
    //{
    //    if (ListTargetEntries.ContainsKey(name))
    //    {
    //        return ListTargetEntries[name];
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Object with the name " + name + " does not exist in the dictionary.");
    //        return null;
    //    }
    //}

    void LogDictionaryItems<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        string logString = "List Target Dictionary Items:\n";

        foreach (var kvp in dictionary)
        {
            logString += $"{kvp.Key}: {kvp.Value}\n";
        }

        Debug.Log(logString);
    }

    public IEnumerable<GameObject> GetObjectsExceptOne(string excludedObjectName)
    {
        return ListTargetEntries
            .Where(pair => pair.Key != excludedObjectName)
            .Select(pair => pair.Value);
    }
}
