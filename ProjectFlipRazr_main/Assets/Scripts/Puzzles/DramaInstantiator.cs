using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DramaInstantiator : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public bool requireCamera;
    public FlipPhoneManager flipPhoneManager;
    public enum whichPlayer
    {
        FirstPerson,
        ThirdPerson
    }
    public float cooldownDuration = 2f;  // Cooldown duration in seconds

    private bool canTrigger = true;
    public whichPlayer player;
    private void OnTriggerEnter(Collider other)
    {
        switch (player)
        {
            case whichPlayer.FirstPerson:
                if(other.CompareTag("Player") && canTrigger)
                {
                    // Instantiate the prefab
                    Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
                    Debug.Log(prefabToInstantiate + " has been instantiated");

                    // Set cooldown
                    StartCoroutine(Cooldown());
                }
                break;
            case whichPlayer.ThirdPerson:
                if(other.gameObject.name == ("3rdPersonPlayer") && canTrigger)
                {
                    //Instantiate the prefab
                    Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
                    Debug.Log(prefabToInstantiate + " has been instantiated");

                    // Set cooldown
                    StartCoroutine(Cooldown());            // Set cooldown
                }
                break;
        }
    }

    private IEnumerator Cooldown()
    {
        canTrigger = false;
        yield return new WaitForSeconds(cooldownDuration);
        canTrigger = true;
    }
}
