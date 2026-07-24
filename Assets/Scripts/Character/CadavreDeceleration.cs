using UnityEngine;
using UnityEngine.TextCore.Text;

public class CadavreDeceleration : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movements")]
    [SerializeField] private float deceleration = 60.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovements();
    }

    void HandleMovements()
    {
        float movement = -rb.linearVelocity.x * deceleration * Time.fixedDeltaTime;
        rb.AddForce(Vector3.right * movement, ForceMode.VelocityChange);
    }
}
