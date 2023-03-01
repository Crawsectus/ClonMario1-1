using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D  rb;
    public bool grounded = false;
    private Camera mainCamera;
    private SpriteRenderer sr;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int vida=0;
    private Animator anim;
    private float initialX;
    public bool salto=false;
    private Collider2D col;
    public bool moverLeft,moverRight,muelto=false;
    public int puntos;
    public int monedas;
    public int vidas;
   //holaaaaaaa xcompileeee
	


    // Start is called before the first frame update
    void Start()
    {   
        rb=GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        if (!PlayerPrefs.HasKey("vidas") || PlayerPrefs.GetInt("vidas") <= 0)
        {
            vidas = 3;
            PlayerPrefs.SetInt("vidas", vidas);
        }
        else
        {
            vidas = PlayerPrefs.GetInt("vidas");
        }
        	
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (!Input.anyKey){
            anim.SetBool("isWalking",false);
            moverRight=false;
            moverLeft=false;
            //anim.SetBool("isJumping",false);
        }
        // Cambiar la dirección del sprite del jugador según la dirección del movimiento
        if (moveInput > 0 && muelto==false)
        {
            anim.SetBool("isWalking",true);
            sr.flipX = false;
            moverRight=true;
            if (moverLeft==true){
              Debug.Log("Debería reproducirse el pasito");
              StartCoroutine(Pasito());
            }              
        }
        else if (moveInput < 0 && muelto==false)
        {
            anim.SetBool("isWalking",true);
            sr.flipX = true;
            moverLeft=true;
            if (moverRight==true){
              Debug.Log("Debería reproducirse el pasito");
              StartCoroutine(Pasito());
            }
        }
        //salto
        if(Input.GetKeyDown(KeyCode.Space) && grounded == true && muelto==false)
        {
            rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            salto=true;
        }
        if(salto==true){
            anim.SetBool("isWalking",false);
            anim.SetBool("isJumping",true);
        }
        // Hacer que la cámara siga al jugador en el eje X
        //mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);

        // Verificar si el jugador ha avanzado hacia la derecha
        initialX = mainCamera.transform.position.x;
        if (transform.position.x > initialX) {
            // Actualizar la posición de la cámara sólo si el jugador ha avanzado hacia la derecha
            mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
            //limitLeft.transform.position = new Vector3(transform.position.x, limitLeft.transform.position.y, limitLeft.transform.position.z);
        }
        else {
            // Mantener la posición x de la cámara en su valor inicial si el jugador no ha avanzado hacia la derecha
            mainCamera.transform.position = new Vector3(initialX, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        if (transform.position.y < -2)
        {
            Morir();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            salto=false;
            grounded = true;
            anim.SetBool("isJumping",false);
        }
    }
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
            anim.SetBool("isJumping",false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            anim.SetBool("isJumping",true);
        }
    }
    private void Die()
    {
        // Cargar la siguiente escena
        SceneManager.LoadScene("die");
    }
    private void GameOver()
    {
        // Cargar la siguiente escena
        SceneManager.LoadScene("GameOver");
    }
    public void Morir()
    { 
    	if (vida<=0){
            anim.SetBool("isDead",true);
            
            rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            col.isTrigger = true;
            muelto=true;
            vidas--;
            PlayerPrefs.SetInt("vidas", vidas);
            if (vidas <= 0){
                Invoke("GameOver", 1f);
            }
            else{
                Invoke("Die", 1f);
            }
        }else{
            vida--;
            gameObject.transform.localScale *= 0.75f;
        }
    }
    IEnumerator Pasito(){
        anim.SetBool("Pasito",true);
        anim.SetBool("isWalking",false);
        Debug.Log("ANIMATEEEEEEE");
    	yield return new WaitForSeconds(0.25f);
    	anim.SetBool("Pasito",false);
    	anim.SetBool("isWalking",true);
    }
    public void Crecer()
    {
    	if (vida<=0){
    	gameObject.transform.localScale *= 1.5f;
    	}else if (vida>=1){
    	Debug.Log("FUEGO!");
    	}
    	vida++;
    }
    public int getTam(){
       return vida;
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("Bajada")){
            if(Input.GetKeyDown(KeyCode.S)){
                transform.position = new Vector3(-8.82f, -0.4f, transform.position.z);
                mainCamera.transform.position = new Vector3(transform.position.x, -1.2f, mainCamera.transform.position.z);
            }
        }
    }
}
