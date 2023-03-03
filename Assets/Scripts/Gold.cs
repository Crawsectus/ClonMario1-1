using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private Collider2D col; // Componente Collider2D del objeto
    private Animator anim;
    private bool flag=false;
    public AudioSource audioGold;
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
            audioGold.Play();
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
