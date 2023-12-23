using UnityEngine;

public class DrawLineGizmo : MonoBehaviour
{
    public float lineLength = 5f;

    void OnDrawGizmos()
    {
        // Draw a line in the forward direction of the object
        Gizmos.color = Color.yellow;  // Set the color of the gizmo line
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * lineLength);
    }
}
