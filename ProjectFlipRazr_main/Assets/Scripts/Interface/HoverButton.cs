using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI targetText;
    public string desiredString;
    public string desiredExitString;
    // This method will be called when the mouse pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Place your code here to execute when the button is hovered
        targetText.text = desiredString;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetText.text = desiredExitString;
    }

}

