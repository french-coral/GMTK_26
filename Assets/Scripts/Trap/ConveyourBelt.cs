using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ConveyourBelt : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject G in onBelt) 
        {
            //G.GetComponent<Rigidbody>().linearVelocity = speed * direction;
            Rigidbody rb = G.GetComponent<Rigidbody>();

            if (rb != null )
            {
                rb.AddForce(direction *  speed, ForceMode.Acceleration);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
