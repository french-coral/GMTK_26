using UnityEngine;
using UnityEngine.TextCore.Text;

public class CadavreDeceleration : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movements")]
    [SerializeField] private float deceleration = 0.0f;

    [Header("Gravity")]
    [SerializeField] private float gravityFallMultiplier = 20.0f;
    [SerializeField] private float lowJumpGravityMultiplier = 8.0f;
    [SerializeField] private float baseMultiplier = 4.0f;
    [SerializeField] private float baseGravity = 9.81f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleGravity();
    }

    void HandleGravity()
    {
        float multiplier = baseMultiplier;

        if (rb.linearVelocity.y < 0.0f)
        {
            multiplier = gravityFallMultiplier;
        } 
        else
        {
            multiplier = baseMultiplier;
        }

        rb.AddForce(Vector3.down * multiplier * baseGravity, ForceMode.Acceleration);

    }
}
