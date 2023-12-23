using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ListTargetBehaviour : MonoBehaviour
{
    public int listItemCount;
    public Dictionary<string, Button> ListTargetEntries = new Dictionary<string, Button>();
    public string currentlySelected;

    void OnEnable()
    {
        ListTargetEntries.Clear();
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            ListTargetEntries[button.name] = button;
        }
        listItemCount = ListTargetEntries.Count();
        LogDictionaryItems(ListTargetEntries);
        ResetButtonBehaviour();

    }

    // Update is called once per frame
    void OnDisable()
    {
        DeselectAll();
    }

    public void DeselectAllButOne(string currentlySelected)
    {
        IEnumerable<Button> objectsExceptOne = GetObjectsExceptOne(currentlySelected);
        foreach (Button button in objectsExceptOne)
        {
            button.interactable = false;
        }
    }

    public void ResetButtonBehaviour()
    {
        foreach (Button button in ListTargetEntries.Values)
        {
            button.interactable = true;
        }
    }

    public void DeselectAll()
    {
        foreach (Button button in ListTargetEntries.Values)
        {
            button.interactable = false;
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

    public IEnumerable<Button> GetObjectsExceptOne(string excludedObjectName)
    {
        return ListTargetEntries
            .Where(pair => pair.Key != excludedObjectName)
            .Select(pair => pair.Value);
    }
}
