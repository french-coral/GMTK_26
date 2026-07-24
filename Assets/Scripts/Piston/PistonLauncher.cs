using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PistonLauncher : MonoBehaviour
{
    [SerializeField] private PistonMotor motor;

    [Header("Launch")]
    [SerializeField] private float launchMultiplier = 1.0f;
    [SerializeField] private float minimumLaunchSpeed = 5.0f;

    private readonly HashSet<Rigidbody> launchedBodies = new();

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!motor.IsExtending)
            return;

        if (motor.CurrentSpeed < minimumLaunchSpeed)
            return;

        Rigidbody rb = other.attachedRigidbody;

        if (rb == null)
            return;

        if (launchedBodies.Contains(rb))
            return;

        Launch(rb);

        launchedBodies.Add(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null)
            launchedBodies.Remove(rb);
    }

    private void Launch(Rigidbody rb)
    {
        Vector3 launchVelocity = motor.LaunchDirection * motor.CurrentSpeed * launchMultiplier;
        
        Debug.Log($"Launch = {launchVelocity}");

        rb.linearVelocity = launchVelocity;
    }
}