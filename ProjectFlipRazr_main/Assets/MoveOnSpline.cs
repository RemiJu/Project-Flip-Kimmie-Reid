using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
    public SplineContainer spline;
    public float moveSpeed = 1f;
    public float rotationSpeed = 5f;

    private float currentDistance = 0f;

    void Update()
    {
        // Calculate the target position on the spline
        Vector3 targetPosition = spline.EvaluatePosition(currentDistance);

        // Move the character towards the target position on the spline
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Calculate the target rotation on the spline
        Vector3 targetDirection = spline.EvaluateTangent(currentDistance);

        // Rotate the character towards the target rotation on the spline
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // If the end of the spline is reached, loop back to the beginning
        if (currentDistance >= 1f)
        {
            currentDistance = 0f;
        }
        else
        {
            // Adjust the movement based on the length of the spline
            float splineLength = spline.CalculateLength();
            float movement = moveSpeed * Time.deltaTime / splineLength;
            currentDistance += movement;
        }
    }
}