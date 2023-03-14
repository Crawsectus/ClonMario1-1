using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hongo : MonoBehaviour
{
    public float speed = 1.0f;	
    private Rigidbody2D rb; // Componente Rigidbody2D del objeto
    private Collider2D col; // Componente Collider2D del objeto
    public bool tipoHongo=true;
    private bool flag=false;

    public bool isVida;
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("LSD") || collision.gameObject.CompareTag("Invencible") && flag==false){
            flag=true;
            if (tipoHongo==true){
                collision.gameObject.GetComponent<Player>().Crecer();
            }else{
                collision.gameObject.GetComponent<Player>().AumentarVidas();
            }
            if (!isVida){
                collision.gameObject.GetComponent<Player>().AumentarPuntos(1000);
            }
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
