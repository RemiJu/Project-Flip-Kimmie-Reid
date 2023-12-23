using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poses : MonoBehaviour
{
    public Animator animo;
    public int poseCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        animo = gameObject.GetComponent<Animator>();
        if (poseCount == 0)
        {
            animo.SetTrigger("Pose Type A");
        } else if (poseCount == 1)
        {
            animo.SetTrigger("Pose Type B");
        } else if (poseCount == 2)
        {
            animo.SetTrigger("Pose Type C");
        }
        else if (poseCount == 3)
        {
            animo.SetTrigger("Pose Type D");
        } else if (poseCount == 4)
        {
            animo.SetTrigger("Pose Type E");
        }

    }
}
