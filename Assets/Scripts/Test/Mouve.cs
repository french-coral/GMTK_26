using UnityEngine;

public class Mouve : MonoBehaviour
{
    public Rigidbody rb;
    private float move = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 next = new Vector3(rb.position.x + move, rb.position.y, rb.position.z);
        transform.position = next;
    }
}
