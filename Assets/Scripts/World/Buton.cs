using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Buton : MonoBehaviour
{
    public Door door;
    private bool doorExist = true;
    private bool buttonDown = false;
    public GameObject button;

    private void OnCollisionEnter(Collision collision)
    {
        if (buttonDown == false) 
        {
            button.transform.Translate(0, -0.1f, 0);
            buttonDown = true;
        }

        if (doorExist == true)
        {
            door.OpenTheDoor();
            doorExist = false;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        button.transform.Translate(0, 0.1f, 0);
        buttonDown = false;
    }

}
