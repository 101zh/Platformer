using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item_Collector : MonoBehaviour
{
    private int cherries=0;
    [SerializeField] private AudioSource pickupSound;

    [SerializeField] private Text cherriesText;

    public static bool canDoubleJump=false;
    public static bool canTeleport=false;
    public static bool canShootFire=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Cherry")){
            pickupSound.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: "+cherries;
        }

        if (collision.gameObject.CompareTag("Double Jump Potion")){
            pickupSound.Play();
            Destroy(collision.gameObject);
            canDoubleJump=true;
            canTeleport=false;
            canShootFire=false;
        }

        if (collision.gameObject.CompareTag("Teleportation Potion")){
            pickupSound.Play();
            Destroy(collision.gameObject);
            canDoubleJump=false;
            canTeleport=true;
            canShootFire=false;
        }

        if (collision.gameObject.CompareTag("Fireball Potion")){
            pickupSound.Play();
            Destroy(collision.gameObject);
            canDoubleJump=false;
            canTeleport=false;
            canShootFire=true;
        }

    }
}
