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
    public bool movy=false;
    // Start is called before the first frame update
    void Start()
    {
      col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto   
      rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(dir==true){
        if (movy==true){
          transform.position += new Vector3(speed * Time.deltaTime, speed*Time.deltaTime,0);  
        }else{
          transform.position += new Vector3(speed * Time.deltaTime, -speed*Time.deltaTime,0);  
        }
      }else{
        if (movy==true){
          transform.position += new Vector3(-speed * Time.deltaTime, speed*Time.deltaTime,0);  
        }else{
          transform.position += new Vector3(-speed * Time.deltaTime, -speed*Time.deltaTime,0);  
        } 
      }  
    }
    public void Mover(bool direc){
      dir=direc;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
          Debug.Log("Eche pa arriba");
          movy=true;
          StartCoroutine(rebote());
        }
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
    IEnumerator rebote(){
      yield return new WaitForSeconds(0.1f);
      movy=false;
    }
}
