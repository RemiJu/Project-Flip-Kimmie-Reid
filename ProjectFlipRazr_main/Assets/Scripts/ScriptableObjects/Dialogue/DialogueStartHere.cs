using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue System", menuName = "Dialogue System")]
public class DialogueStartHere : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public int id;
        public string speakerName;
        [TextArea(3, 10)]
        public string text;
        public float durationInSeconds;
        public float hideDelay;
        public bool manualClick;
        public Texture portrait;
        public float portraitRotation = 0;
        public Texture backdrop;
    }

    public bool noMovementWhileDialogue;
    public DialogueLine[] dialogueLines;

    public int GetDialogueLineCount()
    {
        return dialogueLines.Length;
    }

    public DialogueLine GetDialogueLine(int index)
    {
        if (index >= 0 && index < dialogueLines.Length)
        {
            return dialogueLines[index];
        }

        return null; // Or handle this case as appropriate for your application
    }
}
