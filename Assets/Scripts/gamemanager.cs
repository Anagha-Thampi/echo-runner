using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart current level
    }

    public void LevelComplete()
    {
        Debug.Log("Level Complete!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // For now, just restart
    }
}