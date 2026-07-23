using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Reset reset;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            death();
        }

    }

    private void death()
    {
        health = maxHealth;
        reset.ResetPlayer();
        reset.ResetScene();
    }
}
