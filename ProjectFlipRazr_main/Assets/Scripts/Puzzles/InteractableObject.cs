using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    public enum Object
    {
        SpawnDialogue,
        Door,
        AccessToStudio,
    }

    public Object obj;
    public GameObject player;
    public PhotoInfoDatabase photoInfoDatabase;
    public DialogueStartHere setDialogueTo;
    public GameObject dialoguePrefab;
    public bool doorNeedsKey;

    private InputAction confirmAction;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();

        InputActionMap gameInputActions = playerInput.currentActionMap;

        if (gameInputActions != null)
        {
            confirmAction = gameInputActions.FindAction("Pickup");

            if (confirmAction != null)
            {
                // Subscribe to the button press event
                confirmAction.started += ctx => Interact();
            }
            else
            {
                Debug.LogError("Pickup action not found!");
            }
        }
        else
        {
            Debug.LogError("gameInputActions action map not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 2f)
        {
            confirmAction.Enable();
        }
        else { confirmAction.Disable(); }
    }

    public void Interact()
    {
        switch (obj)
        {
            case Object.SpawnDialogue:
                dialoguePrefab.GetComponent<DialogueManager>().dialogueSystem = setDialogueTo;
                Instantiate(dialoguePrefab);
                Destroy(this);
                break;
            case Object.Door:
                if (doorNeedsKey)
                {
                    foreach (PhotoInfo info in photoInfoDatabase.photos)
                    {
                        if (info.photoItems.Length > 0)
                        {
                            for (int i = 0; i < info.photoItems.Length; i++)
                            {
                                if (info.photoItems[i] == PhotoInfo.PhotoItem.OfficeKey)
                                {
                                    gameObject.GetComponent<LockedDoor>().Unlock();
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    gameObject.GetComponent<LockedDoor>().Unlock();
                }
                break;
            case Object.AccessToStudio:
                GlobalPlaytestSettings.instance.hasStudio = true;
                Destroy(gameObject);
                break;
        }
    }
}
