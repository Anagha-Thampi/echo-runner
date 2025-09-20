using UnityEngine;
using UnityEngine.SceneManagement; // Needed to switch scenes

public class LevelLoader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player touches the Goal
        if (other.CompareTag("Goal"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
