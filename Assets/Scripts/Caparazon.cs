using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caparazon : MonoBehaviour
{
    public float speed = 1.0f;	
    public bool canMove = false;
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
        if (canMove)
        {
          transform.position += new Vector3(-speed * Time.deltaTime, 0,0); 
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto colisiona con otro objeto que tenga un Collider2D
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Comprobar si la normal de la colisión está en la dirección horizontal
            if (Mathf.Abs(collision.contacts[0].normal.y) < 0.5f)
            {
                // Mover el objeto al otro lado de la pared en el eje Y (arriba y abajo)
                speed= -speed;
            }
        }

        if (collision.gameObject.CompareTag("Player")){
            ContactPoint2D contact = collision.contacts[0];
            float dotProduct = Vector2.Dot(contact.normal, Vector2.up);
            if (dotProduct < -0.5f)
            {
                // El jugador ha tocado la parte inferior del BoxCollider2D
                Debug.Log("abajo");
                canMove=true;
                /*if (canMove==false){
                    canMove=true;
                }else{
                   canMove=false; 
                }*/
            }else if (contact.normal.x > 0)
            {
                // El jugador ha tocado el lado derecho del BoxCollider2D
                Debug.Log("derecha");
                if (canMove==false){
                    speed=-speed;
                    canMove=true;
                }else{
                   collision.gameObject.GetComponent<Player>().Morir(true);
                }
            }else if (contact.normal.x < 0)
            {
                // El jugador ha tocado el lado derecho del BoxCollider2D
                Debug.Log("izquierda");
                if (canMove==false){
                    canMove=true;
                }else{
                   collision.gameObject.GetComponent<Player>().Morir(true);
                }
            }
        }
        if (collision.gameObject.CompareTag("Enemy")){
            Destroy(collision.gameObject);
        }
    }
}
