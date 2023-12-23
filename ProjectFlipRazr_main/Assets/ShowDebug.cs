using UnityEngine;
using UnityEngine.UI;

public class ShowDebug : MonoBehaviour
{
    public GameObject objectToToggle;

    private void Start()
    {

        // Ensure you have assigned the GameObject in the Unity Editor or through code.
        if (objectToToggle == null)
        {
            Debug.LogError("Please assign the GameObject to toggle in the inspector.");
        }

    }

    public void Awake()
    {
        objectToToggle.SetActive(false);
    }
    public void OnButtonClick()
    {
        // Toggle the active state of the GameObject
        objectToToggle.SetActive(!objectToToggle.activeSelf);
    }
}

