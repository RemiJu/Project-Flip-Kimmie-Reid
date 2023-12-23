using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoIndividual_Txt : MonoBehaviour
{
    public enum whichDescriptor
    {
        Items,
        Location,
        Date
    }
    public whichDescriptor descriptorProperty;
    public GalleryListBehaviour targetBehaviour;
    public OptionsContextMenu contextMenu;
    public PhotoInfo containedPhotoInfo;
    public TextMeshProUGUI targetText;
    public PhotoInfoDatabase photoDatabase;
    private string gameObjectName;
    // Define three different colors
    Color nothingSelected = new Color(0.75f, 0.75f, 0.75f, 1f);      // Grey
    Color importantColour = new Color(1f, 0.45f, 0f, 1f);    // Red/Orange
    Color uncategorizedColour = new Color(1f, 1f, 1f, 1f);     // White

    // Start is called before the first frame update
    void OnEnable()
    {
       switch (descriptorProperty)
        {
            case whichDescriptor.Items:
                gameObjectName = targetBehaviour.currentlySelected;
                Debug.Log("PhotoIndividual_Txt receiving currently selected: " + gameObjectName);
                // Assuming ListTargetEntries is a Dictionary<string, GameObject>
                if (targetBehaviour.ListTargetEntries.TryGetValue(gameObjectName, out GameObject gameObject))
                {
                    // Get the PhotoStickyButton component from the GameObject
                    PhotoStickyButton photoStickyButton = gameObject.GetComponent<PhotoStickyButton>();

                    if (photoStickyButton != null)
                    {
                    }
                    else
                    {
                        Debug.LogError($"Could not find PhotoStickyButton component on {gameObjectName}.");
                    }
                }
                else
                {
                    Debug.LogError($"Could not find {gameObjectName} in ListTargetEntries.");
                }
                targetText.color = importantColour;
                Debug.Log("Name: " + containedPhotoInfo.photoName + ", Location: " + containedPhotoInfo.gameLocation + ", Items: " + containedPhotoInfo.photoItems);
                if (containedPhotoInfo.photoItems.Length != 0)
                {
                    // Make the text the "important colour"
                    targetText.color = importantColour;

                    // Create a list to store the item strings
                    List<string> itemStrings = new List<string>();

                    // Iterate over the photoItems array and get the corresponding strings
                    foreach (PhotoInfo.PhotoItem item in containedPhotoInfo.photoItems)
                    {
                        string itemString = containedPhotoInfo.GetPhotoItemString(item);
                        itemStrings.Add(itemString);

                    }
                    // Combine the item strings with commas
                    string concatenatedItems = string.Join(", ", itemStrings);

                    // Set the targetText.text to the concatenated item strings
                    targetText.text = concatenatedItems;

                    // If there are more than two items, add "More..." at the end
                    if (containedPhotoInfo.photoItems.Length >= 3)
                    {
                        targetText.text += ", More...";
                    }
                }

                break;
            case whichDescriptor.Location:
                containedPhotoInfo = targetBehaviour.GetComponent<PhotoStickyButton>().containedPhotoInfo;
                targetText.color = uncategorizedColour;
                targetText.text = containedPhotoInfo.GetLocationString();
                break;
            case whichDescriptor.Date:
                containedPhotoInfo = targetBehaviour.GetComponent<PhotoStickyButton>().containedPhotoInfo;
                targetText.color = uncategorizedColour;
                Debug.Log(containedPhotoInfo.photoTime.ToString());
                targetText.text = containedPhotoInfo.photoTime.ToString();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
