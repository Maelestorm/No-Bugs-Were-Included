using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScreen : MonoBehaviour
{
    public GameObject characterSelectMenu;
    public Button antButton;
    public Button bettleButton;
    public Button ladyButton;

    void Start()
    {
        characterSelectMenu.SetActive(false);
        ladyButton.onClick.AddListener(LadyLevel);
        antButton.onClick.AddListener(AntLevel);
        bettleButton.onClick.AddListener(BettleLevel);
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (characterSelectMenu.activeSelf)
        {
            characterSelectMenu.SetActive(false);
        }
    }
}
    public void AntLevel()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("ButtonClick");
            audioManager.Stop("MainMenuMusic");
        }
        SceneManager.LoadScene(2);
        audioManager.Play("ThemeSong");
    }
    public void BettleLevel()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("ButtonClick");
            audioManager.Stop("MainMenuMusic");
        }
        SceneManager.LoadScene(3);
        audioManager.Play("ThemeSong");
    }
    public void LadyLevel()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("ButtonClick");
            audioManager.Stop("MainMenuMusic");
        }
        SceneManager.LoadScene(4);
        audioManager.Play("ThemeSong");
    }

    public void PlayButton()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("ButtonClick");
        }
        characterSelectMenu.SetActive(true);
    }


    public void MainMenu()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("ButtonClick");
        }
        SceneManager.LoadScene(0);
    }
    public void QuitGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}