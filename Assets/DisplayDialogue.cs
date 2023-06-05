using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DisplayDialogue : MonoBehaviour
{
    public GameObject pressEButton;
    public BoxCollider2D harubyCollider;
    public GameObject DialogueCanvas;
    public Text dialogueText;
    public float letterDelay = 0.1f;
    public bool messageDisplayed = false;
    public LadyBugMovementScript ladyBug;
    private bool isDialogueStarted = false;
    public GameObject pressEtoInteractDisplay;

    private void Start()
    {
        pressEButton.SetActive(false);
        DialogueCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !messageDisplayed)
        {
            pressEButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressEButton.SetActive(false);
        }
    }

    void StartDialogue()
    {
        // Time.timeScale = 0;
        ladyBug.enabled = false;
        DialogueCanvas.SetActive(true);
        if (!isDialogueStarted)
        {
            StartCoroutine(DisplayTextLetterByLetter());
            isDialogueStarted = true;
        }
    }

    IEnumerator DisplayTextLetterByLetter()
    {
        dialogueText.text = ""; // Clear the text initially
        string originalText = "selamın aleyküm alyküm selam naber ii sen iiinaber ii esen iii napıon sanane xdxd cscscscs \n press 'E' to skip";
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < originalText.Length; i++)
        {
            stringBuilder.Append(originalText[i]);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("BlipSound");
            }
            dialogueText.text = stringBuilder.ToString();
            yield return new WaitForSeconds(letterDelay);
        }
        messageDisplayed = true;
    }

    void Update()
    {
        if (pressEButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                if (audioManager != null)
                {
                    audioManager.Play("HarubyyActivation");
                }
                StartDialogue();
                if (Input.GetKeyDown(KeyCode.E) && messageDisplayed)
                {
                    DialogueCanvas.SetActive(false);
                    ladyBug.enabled = true;
                    pressEtoInteractDisplay.SetActive(false);
                }
            }
        }
    }
}