using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLeft : MonoBehaviour
{
	private Collider2D col; // Componente Collider2D del objeto
	void Start()
    	{
      		col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto        
        }
        void OnCollisionEnter2D(Collision2D collision)
    	{
    		if (collision.gameObject.CompareTag("Enemy")){
    			collision.gameObject.GetComponent<Goomba>().Morir();
    		}
    	}
}
