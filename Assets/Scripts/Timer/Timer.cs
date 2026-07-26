using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Time init
    public float origineTime;
    [HideInInspector] public float time;

    // Canva number for UI
    public GameObject dizaine;
    public GameObject unite;

    // Countdown ref
    [HideInInspector] public Coroutine countDown;

    // Reset script ref
    public Reset reset;


    void Start()
    {
        time = origineTime;

        countDown = StartCoroutine(CountDown());
    }

    public void Restart()
    {
        if (countDown != null)
        {
            StopCoroutine(countDown);
            countDown = null;
        }
        time = origineTime;
        countDown = StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        while (true) 
        {
            while (time > 0.0f)
            {
                if (time > 9.0f)
                {
                    int dizaine_ = Mathf.FloorToInt(time / 10);
                    int unite_ = Mathf.FloorToInt(time) - dizaine_ * 10;
                    dizaine.GetComponent<TMPro.TMP_Text>().text = dizaine_.ToString();
                    unite.GetComponent<TMPro.TMP_Text>().text = unite_.ToString();
                }

                else
                {
                    dizaine.GetComponent<TMPro.TMP_Text>().text = "0";
                    unite.GetComponent<TMPro.TMP_Text>().text = time.ToString();
                }

                yield return new WaitForSeconds(1.0f);
                time --;
            }
            reset.ResetPlayer();
            reset.ResetScene();

            yield break;
        }
        
    }
}
