using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandera : MonoBehaviour
{
    public GameObject bandera;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto  
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Invencible") || collision.gameObject.CompareTag("LSD")){
            bandera.GetComponent<banderin>().bajar();
            Debug.Log("GANASTE!");
            collision.gameObject.GetComponent<Player>().Ganar();
        }
    }
}
