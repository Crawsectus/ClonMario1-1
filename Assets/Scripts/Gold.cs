using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private Collider2D col; // Componente Collider2D del objeto
    private Animator anim;
    private bool flag=false;
    public AudioSource audioGold;
    public float velocidadMoneda = 2f; // La velocidad a la que la moneda se moverá hacia arriba.
    public GameObject monedaPrefab; // GameObject de la moneda que aparecerá cuando el jugador golpee el bloque.
    //public float alturaMoneda = 1f; // La altura a la que la moneda se moverá hacia arriba.
    public int contador=0;
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
        if (collision.gameObject.CompareTag("Player")){
        ContactPoint2D contact = collision.contacts[0];
        float dotProduct = Vector2.Dot(contact.normal, Vector2.up);
        if (dotProduct > 0.5f)
         {
            StartCoroutine(Salto());
         }
        } 
      }
      IEnumerator Salto(){
        if (flag==false){
            if (contador<=0){
                flag=true;
                anim.SetBool("isDead",true);
            }
            audioGold.Play();
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            GameObject monedaObject = Instantiate(monedaPrefab, transform.position, Quaternion.identity);
            monedaObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidadMoneda);
            yield return new WaitForSeconds(0.25f);
            Destroy(monedaObject);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            contador--;
        }
      }
  }
