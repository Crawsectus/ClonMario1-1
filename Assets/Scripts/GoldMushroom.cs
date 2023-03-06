using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMushroom : MonoBehaviour
{
    public GameObject hongoPrefab; // GameObject de la moneda que aparecerá cuando el jugador golpee el bloque.
    public GameObject florPrefab; // GameObject de la moneda que aparecerá cuando el jugador golpee el bloque.
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
                int tam=collision.gameObject.GetComponent<Player>().getTam();
                audioAparecer.Play();
                if (tam<=0){
                    Vector3 posHongo=new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                    GameObject hongoObject = Instantiate(hongoPrefab, posHongo, Quaternion.identity);
                }else{
                    Vector3 posFlor=new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                    GameObject florObject = Instantiate(florPrefab, posFlor, Quaternion.identity);
                }
                flagSpawn=true;
            }
            
            StartCoroutine(Salto());
         }
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
