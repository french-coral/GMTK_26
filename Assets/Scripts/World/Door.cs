using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Door : MonoBehaviour
{
    public SplineContainer spline;
    public float speed;

    private float t = 0f;
    public bool buttonPressed;

    private void Update()
    {
        if (buttonPressed)
        {
            t += speed * Time.deltaTime;
        }

        else
        {
            t -= speed * Time.deltaTime;
        }

        t = Mathf.Clamp01(t);

        transform.position = spline.EvaluatePosition(t);
    }
}
