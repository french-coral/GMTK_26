using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDeath : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Reset reset;
    public Timer timer;
    private InputAction suicide;
    private float nextSuicide = 1.0f;
    private InputAction clear;
    private float nextClear = 1.0f;

    private void Start()
    {
        suicide = InputSystem.actions.FindAction("Attack");
        clear = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (suicide.IsPressed() && nextSuicide <= Time.time)
        {
            nextSuicide = Time.time + 1.0f;
            death();
        }

        if (clear.IsPressed() && nextClear <= Time.time)
        {
            nextClear = Time.time + 1.0f;
            reset.ResetBodies();
        }
    }

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
