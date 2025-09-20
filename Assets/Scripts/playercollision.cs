using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If player hits echo -> Game Over
        if (collision.gameObject.CompareTag("Echo"))
        {
            gameManager.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            gameManager.GameOver();
        }

        if (other.CompareTag("Goal"))
        {
            gameManager.LevelComplete();
        }
    }
}
