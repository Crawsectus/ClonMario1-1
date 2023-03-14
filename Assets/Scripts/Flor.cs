using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flor : MonoBehaviour
{
    private Collider2D col; // Componente Collider2D del objeto
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("LSD") || collision.gameObject.CompareTag("Invencible")){
            collision.gameObject.GetComponent<Player>().CrecerFuego();
            collision.gameObject.GetComponent<Player>().AumentarPuntos(1000);
            Destroy(gameObject);
        }
    }
}
