using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D  rb;
    public AudioSource audioMain;
    public AudioSource audioMuerte;
    public AudioSource audioSalto;
    public AudioSource audioTubo;
    public AudioSource audioMoneda;
    public AudioSource audioCrecer;
    public AudioSource audioVida;
    public AudioSource audioFuego; 
    public AudioSource audioEstrella;
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
    private bool fuego=false;
    private bool Diva=false;
    public GameObject bolaFuego;
    public int contador=0;
    public bool CD=false;
   //holaaaaaaa xcompileeee
	


    // Start is called before the first frame update
    void Start()
    {   audioMain.Play();
        rb=GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();
        col = GetComponent<Collider2D>(); 
        // vidas
        if (!PlayerPrefs.HasKey("vidas") || PlayerPrefs.GetInt("vidas") <= 0) 
        {
            vidas = 3;
            PlayerPrefs.SetInt("vidas", vidas);
        }
        else
        {
            vidas = PlayerPrefs.GetInt("vidas");
        }
        // puntos
        if (!PlayerPrefs.HasKey("puntos") || PlayerPrefs.GetInt("puntos") < 0)
        {
            puntos = 0;
            PlayerPrefs.SetInt("puntos", puntos);
        }
        else
        {
            puntos = PlayerPrefs.GetInt("puntos");
        }
        // Monedas
        if (!PlayerPrefs.HasKey("monedas") || PlayerPrefs.GetInt("monedas") >= 19)
        {
            monedas = 0;
            PlayerPrefs.SetInt("monedas", monedas);
        }
        else
        {
            monedas = PlayerPrefs.GetInt("monedas");
        }
        	
    }

    // Update is called once per frame
    void Update()
    {
        if (muelto == false)
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
                audioSalto.Play();
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
            if (fuego==true){
                if(Input.GetKeyDown(KeyCode.P)){
                    if (contador<=1 && CD==false){
                    audioFuego.Play();    
                    if (sr.flipX==false){
                        Vector3 posFuego=new Vector3(transform.position.x+ 0.2f, transform.position.y, transform.position.z);
                        GameObject fuegoObject = Instantiate(bolaFuego, posFuego, Quaternion.identity);
                        fuegoObject.GetComponent<bolaFuego>().Mover(true);
                    }else{
                        Vector3 posFuego=new Vector3(transform.position.x- 0.2f, transform.position.y, transform.position.z);
                        GameObject fuegoObject = Instantiate(bolaFuego, posFuego, Quaternion.identity);
                        fuegoObject.GetComponent<bolaFuego>().Mover(false);
                    }
                    contador++;
                    }else if(contador>1){
                        CD=true;
                        contador=0;
                        StartCoroutine(cooldown());
                    }
                }
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Moneda"))
        {
            audioMoneda.Play();
            Destroy(collision.gameObject);
            monedas++;
            PlayerPrefs.SetInt("monedas", monedas);
        }
        if (collision.CompareTag("Vacio"))
        {
            Morir(false);
        }
        if (collision.gameObject.CompareTag("Subida")){
                audioTubo.Play();
                transform.position = new Vector3(9.37f, 0.733f, transform.position.z);
                mainCamera.transform.position = new Vector3(transform.position.x, 1.24f, mainCamera.transform.position.z);
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
        monedas=0;
        PlayerPrefs.SetInt("monedas", monedas);
        // Cargar la siguiente escena
        SceneManager.LoadScene("GameOver");
    }
    public void Morir(bool animar)
    { 
        if (animar==false){
            audioMain.Stop();
            gameObject.layer = LayerMask.NameToLayer("Muerto");
            audioMuerte.Play();
            vidas--;
            PlayerPrefs.SetInt("vidas", vidas);
            if (vidas <= 0){
                Invoke("GameOver", 2.5f);
            }
            else{
                Invoke("Die", 2.5f);
            } 
        }else{
    	if (vida<=0){
            audioMain.Stop();
            gameObject.layer = LayerMask.NameToLayer("Muerto");
            audioMuerte.Play();
            anim.SetBool("isDead",true);
            GetComponent<Rigidbody2D>().constraints = (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
            rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            muelto=true;
            vidas--;
            PlayerPrefs.SetInt("vidas", vidas);
            if (vidas <= 0){
                Invoke("GameOver", 2.5f);
            }
            else{
                Invoke("Die", 2.5f);
            }
        }else if (vida==1){
            gameObject.transform.localScale *= 0.75f;
            fuego=false;
            vida--;
            StartCoroutine(Invencibilidad(3f,false));
        }else{
            fuego=false;
            changeAnimator("Base Layer");
            vida--;
            StartCoroutine(Invencibilidad(3f,false));
        }
        }
    }
    IEnumerator Pasito(){
        //anim.SetBool("Pasito",true);
        //anim.SetBool("isWalking",false);
        anim.SetBool("Pasito",true);
        Debug.Log("ANIMATEEEEEEE");
    	yield return new WaitForSeconds(0.25f);
    	anim.SetBool("Pasito",false);
    	anim.SetBool("isWalking",true);
    }
    IEnumerator cooldown(){
      yield return new WaitForSeconds(0.2f);  
      CD=false;
    }
    public void Crecer()
    {
        audioCrecer.Play();
    	//gameObject.transform.localScale *= 1.5f;
        transform.localScale = new Vector3(0.085f, 0.13f, 0f);
        vida=1;
    }
    public void CrecerFuego(){
        fuego=true;
        if(Diva==false){
            changeAnimator("Fuego");
        }
        vida=2;
    }
    public int getTam(){
       return vida;
    }
    public void AumentarVidas(){
        audioVida.Play();
        vidas++;
        PlayerPrefs.SetInt("vidas", vidas);
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("Bajada")){
            if(Input.GetKeyDown(KeyCode.S)){
                audioTubo.Play();
                transform.position = new Vector3(-8.82f, -0.4f, transform.position.z);
                mainCamera.transform.position = new Vector3(transform.position.x, -1.2f, mainCamera.transform.position.z);
            }
        }
    }
    public void Estrella(){
        audioMain.Pause();
        audioEstrella.Play();
        StartCoroutine(Invencibilidad(10f,true));
    }
    IEnumerator Invencibilidad(float tiempo,bool modo){
        changeAnimator("Estrella");
        Diva=true;
        if (modo==true){
            gameObject.tag="LSD";
            moveSpeed=moveSpeed+1.5f;
        }else{
            gameObject.tag="Invencible";
        }
        yield return new WaitForSeconds(tiempo); 
        gameObject.tag="Player";
        if (fuego==true){
            changeAnimator("Fuego");
        }else{
            changeAnimator("Base Layer");
        }
        Diva=false;
        if (modo==true){
            audioEstrella.Stop();
            audioMain.UnPause();
            moveSpeed=moveSpeed-1.5f;
            
        }
    }
    public void changeAnimator(string nombreLayer){
        for (int i =0;i<anim.layerCount;i++){
            anim.SetLayerWeight(i,0);
        }
        int index=anim.GetLayerIndex(nombreLayer);
        anim.SetLayerWeight(index,1);
    }
}