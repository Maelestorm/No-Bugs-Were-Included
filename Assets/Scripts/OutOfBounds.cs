using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OutOfBounds : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadEndGame(); 
        }
    }
    private void LoadEndGame()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Stop("ThemeSong");
            audioManager.Stop("BossMusic");
        }
        SceneManager.LoadScene(1);
        audioManager.Play("EndGameMusic");
    }
}
