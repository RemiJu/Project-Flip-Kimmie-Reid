using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Drama System/Quest", order = 1)]
public class DramaSystem : ScriptableObject
{
    public enum ActionOption
    {
        None,
        Instantiate,
    }

    [System.Serializable]
    public class Progression
    {
        public string objectiveDescription;
        public GameObject[] prefabObjects; // These could be anything, i.e., triggers.
        public Vector3[] spawnPositions; // The location for the above prefab objects.
        public ActionOption[] prefabActions; // What to do with each of the prefabs above.
        public DialogueStartHere setDialogueTo; // Changes the dialogue scriptable object for when dialogue prefab is spawned
    }

    [System.Serializable]
    public class Quest
    {
        public string name;
        public int totalProgressionStages; // This should equal the length of the 'progress' array.
        public Progression[] progress; // Each element represents a stage in the quest.
        public GameObject[] uiCanvasPrefabs; // Any UI stuff.
        public string developerComments; // Space to explain stuff that the player won't see.
        public bool isCompleted;
        public bool GiveRewardOnCompletion;
    }

    public Quest[] quests;

    public int GetQuestCount()
        {
            return quests.Length;
        }

    public int GetQuestProgressionGoal(int index)
    {
        if (index >= 0 && index < quests.Length) // are we within the bounds of array
        {
            return quests[index].totalProgressionStages;
        }
        else
        {
            return -1; // returns -1 if things are busted somehow
        }
    }

}
