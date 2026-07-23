using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float origineTime;
    [HideInInspector] public float time;

    public Reset reset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = origineTime;

        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator CountDown()
    {
        while (time > 0.0f)
        {
            Debug.Log(time);
            yield return new WaitForSeconds(1.0f);
            time -= 1;
        }
        reset.ResetPlayer(origineTime);
        reset.ResetScene();
    }
}
