using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameSystem gameSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameSystem.AddScore(10);

            Destroy(gameObject);
        }
    }
}