using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhotoItem : MonoBehaviour
{
    public PhotoInfo.PhotoItem photoItem;
    public DialogueStartHere SpawnDialogue;
    public bool progressQuest;
    public int questNumberToProgress;
    public bool questProgressionDone;
    public bool destroyIfPhotoTaken;
}
