using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSound;

    // Start is called before the first frame update
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Trap")){
            Die();
            deathSound.Play();
            Debug.Log("Touched Trap");
        }
    }

    private void Die()
    {
        rb.bodyType= RigidbodyType2D.Static;
        anim.SetTrigger("dead");
        anim.SetBool("isDead", true);
    }

    private void RestartLevel(){
        Item_Collector.canDoubleJump=false;
        Item_Collector.canShootFire=false;
        Item_Collector.canTeleport=false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        rb.bodyType=RigidbodyType2D.Dynamic;
    }
}
