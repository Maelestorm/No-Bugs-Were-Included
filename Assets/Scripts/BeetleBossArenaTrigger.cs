using UnityEngine;
using UnityEngine.SceneManagement;

public class BeetleBossArenaTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Stop("ThemeSong");
            }
            SceneManager.LoadScene(7);
            if (audioManager != null)
            {
                audioManager.Play("CaveLoadingSound");
            }
            
        }
    }
}
