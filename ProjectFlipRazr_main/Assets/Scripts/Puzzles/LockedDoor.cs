using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public Animator animatorL;
    public Animator animatorR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        animatorL.SetTrigger("OpenL");
        animatorR.SetTrigger("OpenR");
    }
}
