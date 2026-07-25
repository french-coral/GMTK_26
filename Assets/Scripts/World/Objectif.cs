using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectif : MonoBehaviour
{
    public GameObject timer;
    public GameObject victoryScreen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            timer.SetActive(false);
            victoryScreen.SetActive(true);
        }
    }

}
