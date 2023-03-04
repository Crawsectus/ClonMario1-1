using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float speed = 1.0f;	
    private bool canMove = false;
    public AudioSource audioMorir;
    private Renderer enemyRenderer;
    private Rigidbody2D rb; // Componente Rigidbody2D del objeto
    private SpriteRenderer sr;
    private Collider2D col; // Componente Collider2D del objeto
    public bool muelto=false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del objeto
      col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
      enemyRenderer = GetComponent<Renderer>();
      anim= GetComponent<Animator>();
      sr = GetComponent<SpriteRenderer>();
        
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
        if (collision.collider.GetComponent<Collider2D>())
        {
            // Comprobar si la normal de la colisión está en la dirección horizontal
            if (Mathf.Abs(collision.contacts[0].normal.y) < 0.5f)
            {
                // Mover el objeto al otro lado de la pared en el eje Y (arriba y abajo)
                speed= -speed;
            }
        }
        if (collision.gameObject.CompareTag("Fuego")){
            MorirFuego();
        }
        if (collision.gameObject.CompareTag("LSD")){
            MorirFuego();
        }
        if (collision.gameObject.CompareTag("Player")){
            ContactPoint2D contact = collision.contacts[0];
            float dotProduct = Vector2.Dot(contact.normal, Vector2.up);
            if (dotProduct > 0.5f)
            {
                // El jugador ha tocado la parte superior del BoxCollider2D
                Debug.Log("arriba");
                collision.gameObject.GetComponent<Player>().Morir(true);
            }
            else if (dotProduct < -0.5f)
            {
                // El jugador ha tocado la parte inferior del BoxCollider2D
                Debug.Log("abajo");
                Morir();
            }
            else if (contact.normal.x > 0 && dotProduct > -0.5f)
            {
                // El jugador ha tocado el lado derecho del BoxCollider2D
                Debug.Log("derecha");
                collision.gameObject.GetComponent<Player>().Morir(true);
            }
            else if (contact.normal.x < 0 && dotProduct > -0.5f)
            {
                // El jugador ha tocado el lado izquierdo del BoxCollider2D
                Debug.Log("izquierda");
                collision.gameObject.GetComponent<Player>().Morir(true);
            }
        }
    }
    public void Morir(){
        if (muelto==false){
            rb.isKinematic = true;
            col.enabled=false;
            audioMorir.Play();
    	    StartCoroutine(MorirAhoraSi());
    	    anim.SetBool("Muelto",true);
    	    muelto=true;
        }
    }
    public void MorirFuego(){
        sr.flipY=true;
        muelto=true;
        gameObject.layer = LayerMask.NameToLayer("Muerto");
        rb.AddForce(Vector2.up*2,ForceMode2D.Impulse);
        rb.AddForce(Vector2.right*1,ForceMode2D.Impulse);
        StartCoroutine(MorirAhoraSi());
    }
    public void Destruir(){
	Destroy(gameObject);
    }
    IEnumerator MorirAhoraSi()
    {
           yield return new WaitForSeconds(0.5f);
           Destroy(gameObject);
        
    }
}
