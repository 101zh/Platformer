using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Level : MonoBehaviour
{

    private AudioSource levelEndSound;

    private bool levelCompleted = false;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        levelEndSound = GetComponent<AudioSource>();
        anim=GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name=="Player" && !levelCompleted)
        {
            levelEndSound.Play();
            levelCompleted=true;
            anim.Play("Flag_unfurl");
            Invoke("CompleteLevel", 2f);

        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    private bool isPlaying(Animator anim, string stateName)
    {
    if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        return true;
    else
        return false;
    }
}
