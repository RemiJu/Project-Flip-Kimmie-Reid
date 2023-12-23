using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    MoveInputManager moveInputManager;
    PlayerMovement2 playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        moveInputManager = GetComponent<MoveInputManager>();
        playerMovement = GetComponent<PlayerMovement2>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInputManager.HandleAllInputs();
    }

    void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }
}
