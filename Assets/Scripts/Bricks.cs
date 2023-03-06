using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    private Collider2D col; // Componente Collider2D del objeto
    private Animator anim;
    public AudioSource audioDestruir;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("LSD") || collision.gameObject.CompareTag("Invencible")){
        ContactPoint2D contact = collision.contacts[0];
        float dotProduct = Vector2.Dot(contact.normal, Vector2.up);
        if (dotProduct > 0.5f)
         {
           int tam= collision.gameObject.GetComponent<Player>().getTam();
           if (tam<=0){
              StartCoroutine(Salto());
           }else{
              audioDestruir.Play();
              StartCoroutine(Destruir());
           }
         }
        } 
      }
      IEnumerator Salto(){
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        yield return new WaitForSeconds(0.25f);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
      }
      IEnumerator Destruir(){
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        anim.SetBool("isDead",true);
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
      }
  }
