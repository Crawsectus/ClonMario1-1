using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flor : MonoBehaviour
{
    private Collider2D col; // Componente Collider2D del objeto
    public GameObject puntaje;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("LSD") || collision.gameObject.CompareTag("Invencible")){
            collision.gameObject.GetComponent<Player>().CrecerFuego();
            StartCoroutine(mostrarPuntos());
            collision.gameObject.GetComponent<Player>().AumentarPuntos(1000);
            Destroy(gameObject);
        }
    }
    IEnumerator mostrarPuntos(){
        Vector3 posPuntos=new Vector3(transform.position.x+0.05f,transform.position.y+0.15f,transform.position.z);
        GameObject puntajeObject = Instantiate(puntaje, posPuntos, Quaternion.identity);
        puntajeObject.GetComponent<TextMesh>().text="1000";
        yield return new WaitForSeconds(0.25f);
        Destroy(puntajeObject);
    }
}
