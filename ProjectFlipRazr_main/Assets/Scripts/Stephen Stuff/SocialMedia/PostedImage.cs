using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostedImage : ScriptableObject
{
    public List<string> Comments = new List<string>();

    public GameObject Parent;
}
