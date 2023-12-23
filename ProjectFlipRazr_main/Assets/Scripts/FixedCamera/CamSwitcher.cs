using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
public class CamSwitcher : MonoBehaviour
{
    public Transform player;
    public string playerObjectName;
    public CinemachineVirtualCamera activeCam;
    public int priorityWhenOn;
    public int priorityWhenOff;
    private void Start()
    {
        player = GameObject.Find(playerObjectName).GetComponent<Transform>();
        activeCam.Priority = priorityWhenOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerObjectName)
        {
            activeCam.Priority = priorityWhenOn;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == playerObjectName)
        {
            activeCam.Priority = priorityWhenOff;
        }
    }
}
