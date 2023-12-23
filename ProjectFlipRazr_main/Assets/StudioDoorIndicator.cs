using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StudioDoorIndicator : MonoBehaviour
{
    public PuzzleManager manager;
    public GameObject[] thingsToSetActive_PuzzleProg1;
    public GameObject[] thingsToSetActive_PuzzleProg2;
    public GameObject[] thingsToSetActive_PuzzleProg3;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindAnyObjectByType<PuzzleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPuzzleProg();
    }

    void CheckForPuzzleProg()
    {
        if (manager.puzzleProgression == 1)
        {
            foreach (GameObject obj in thingsToSetActive_PuzzleProg1)
            {
                gameObject.SetActive(true);
            }
        }
        if (manager.puzzleProgression == 2)
        {
            foreach (GameObject obj in thingsToSetActive_PuzzleProg2)
            {
                gameObject.SetActive(true);
            }
        }
        if (manager.puzzleProgression == 3)
        {
            foreach (GameObject obj in thingsToSetActive_PuzzleProg3)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
