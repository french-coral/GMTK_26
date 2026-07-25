using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Coroutine open;
    private Coroutine close;
    private float position;

    private void Start()
    {
        position = gameObject.transform.position.y;
    }

    public void OpenTheDoor()
    {
        if (close != null)
        {
            StopCoroutine(close);
        }

        open = StartCoroutine(Open());
    }

    public void CloseTheDoor()
    {
        if (open != null)
        {
            StopCoroutine(open);
        }

        close = StartCoroutine(Close());
    }

    private IEnumerator Open()
    {

        while (gameObject.transform.position.y != position + 15) 
        {
            Debug.Log("plouf");
            gameObject.transform.Translate(0, 0.5f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Close()
    {
        while (gameObject.transform.position.y != position)
        {
            gameObject.transform.Translate(0, -0.5f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
