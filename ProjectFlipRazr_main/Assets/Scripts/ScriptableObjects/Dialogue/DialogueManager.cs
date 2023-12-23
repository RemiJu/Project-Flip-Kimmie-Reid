using System.Collections;
using System.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public float delayBeforePrinting;
    public DialogueStartHere dialogueSystem;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerNameText;
    public RawImage dialogueImage; // Assuming this is the RawImage component for the Sprite
    public float customImageRotation = 0;
    public RawImage dialogueBackdrop;
    public RawImage manualclickImage;
    public bool manualClick;
    public bool destroySelfAtEnd;

    [Header("TextMeshPro Fades")]
    public float textMeshProFadeInTime;
    public float textMeshProFadeOutTime;

    [Header("Sprite/Backdrop Fades")]
    public float rawImageFadeInTime;
    public float rawImageFadeOutTime;
    public float backdropFadeInTime;
    public float backdropFadeOutTime;
    public float backdropAlpha = 0.33f;

    [Header("Ease-in/Ease-out")]
    public bool useAnimation;
    public AnimationCurve easeInCurve; // Assign an easing in curve
    public AnimationCurve easeOutCurve; // Assign an easing out curve
    public Vector2 offset = new Vector2(100, 0); // Example offset
    public float animDuration = 2f; // Duration of the movement

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
            confirmAction = phoneNavigationMap.FindAction("Confirm");

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
        StartCoroutine(DisplayDialogue());

        if (dialogueSystem.noMovementWhileDialogue)
        {
            FindAnyObjectByType<PhoneSwitcher>().DialogueFreeze();
        }
    }

    private void Update()
    {
       
    }

    IEnumerator DisplayDialogue()
    {
        yield return new WaitForSeconds(delayBeforePrinting);

        for (int i = 0; i < dialogueSystem.GetDialogueLineCount(); i++)
        {
            DialogueStartHere.DialogueLine dialogueLine = dialogueSystem.GetDialogueLine(i);

            // Load and set the Sprite for portrait
            if (dialogueLine.portrait != null)
            {
                customImageRotation = dialogueLine.portraitRotation;
                dialogueImage.texture = dialogueLine.portrait; //this is the speaker portrait to display
                dialogueImage.rectTransform.localEulerAngles = new Vector3(0, 0, customImageRotation); //set rotation, if any
            }

            // Set UI Sprite RawImage alpha to 0
            Color rawImageColor = dialogueImage.color;
            //rawImageColor.a = 0f;
            //desiredAlpha = 0f;
            dialogueImage.color = rawImageColor;

            // Set UI text
            dialogueText.text = dialogueLine.text;
            speakerNameText.text = dialogueLine.speakerName;

            // Fade in TextMeshPro and Sprite independently

            StartCoroutine(FadeIn(dialogueImage, rawImageFadeInTime));
            StartCoroutine(FadeIn(dialogueBackdrop, backdropFadeOutTime));
            StartCoroutine(FadeIn(dialogueText, textMeshProFadeInTime));
            StartCoroutine(FadeIn(speakerNameText, textMeshProFadeInTime));
            StartCoroutine(EaseIn(speakerNameText.rectTransform, offset, animDuration, easeInCurve));
            StartCoroutine(EaseIn(dialogueText.rectTransform, offset, animDuration, easeInCurve));
            if(useAnimation)
            {
                StartCoroutine(EaseIn(dialogueBackdrop.rectTransform, offset, animDuration, easeInCurve));
            }

            // Wait for the specified duration
            yield return new WaitForSeconds(dialogueLine.durationInSeconds);

            // Detect whether this line will require a manual click from the scriptable object's declaration
            manualClick = dialogueLine.manualClick;

            // If "Manual Click" is true, you will have to press the Confirm button to move to the next line.
            if (manualClick)
            {
                if (confirmAction == null)
                {
                    Debug.LogError("confirmAction is null. Manual click won't work!");
                    yield break; // exit the coroutine to avoid further issues
                }
                StartCoroutine(FadeIn(manualclickImage, rawImageFadeInTime));
                Debug.Log("Waiting for Confirm button press...");
                yield return new WaitUntil(() => confirmAction.triggered);
                Debug.Log("Confirm button pressed. Continuing...");
            }

            // Fade out TextMeshPro and Sprite independently
            StartCoroutine(FadeOut(dialogueImage, rawImageFadeOutTime));
            StartCoroutine(FadeOut(dialogueBackdrop, backdropFadeOutTime));
            StartCoroutine(FadeOut(dialogueText, textMeshProFadeOutTime));
            StartCoroutine(FadeOut(speakerNameText, textMeshProFadeOutTime));
            StartCoroutine(FadeOut(manualclickImage, rawImageFadeOutTime));
            StartCoroutine(EaseOut(speakerNameText.rectTransform, offset, animDuration, easeOutCurve));
            StartCoroutine(EaseOut(dialogueText.rectTransform, offset, animDuration, easeInCurve));
            if (useAnimation)
            {
                StartCoroutine(EaseOut(dialogueBackdrop.rectTransform, offset, animDuration, easeInCurve));
            }


            // Clear the UI text
            dialogueText.text = "";

            // Hide the text after the specified hide delay or default to 1 second
            float hideDelay = (dialogueLine.hideDelay > 0) ? dialogueLine.hideDelay : 1f;
            yield return new WaitForSeconds(hideDelay);
        }

        // Clear the UI text after all dialogue lines
        ClearDialogue();
        Destroy(gameObject);
        var phoneSwitcher = FindAnyObjectByType<PhoneSwitcher>();
        phoneSwitcher.RefreshPhoneStateReference();
        if (dialogueSystem.noMovementWhileDialogue)
        {
            phoneSwitcher.DialogueUnFreeze();
        }
        Debug.Log(this.gameObject.name + " destroyed.");
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
        float targetAlpha;
        if(rawImage.name == "DialogueBackdrop")
        {
            targetAlpha = backdropAlpha;
        }
        else
        {
            targetAlpha = 1f; // Set the target alpha to 1 (100%)
        }

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
            float alpha;
            if (rawImage.name == "DialogueBackdrop")
            {
                alpha = Mathf.Lerp(backdropAlpha, targetAlpha, elapsedTime / fadeOutTime);
            }
            else
            {
                alpha = Mathf.Lerp(1f, targetAlpha, elapsedTime / fadeOutTime);
            }
            startColor.a = alpha;
            rawImage.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator EaseIn(RectTransform rectTransform, Vector2 offset, float animDuration, AnimationCurve easeInCurve)
    {
        Vector2 originalPosition = rectTransform.anchoredPosition;
        Vector2 startPosition = originalPosition - offset; // Start from offset position

        float time = 0;
        while (time < animDuration)
        {
            time += Time.deltaTime;
            float t = easeInCurve.Evaluate(time / animDuration);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, originalPosition, t);
            yield return null;
        }

        // StartCoroutine(EaseOut(rectTransform, offset, animDuration, easeOutCurve)); // Uncomment if you want to chain to EaseOut
    }

    IEnumerator EaseOut(RectTransform rectTransform, Vector2 offset, float animDuration, AnimationCurve easeOutCurve)
    {
        Vector2 originalPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = originalPosition; // End at offset position

        float time = 0;
        while (time < animDuration)
        {
            time += Time.deltaTime;
            float t = easeOutCurve.Evaluate(time / animDuration);
            rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, endPosition, t);
            yield return null;
        }
    }



    public void ClearDialogue()
    {
        // Set UI sprite alpha
        Color spriteColor = dialogueImage.color;
        dialogueImage.color = spriteColor;
        spriteColor.a = 0;
        //wipe text
        dialogueText.text = "";
    }

    void OnConfirmButtonPressed()
    {
        Debug.Log("Confirm button pressed!");
    }

}