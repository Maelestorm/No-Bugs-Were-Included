using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartFromLadyScene()
    {
        SceneManager.LoadScene(4);
    }
    public void RestartFromBettleScene()
    {
        SceneManager.LoadScene(3);
    }
    public void RestartFromAntonioScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoBackToMenu()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Stop("EndGameMusic");
        }
        SceneManager.LoadScene(0);
    }
}