using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float origineTime;
    [HideInInspector] public float time;
    public GameObject dizaine;
    public GameObject unite;
    [HideInInspector] public Coroutine countDown;

    public Reset reset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = origineTime;

        countDown = StartCoroutine(CountDown());
    }

    public void StopCountDown()
    {
        if (countDown != null) 
        {
            StopCoroutine(countDown);
            countDown = null;
        }
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

                Debug.Log(time);
                yield return new WaitForSeconds(1.0f);
                time --;
            }
            reset.ResetPlayer();
            reset.ResetScene();
            break;
        }
        
    }
}
