using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public float speed = 1.0f;	
    private bool canMove = false;
    private Renderer enemyRenderer;
    private Rigidbody2D rb; // Componente Rigidbody2D del objeto
    private Collider2D col; // Componente Collider2D del objeto
    public bool muelto=false;
    private Animator anim;
    public GameObject caparazon;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del objeto
      col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
      sr = GetComponent<SpriteRenderer>();
      enemyRenderer = GetComponent<Renderer>();
      anim= GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    { 
      if (!canMove && enemyRenderer.isVisible)
        {
            canMove = true;
        }

        if (canMove && muelto==false)
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
                if  (sr.flipX == false){
                sr.flipX = true;
                }else{
                    sr.flipX = false;
                }
            }
        }

        if (collision.gameObject.CompareTag("Player")){
            ContactPoint2D contact = collision.contacts[0];
            float dotProduct = Vector2.Dot(contact.normal, Vector2.up);
            if (dotProduct > 0.5f)
            {
                // El jugador ha tocado la parte superior del BoxCollider2D
                Debug.Log("arriba");
                collision.gameObject.GetComponent<Player>().Morir();
            }
            else if (dotProduct < -0.5f)
            {
                // El jugador ha tocado la parte inferior del BoxCollider2D
                Debug.Log("abajo");
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                GameObject caparazonObject = Instantiate(caparazon, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (contact.normal.x > 0)
            {
                // El jugador ha tocado el lado derecho del BoxCollider2D
                Debug.Log("derecha");
                collision.gameObject.GetComponent<Player>().Morir();
            }
            else if (contact.normal.x < 0)
            {
                // El jugador ha tocado el lado izquierdo del BoxCollider2D
                Debug.Log("izquierda");
                collision.gameObject.GetComponent<Player>().Morir();
            }
        }
    }
    public void Morir(){
    	StartCoroutine(MorirAhoraSi());
    	anim.SetBool("Muelto",true);
    	muelto=true;
    }
    public void Destruir(){
	Destroy(gameObject);
    }
    IEnumerator MorirAhoraSi()
    {
           Debug.Log("Tengo frio");
           yield return new WaitForSeconds(0.3f);
           Destroy(gameObject);
           Debug.Log("Aeugh");
        
    }
}