using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1Continue : MonoBehaviour
{
    public DramaManager dramaManager;
    public DramaSystem dramaSystem;
    public int desiredQuestToProgress;
    // Start is called before the first frame update
    void Start()
    {
        dramaSystem = dramaManager.dramaSystem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            var questManager = GameObject.Find("DramaManager").GetComponent<DramaManager>();
            questManager.UpdateQuestProgression(dramaSystem.quests[desiredQuestToProgress]);
        }
    }
}
