using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target; // The target object to look at

    void Update()
    {
        // Check if the target is not null
        if (target != null)
        {
            // Calculate the direction towards the target, ignoring the Z component
            Vector3 direction = target.position - transform.position;
            direction.z = 0f; // Set the Z component to 0

            // Make this object's transform look at the modified direction
            transform.up = direction.normalized;
        }
        else
        {
            Debug.LogWarning("Target is not assigned!");
        }
    }
}
