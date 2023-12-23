using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lazy Tutorial", menuName = "Tutorial")]
public class LazyTutorial : ScriptableObject
{
    [System.Serializable]
    public class TutorialLine
    {
        public int id;
        [TextArea(3, 10)]
        public string text;
        public float durationInSeconds;
        public float hideDelay;
        public bool isInfinite;
        public Texture accompanyingImage;
    }

    public TutorialLine[] tutorialLines;

    public int GetTutorialLineCount()
    {
        return tutorialLines.Length;
    }

    public TutorialLine GetTutorialLine(int index)
    {
        if (index >= 0 && index < tutorialLines.Length)
        {
            return tutorialLines[index];
        }

        return null; // Or handle this case as appropriate for your application
    }
}
