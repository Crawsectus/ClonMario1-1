using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLeft : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            // Hacer la pared sólida
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else{
            // Hacer la pared sólida
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }
}
