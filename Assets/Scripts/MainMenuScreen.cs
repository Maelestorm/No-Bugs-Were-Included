using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}