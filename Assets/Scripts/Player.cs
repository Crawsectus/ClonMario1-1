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
    public AudioSource audioVictoria;
    private Camera mainCamera;
    private SpriteRenderer sr;
    public float moveSpeed = 0.8f;
    public float runSpeed = 1.2f; 
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
    public bool grounded = false;
    public bool saltar = false;
    private bool flagVictoria=false;
    public GameObject puntaje;
    [Header("Salto regulable")]
    [SerializeField] private float jumpForce;
    [Range(0, 1)] [SerializeField] private float MultiplicadorCancelarSalto;
    private bool botonSaltoArriba = true;



	


    // Start is called before the first frame update
    void Start()
    {   audioMain.Play();
        rb=GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();
        col = GetComponent<Collider2D>(); 
        // vidas
        if (!PlayerPrefs.HasKey("vidas") || PlayerPrefs.GetInt("vidas") <= 0 || PlayerPrefs.GetInt("vidas") >= 5) 
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
        if (!PlayerPrefs.HasKey("monedas") || PlayerPrefs.GetInt("monedas") >= 40)
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
            float moveInput = Input.GetAxis("Horizontal");
            float currentSpeed = Input.GetKey(KeyCode.X) ? runSpeed : moveSpeed; // verificar si se está corriendo
            rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);
            if (!Input.anyKey){
                anim.SetBool("isWalking",false);
                anim.SetBool("isCrouching",false);
                moverRight=false;
                moverLeft=false;
                //anim.SetBool("isJumping",false);
            }
            // Cambiar la dirección del sprite del jugador según la dirección del movimiento
            if (moveInput > 0 && muelto==false)
            {
                anim.SetBool("isWalking",true);
                anim.SetBool("isCrouching",false);
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
                anim.SetBool("isCrouching",false);
                sr.flipX = true;
                moverLeft=true;
                if (moverRight==true){
                Debug.Log("Debería reproducirse el pasito");
                StartCoroutine(Pasito());
                }
            }
            // salto:
            if (Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.Z))
            {
                saltar = true;
            }
            if (Input.GetKeyUp(KeyCode.Space)||Input.GetKeyUp(KeyCode.Z))
            {
                BotonSaltoArriba();
            }
            if (saltar && grounded && botonSaltoArriba)
            {
                Saltar();
            }
            saltar = false;
            if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S) && vida>0 && moverRight==false && moverLeft==false){
                anim.SetBool("isCrouching",true);
                col.offset=new Vector2(0, 0.2f);
                //col.size=new Vector2(1.4f, 1f);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)||Input.GetKeyUp(KeyCode.S)){
                anim.SetBool("isCrouching",false);
                col.offset=new Vector2(0, 0f);
                //col.size=new Vector2(1.4f, 1.92f);
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
                if(Input.GetKeyDown(KeyCode.P)||Input.GetKeyDown(KeyCode.C)){
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

    private void Saltar()
    {
        audioSalto.Play();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        grounded = false;
        botonSaltoArriba = false;
        saltar = false;
        
    }
    private void BotonSaltoArriba(){
        if(rb.velocity.y>0){
            rb.AddForce(Vector2.down * rb.velocity.y * (1-MultiplicadorCancelarSalto), ForceMode2D.Impulse);
        }
        botonSaltoArriba = true;
        saltar = false;
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
                StartCoroutine(animSubir());
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
    IEnumerator CrecerAnim(){
        grounded=true;
        transform.localScale = new Vector3(0.085f, 0.10f, 0f);
        yield return new WaitForSeconds(0.1f);
        grounded=true;
        transform.localScale = new Vector3(0.085f, 0.08f, 0f);
        yield return new WaitForSeconds(0.1f);
        grounded=true;
        transform.localScale = new Vector3(0.085f, 0.10f, 0f);
        yield return new WaitForSeconds(0.1f);
        grounded=true;
        transform.localScale = new Vector3(0.085f, 0.13f, 0f);
        muelto=false;
    }
    public void Crecer()
    {
        audioCrecer.Play();
    	//gameObject.transform.localScale *= 1.5f;
        muelto=true;
        StartCoroutine(CrecerAnim());
        vida=1;
    }
    public void CrecerFuego(){
        fuego=true;
        if(Diva==false){
            muelto=true;
            StartCoroutine(Invencibilidad(0.2f,false));
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
                StartCoroutine(animBajar());
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
        muelto=false;
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
    public void AumentarMonedas(){
        monedas++;
        PlayerPrefs.SetInt("monedas", monedas);
        AumentarPuntos(200);
        Debug.Log(monedas);
    }
    public void AumentarPuntos(int puntos){
        this.puntos += puntos;
        PlayerPrefs.SetInt("puntos", this.puntos);
        Debug.Log(this.puntos);
    }
    IEnumerator animGanar(){
        audioMain.Stop();
        if (flagVictoria==false){
            audioVictoria.Play();
            flagVictoria=true;
        }
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(1.2f); 
        transform.position+=new Vector3(0.1f,0,0);
        transform.rotation=Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(0.8f); 
        rb.constraints = RigidbodyConstraints2D.None;
        transform.rotation=Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.8f); 
        anim.SetBool("isWalking",true);
        while (transform.position.x<=15.84f){
            transform.position += new Vector3(0.03f, 0,0); 
            yield return new WaitForSeconds(0.1f); 
        }
        Invoke("GameOver", 1f);

    }
    IEnumerator verPuntos(string puntosObt){
        Vector3 posPuntos=new Vector3(transform.position.x+0.05f,transform.position.y+0.15f,transform.position.z);
        GameObject puntajeObject = Instantiate(puntaje, posPuntos, Quaternion.identity);
        puntajeObject.GetComponent<TextMesh>().text=puntosObt;
        yield return new WaitForSeconds(0.5f);
        Destroy(puntajeObject);
    }
    public void Ganar(){
        // obtener la posicion en y del jugador en este momento
        // guardarla en una variable
        float altura = transform.position.y;
        Debug.Log(altura);
        if (altura >= 1.65f){
            StartCoroutine(verPuntos("5000"));
            AumentarPuntos(5000);
        }
        else if (altura<1.65f && altura >= 1.33f ){
            StartCoroutine(verPuntos("1000"));
            AumentarPuntos(1000);
        }
        else if (altura<1.33f && altura > 0.57f ){
            StartCoroutine(verPuntos("100"));
            AumentarPuntos(100);
        }
        muelto=true;
        StartCoroutine(animGanar());
    }
    IEnumerator animBajar(){
        rb.isKinematic=true;
        col.enabled=false;
        transform.position+=new Vector3(0,-0.04f,0);
        yield return new WaitForSeconds(0.2f); 
        transform.position+=new Vector3(0,-0.04f,0);
        yield return new WaitForSeconds(0.2f); 
        transform.position+=new Vector3(0,-0.04f,0);
        yield return new WaitForSeconds(0.2f); 
        rb.isKinematic=false;
        col.enabled=true;
        transform.position = new Vector3(-8.82f, -0.4f, transform.position.z);
        mainCamera.transform.position = new Vector3(transform.position.x, -1.2f, mainCamera.transform.position.z);
    }
    IEnumerator animSubir(){
        rb.isKinematic=true;
        col.enabled=false;
        transform.position+=new Vector3(0.04f,0,0);
        yield return new WaitForSeconds(0.2f); 
        transform.position+=new Vector3(0.04f,0,0);
        yield return new WaitForSeconds(0.2f); 
        transform.position+=new Vector3(0.04f,0,0);
        yield return new WaitForSeconds(0.2f); 
        rb.isKinematic=false;
        col.enabled=true;
        transform.position = new Vector3(9.37f, 0.733f, transform.position.z);
        mainCamera.transform.position = new Vector3(transform.position.x, 1.24f, mainCamera.transform.position.z);
    }
}