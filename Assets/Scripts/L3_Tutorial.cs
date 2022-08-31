using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class L3_Tutorial : MonoBehaviour
{
    [SerializeField] private Text tutorialText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Double Jump Potion")){

            tutorialText.text="Press the JUMP button while in the air to perform a double jump";
        }
    }
}
