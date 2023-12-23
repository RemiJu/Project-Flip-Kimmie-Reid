using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhotoInfo;

public class PuzzleManager : MonoBehaviour
{
    public enum PuzzleLocation
    {
        StudioStage56,

    }

    public PuzzleLocation puzzleLocation;
    public PhotoInfoDatabase photoInfoDatabase;

    public int puzzleProgression;
    public int puzzleCompletionNum;
    public bool puzzleCompleted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleProgression == puzzleCompletionNum)
        {
            if (!puzzleCompleted)
            {
                OnPuzzleCompletion();
                puzzleCompleted = true;
            }
        }
    }

    public void CheckPhotoItemProgression(PhotoInfo.PhotoItem photoItem)
    {
        switch (puzzleLocation)
        {
            case PuzzleLocation.StudioStage56:
                if (photoItem == PhotoInfo.PhotoItem.RingImprint)
                {
                    if (!GlobalPlaytestSettings.instance.hasRingImprint)
                    {
                        GlobalPlaytestSettings.instance.hasRingImprint = true;
                        CheckProgression();
                    }
                }
                if (photoItem == PhotoInfo.PhotoItem.OfficeKey)
                {
                    if (!GlobalPlaytestSettings.instance.hasOfficeKey)
                    {
                        GlobalPlaytestSettings.instance.hasOfficeKey = true;
                        CheckProgression();
                    }
                }
                if (photoItem == PhotoInfo.PhotoItem.DirectorPhotoFrame)
                {
                    if (!GlobalPlaytestSettings.instance.hasDirectorPhotoFrame)
                    {
                        GlobalPlaytestSettings.instance.hasDirectorPhotoFrame = true;
                        CheckProgression();
                    }
                }
                break;
        }
    }

    public void ResetPhotoItemBools()
    {
        switch (puzzleLocation)
        {
            case PuzzleLocation.StudioStage56:
                GlobalPlaytestSettings.instance.hasRingImprint = false;
                GlobalPlaytestSettings.instance.hasOfficeKey = false;
                GlobalPlaytestSettings.instance.hasDirectorPhotoFrame = false;
                break;
        }
    }

    public void CheckPhotoItemInHand()
    {
        ResetPhotoItemBools();
        switch (puzzleLocation)
        {
            case PuzzleLocation.StudioStage56:

                foreach (PhotoInfo info in photoInfoDatabase.photos)
                {
                    if (info.photoItems.Length > 0)
                    {
                        for (int i = 0; i < info.photoItems.Length; i++)
                        {
                            if (info.photoItems[i] == PhotoInfo.PhotoItem.RingImprint)
                            {
                                GlobalPlaytestSettings.instance.hasRingImprint = true;
                            }
                            if (info.photoItems[i] == PhotoInfo.PhotoItem.OfficeKey)
                            {
                                GlobalPlaytestSettings.instance.hasOfficeKey = true;
                            }
                            if (info.photoItems[i] == PhotoInfo.PhotoItem.DirectorPhotoFrame)
                            {
                                GlobalPlaytestSettings.instance.hasDirectorPhotoFrame = true;
                            }
                        }
                    }
                }
                CheckProgression();
                break;
        }
    }

    public void CheckProgression()
    {
        puzzleProgression = 0;

        switch (puzzleLocation)
        {
            case PuzzleLocation.StudioStage56:
                if (GlobalPlaytestSettings.instance.hasRingImprint)
                {
                    puzzleProgression++;
                }
                if (GlobalPlaytestSettings.instance.hasOfficeKey)
                {
                    puzzleProgression++;
                }
                if (GlobalPlaytestSettings.instance.hasDirectorPhotoFrame)
                {
                    puzzleProgression++;
                }
                break;
        }
    }

    public void OnPuzzleCompletion()
    {
        switch (puzzleLocation)
        {
            case PuzzleLocation.StudioStage56:
                //Code to run when puzzle completed
                Debug.Log("Puzzle Completed");
                //Completion Canvas Set Active
                break;
        }
    }
}
