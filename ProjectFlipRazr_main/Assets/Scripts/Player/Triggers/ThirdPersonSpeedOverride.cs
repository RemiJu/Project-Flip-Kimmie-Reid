using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonSpeedOverride : MonoBehaviour
{
    private PlayerControl playerControl; //Third person character controller
    public string playerTag;
    public float desiredSpeedOverride;
    public float previousSpeedValue;
    private bool playerAssigned;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("AssignPlayer", 0.05f); 
        //my extremely lazy solution to making sure this script only looks for Kimmie AFTER everything's done loading to prevent it returning null.

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag && playerAssigned)
        {
            HandleSpeedOverride();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == playerTag && playerAssigned)
        {
            playerControl.baseSpeed = previousSpeedValue;
            Debug.Log("Movement Speed Override returned to previous value: " + previousSpeedValue);
        }
    }

    void AssignPlayer()
    {
        playerControl = GlobalPlaytestSettings.instance.player.GetComponent<PlayerControl>();  //Retrieves PlayerControl.cs reference by accessing Player GameObject reference in GlobalPlaytestSettings instance.
        if (playerControl != null) Debug.Log("Kimmie Reid GameObject has been assigned for movement override trigger: " + this.gameObject.name);
        playerAssigned = true;

        //The following checks if the player is already standing in the trigger on Start.
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, ~0, QueryTriggerInteraction.Collide);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(playerTag))
            {
                // Player is inside the trigger
                HandleSpeedOverride();
                break; // Exit the loop once the player is found
            }
        }
    }

    void HandleSpeedOverride()
    {
        previousSpeedValue = playerControl.baseSpeed;
        playerControl.baseSpeed = desiredSpeedOverride;
        Debug.Log("Movement Speed Override in effect. New movement speed has been set to " + playerControl.baseSpeed + " from previous value of " + previousSpeedValue);
    }
}
