using NUnit.Framework.Internal;
using System.Dynamic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Reset reset;
    [SerializeField] private GameObject totalDeath;
    [SerializeField] private GameObject currentDeath;

    private void OnEnable()
    {
        totalDeath.GetComponent<TMPro.TMP_Text>().text = reset.totalBodies.ToString();
        currentDeath.GetComponent<TMPro.TMP_Text>().text = reset.onScreenBodies.ToString();
    }

    public void NextLevel()
    {
        Debug.Log("plouf");
        SceneManager.LoadScene("MenueStart");
    }
}
