using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float origineTime;
    [HideInInspector] public float time;
    public GameObject dizaine;
    public GameObject unite;

    public Reset reset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = origineTime;

        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
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
            time -= 1;
        }
        reset.ResetPlayer(origineTime);
        reset.ResetScene();
    }
}
