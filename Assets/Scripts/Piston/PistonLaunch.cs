using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PistonLaunch : MonoBehaviour
{
    [SerializeField] private Piston piston;

    void OnCollisionEnter(Collision collision)
    {
        TryLaunch(collision);
    }

    void OnCollisionStay(Collision collision)
    {

        TryLaunch(collision);
    }

    private void TryLaunch(Collision collision)
    {

        Rigidbody otherRb = collision.rigidbody;
        if (otherRb == null) return;

        // Is the piston moving in its launch direction?
        float speedAlongLaunch = Vector3.Dot(piston.Velocity, piston.LaunchDirection);

        if (speedAlongLaunch < piston.MinLaunchSpeed) return;

        otherRb.AddForce(piston.LaunchDirection * speedAlongLaunch, ForceMode.VelocityChange);

    }
}
