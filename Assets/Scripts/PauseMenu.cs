using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button quitButton;

    void Start()
    {
        pauseMenu.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame(); 
            }
        }
    }

   
    void PauseGame()
    {
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; 
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}

