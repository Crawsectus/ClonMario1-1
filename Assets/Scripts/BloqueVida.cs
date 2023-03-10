using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueVida : MonoBehaviour
{   private Collider2D col; // Componente Collider2D del objeto
    private Animator anim;
    private bool flag=true;
    private bool uso=true;

    [Header("Hongo Vida")]
    public GameObject hongoPrefab; // GameObject de la moneda que aparecerá cuando el jugador golpee el bloque.

    [Header("Audio")]
    public AudioSource audioAparecer;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
        anim= GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")|| other.CompareTag("LSD") || other.CompareTag("Invencible")) && uso==true)
        {   
            uso = false;
            // Si el jugador colisiona con el bloque desde abajo, genera un hongo y destruye el bloque
            Vector2 contactPoint = other.ClosestPoint(transform.position);
            Vector2 normal = transform.position - new Vector3(contactPoint.x, contactPoint.y, transform.position.z);
            if (normal.y > 0)
            {
                Vector3 posHongo=new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                GameObject hongoObject = Instantiate(hongoPrefab, transform.position, Quaternion.identity);
                StartCoroutine(Aparecer(hongoObject,posHongo,true));
                
            
            StartCoroutine(Salto());
            }
        }
    }
    IEnumerator Aparecer(GameObject objeto, Vector3 maxAltura,bool tipo){
        while (objeto.transform.position.y<=maxAltura.y){
            objeto.transform.position+=new Vector3(0,0.01f,0); 
            yield return new WaitForSeconds(0.05f); 
        }
        objeto.GetComponent<Collider2D>().enabled=true;
        if (tipo==true){
            objeto.GetComponent<Rigidbody2D>().isKinematic=false;
            objeto.GetComponent<Hongo>().enabled=true;
        }else{
            objeto.GetComponent<Flor>().enabled=true;
        }
        
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
