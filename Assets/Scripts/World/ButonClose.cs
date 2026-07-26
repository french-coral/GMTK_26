using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ButonClose : MonoBehaviour
{
    public Door door;
    private bool buttonDown = false;
    public GameObject button;
    public float wait;

    private void OnCollisionEnter(Collision collision)
    {
        if (buttonDown == false)
        {
            button.transform.Translate(0, -0.1f, 0);
            buttonDown = true;
        }

        door.buttonPressed = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        button.transform.Translate(0, 0.1f, 0);
        buttonDown = false;
        StartCoroutine(Delais());
    }

    private IEnumerator Delais()
    {
        yield return new WaitForSeconds(wait);
        door.buttonPressed = false;
    } 
}
