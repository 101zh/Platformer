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
        Item_Collector.canDoubleJump=false;
        Item_Collector.canShootFire=false;
        Item_Collector.canTeleport=false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
