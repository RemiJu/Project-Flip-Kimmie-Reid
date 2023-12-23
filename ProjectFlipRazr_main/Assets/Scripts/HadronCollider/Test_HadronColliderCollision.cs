using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HadronColliderCollision : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color newColor = Color.red; // The new color for the material
    public int triggerCount = 0; // Variable to count the number of triggers

    public Color[] materialColors = new Color[]         {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        Color.white,
        Color.gray,
        Color.black
    };

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "HadronParticle")
        {
            // Change material color

            if (meshRenderer != null)
            {
                int randomIndex = Random.Range(0, materialColors.Length);
                meshRenderer.material.color = materialColors[randomIndex];

            }
            triggerCount++;

            //Debug.Log("Collision Count: " + triggerCount);
        }
    }
}

