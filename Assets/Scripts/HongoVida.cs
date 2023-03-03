using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HongoVida : MonoBehaviour
{
    public float speed = 0.4f;	
    private Rigidbody2D rb; // Componente Rigidbody2D del objeto
    private Collider2D col; // Componente Collider2D del objeto
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del objeto
      col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += new Vector3(speed * Time.deltaTime, 0,0); 
    }
        void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<Player>().AumentarVidas();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Comprobar si la normal de la colisión está en la dirección horizontal
            if (Mathf.Abs(collision.contacts[0].normal.y) < 0.5f)
            {
                // Mover el objeto al otro lado de la pared en el eje Y (arriba y abajo)
                speed= -speed;
            }
        }
    }
}
