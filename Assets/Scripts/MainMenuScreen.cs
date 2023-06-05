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
        SceneManager.LoadScene(2);
    }
    public void BettleLevel()
    {
        SceneManager.LoadScene(3);
    }
    public void LadyLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void PlayButton()
    {
        characterSelectMenu.SetActive(true);
    }


    public void MainMenu()
    {
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