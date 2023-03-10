using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldEstrella : MonoBehaviour
{
    public GameObject estrellaPrefab; // GameObject de la moneda que aparecerá cuando el jugador golpee el bloque.
    private Collider2D col; // Componente Collider2D del objeto
    private Animator anim;
    private bool flag=false;
    private bool flagSpawn=false;
    public AudioSource audioAparecer;
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
            if (flagSpawn==false){
                audioAparecer.Play();
                    Vector3 posEstrella=new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                    GameObject estrellaObject = Instantiate(estrellaPrefab, transform.position, Quaternion.identity);
                    StartCoroutine(Aparecer(estrellaObject,posEstrella));
                }
                flagSpawn=true;
            }
            
            StartCoroutine(Salto());
         }
        } 
      }
      IEnumerator Aparecer(GameObject objeto, Vector3 maxAltura){
        while (objeto.transform.position.y<=maxAltura.y){
            objeto.transform.position+=new Vector3(0,0.01f,0); 
            yield return new WaitForSeconds(0.05f); 
        }
        objeto.GetComponent<Collider2D>().enabled=true;
        objeto.GetComponent<Rigidbody2D>().isKinematic=false;
        objeto.GetComponent<Rigidbody2D>().AddForce(Vector2.up*4,ForceMode2D.Impulse);
        objeto.GetComponent<Estrella>().enabled=true;
      }
      IEnumerator Salto(){
        if (flag==false){
            flag=true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            anim.SetBool("isDead",true);
            yield return new WaitForSeconds(0.25f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
        }
      }
  }
