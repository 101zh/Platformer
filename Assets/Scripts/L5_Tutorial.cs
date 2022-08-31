using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class L5_Tutorial : MonoBehaviour
{
    [SerializeField] private Text tutorialText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Teleportation Potion")){

            tutorialText.text="Press SPACE to teleport";
        }
    }
}
