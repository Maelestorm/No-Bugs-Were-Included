using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartFromSceneOne()
    {
        SceneManager.LoadScene(1);
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