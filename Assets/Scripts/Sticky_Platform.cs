using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky_Platform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: Enter");
        if (collision.gameObject.name=="Player"){
            collision.gameObject.transform.SetParent(transform);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Triggered: Exit");
        if (collision.gameObject.name=="Player"){
            collision.gameObject.transform.SetParent(null);

        }
    }

}
