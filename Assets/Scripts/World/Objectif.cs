using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectif : MonoBehaviour
{
    public GameObject timer;
    public GameObject victoryScreen;

    [SerializeField] private float spinSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            timer.SetActive(false);
            victoryScreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(45.0f * Time.fixedDeltaTime * spinSpeed, 45.0f * Time.fixedDeltaTime * spinSpeed, 0.0f, Space.Self);
    }

}
