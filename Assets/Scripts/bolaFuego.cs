using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaFuego : MonoBehaviour
{
    public float speed = 2.0f;	
    private Rigidbody2D rb; // Componente Rigidbody2D del objeto
    private Collider2D col; // Componente Collider2D del objeto
    float dampingFactor = 0.1f;
    public AudioSource audioDestruir;
    private bool dir=true;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del objeto
      col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto   
    }

    // Update is called once per frame
    void Update()
    {
      if(dir==true){
        transform.position += new Vector3(speed * Time.deltaTime, 0,0);  
      }else{
        transform.position += new Vector3(-speed * Time.deltaTime, 0,0);  
      }  
    }
    public void Mover(bool direc){
      dir=direc;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        int collidedLayer = collision.gameObject.layer;
        if (LayerMask.LayerToName(collidedLayer)=="Terrain"){
          audioDestruir.Play();
          Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioDestruir.Play();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
      if (collision.CompareTag("Rango")){
        Destroy(gameObject);
      }
    }
}
