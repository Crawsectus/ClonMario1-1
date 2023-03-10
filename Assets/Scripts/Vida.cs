using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("LSD") || other.gameObject.CompareTag("Invencible"))
        {
            Vector2 contactPoint = other.ClosestPoint(transform.position);
            Vector2 normal = transform.position - new Vector3(contactPoint.x, contactPoint.y, transform.position.z);
            if (normal.y > 0)
            {
                if (flagSpawn == false)
                {
                    audioAparecer.Play();
                    Vector3 posEstrella = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                    GameObject estrellaObject = Instantiate(estrellaPrefab, transform.position, Quaternion.identity);
                    StartCoroutine(Aparecer(estrellaObject, posEstrella));
                }
                flagSpawn = true;
                StartCoroutine(Salto());
            }
            
        }
    }

    private void salio(){
        col.isTrigger = false;
    }
      
      IEnumerator Aparecer(GameObject objeto, Vector3 maxAltura){
        while (objeto.transform.position.y<=maxAltura.y){
            objeto.transform.position+=new Vector3(0,0.01f,0); 
            yield return new WaitForSeconds(0.05f); 
        }
        objeto.GetComponent<Collider2D>().enabled=true;
        objeto.GetComponent<Rigidbody2D>().isKinematic=false;
        objeto.GetComponent<Hongo>().enabled=true;
      }
      IEnumerator Salto(){
        if (flag==false){
            flag=true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            anim.SetBool("isDead",true);
            yield return new WaitForSeconds(0.25f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            Invoke("salio",0.5f);
        }
      }
  }
