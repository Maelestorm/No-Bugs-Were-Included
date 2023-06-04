using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybugGlideSound : MonoBehaviour
{
    [SerializeField] private GameObject glidingSound;

    // Start is called before the first frame update
    private void Start()
    {
        glidingSound.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !LadyBugMovementScript.isGrounded)
        {
            GlidingSound();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopGlidingSound();
        }
    }

    private void GlidingSound()
    {
        glidingSound.SetActive(true);
    }

    private void StopGlidingSound()
    {
        glidingSound.SetActive(false);
    }
}