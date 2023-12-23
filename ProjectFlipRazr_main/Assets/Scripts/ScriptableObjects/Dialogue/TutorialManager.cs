using System.Collections;
using System.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    public float delayBeforePrinting;
    public LazyTutorial tutorialSystem;
    public TextMeshProUGUI tutorialText;
    public RawImage displayedImage; // Assuming this is the RawImage component for the Sprite
    public bool isInfinite;

    // TextMeshPro fade times
    public float textMeshProFadeInTime;
    public float textMeshProFadeOutTime;

    // Sprite fade times
    public float rawImageFadeInTime;
    public float rawImageFadeOutTime;



    private DefaultControls controls;
    private InputAction confirmAction;

    void Start()
    {
        controls = new DefaultControls();

        // Assuming "PhoneNavigation" is the name of your action map
        InputActionMap phoneNavigationMap = controls.PhoneNavigation;

        if (phoneNavigationMap != null)
        {
            // Assuming "Confirm" is the name of your action within that map
            InputAction confirmAction = phoneNavigationMap.FindAction("Confirm");

            if (confirmAction != null)
            {
                // Subscribe to the button press event
                confirmAction.started += ctx => OnConfirmButtonPressed();
            }
            else
            {
                Debug.LogError("Confirm action not found!");
            }
        }
        else
        {
            Debug.LogError("PhoneNavigation action map not found!");
        }

        controls.Enable();
        StartCoroutine(DisplayTutorial());
    }

    private void Update()
    {
       
    }

    IEnumerator DisplayTutorial()
    {
        yield return new WaitForSeconds(delayBeforePrinting);

        for (int i = 0; i < tutorialSystem.GetTutorialLineCount(); i++)
        {
            LazyTutorial.TutorialLine dialogueLine = tutorialSystem.GetTutorialLine(i);

            // Load and set the Sprite for portrait
            if (dialogueLine.accompanyingImage != null)
            {
                displayedImage.texture = dialogueLine.accompanyingImage;
            }

            // Set UI Sprite RawImage alpha to 0
            Color rawImageColor = displayedImage.color;
            //rawImageColor.a = 0f;
            //desiredAlpha = 0f;
            displayedImage.color = rawImageColor;

            // Set UI text
            tutorialText.text = $"{dialogueLine.text}";

            // Fade in TextMeshPro and Sprite independently

            yield return StartCoroutine(FadeIn(displayedImage, rawImageFadeInTime));
            yield return StartCoroutine(FadeIn(tutorialText, textMeshProFadeInTime));


            // Wait for the specified duration
            yield return new WaitForSeconds(dialogueLine.durationInSeconds);

            // Detect whether this line will require a manual click from the scriptable object's declaration
            isInfinite = dialogueLine.isInfinite;

            // If "Manual Click" is true, you will have to press the Confirm button to move to the next line.
            if (!isInfinite)
            {
                // Fade out TextMeshPro and Sprite independently
                yield return StartCoroutine(FadeOut(displayedImage, rawImageFadeOutTime));
                yield return StartCoroutine(FadeOut(tutorialText, textMeshProFadeOutTime));


                // Clear the UI text
                tutorialText.text = "";

                // Hide the text after the specified hide delay or default to 1 second
                float hideDelay = (dialogueLine.hideDelay > 0) ? dialogueLine.hideDelay : 1f;
                yield return new WaitForSeconds(hideDelay);


                // Clear the UI text after all dialogue lines
                ClearTutorial();
            }

        }


    }

    IEnumerator FadeIn(Graphic graphic, float fadeInTime)
    {
        float elapsedTime = 0f;
        Color startColor = graphic.color;
        float targetAlpha = 1f; // Set the target alpha

        while (startColor.a < targetAlpha)
        {
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsedTime / fadeInTime);
            startColor.a = alpha;
            graphic.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeIn(RawImage rawImage, float fadeInTime)
    {
        float elapsedTime = 0f;
        Color startColor = rawImage.color;
        float targetAlpha = 1f; // Set the target alpha

        while (startColor.a < targetAlpha)
        {
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsedTime / fadeInTime);
            startColor.a = alpha;
            rawImage.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(Graphic graphic, float fadeOutTime)
    {
        float elapsedTime = 0f;
        Color startColor = graphic.color;
        float targetAlpha = 0f; // Set the target alpha

        while (startColor.a > targetAlpha)
        {
            float alpha = Mathf.Lerp(1f, targetAlpha, elapsedTime / fadeOutTime);
            startColor.a = alpha;
            graphic.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(RawImage rawImage, float fadeOutTime)
    {
        float elapsedTime = 0f;
        Color startColor = rawImage.color;
        float targetAlpha = 0f; // Set the target alpha

        while (startColor.a > targetAlpha)
        {
            float alpha = Mathf.Lerp(1f, targetAlpha, elapsedTime / fadeOutTime);
            startColor.a = alpha;
            rawImage.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void ClearTutorial()
    {
        // Set UI sprite alpha
        Color spriteColor = displayedImage.color;
        displayedImage.color = spriteColor;
        spriteColor.a = 0;
        //wipe text
        tutorialText.text = "";
    }

    void OnConfirmButtonPressed()
    {
        Debug.Log("Confirm button pressed!");
    }

}