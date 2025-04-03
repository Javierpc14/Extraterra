using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoProta : MonoBehaviour
{
    public float velocidad = 2;
    public float fuerzaRebote = 10f;

    //Variables de salto
    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;
    private bool enSuelo;

    // Variables para la caida al vacio del personaje
    public float xInicial;
    public float yInicial;

    public int vida = 5;
    public bool muerto;

    public Animator animator;
    private bool recibiendoDano;
    Rigidbody2D rigidbody;

    // Variables proyectil
    public float velocidadProyectil = 1000f;
    public GameObject proyectil;

    public int totalPartesReloj;
    public Text txtTotalRelojes;
    

    //Esta variable es para saber si el proyectil tiene que moverse a la derecha o a la izquierda
    // 1 derecha / 2 izquierda
    public int direccionProyectil = 1;

    //variables de sonido
    // no se por que pero con esto [SerializeField] funciona y sin eso no
    [SerializeField] private GameObject objSaltoProta;
    [SerializeField] private GameObject objDanoProta;
    [SerializeField] private GameObject objDisparoProta;
    [SerializeField] private GameObject objMuerteProta;
    [SerializeField] private GameObject objMusicaFondo;
    private AudioSource musicaFondo;
    private AudioSource sSaltoProta;
    private AudioSource sDanoProta;
    private AudioSource sDisparoProta;
    private AudioSource sMuerteProta;

    void Start()
    {
        // dar valores iniciales a xInicial e yInicial
        xInicial = transform.position.x;
        yInicial = transform.position.y;

        totalPartesReloj = 0;

        sSaltoProta = objSaltoProta.GetComponent<AudioSource>();
        sDanoProta = objDanoProta.GetComponent<AudioSource>();
        sDisparoProta = objDisparoProta.GetComponent<AudioSource>();
        sMuerteProta = objMuerteProta.GetComponent<AudioSource>();
        musicaFondo = objMusicaFondo.GetComponent<AudioSource>();

        musicaFondo.Play();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        movimiento();

        saltoProta();
        

        animaciones();

        direccionPersonaje();  
    }

    // Utilizo la funcion update para la bala debido a que FixedUpdate se ejecuta a un ritmo fijo,
    // lo que hace que al pulsar la tecla de disparo no siempre haga salir una bala
    void Update(){
        // GetKeyUp para que no se espameen las balas
        // utilizo el KeyCode.E porque funciona mejor
        if(Input.GetKeyDown(KeyCode.E)){
            sDisparoProta.Play();
            //aqui se especifica la direccion y la rotacion
            GameObject goProyectil = Instantiate(proyectil, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation);

            if(direccionProyectil == 1){
                goProyectil.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(velocidadProyectil * Time.deltaTime, 0f);
            }else if(direccionProyectil == 2){
                goProyectil.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-velocidadProyectil * Time.deltaTime, 0f);
            }

            Destroy(goProyectil, 3f);
        }
    }

    public void movimiento(){
        // movimiento
        if(!muerto){
            if (Input.GetKey("d") && !recibiendoDano)
            {
                rigidbody.linearVelocity = new Vector2(velocidad, rigidbody.linearVelocity.y);
                // al pulsar la d (derecha) indico que la bala se mueva a la derecha
                direccionProyectil = 1;
            }
            else if (Input.GetKey("a") && !recibiendoDano)
            {
                rigidbody.linearVelocity = new Vector2(-velocidad, rigidbody.linearVelocity.y);
                // al pulsar la a (izquierda) indico que la bala se mueva a la derecha
                direccionProyectil = 2;
            }
            else
            {
                rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);
            }
        } 
    }

    public void saltoProta(){
        // creo un RaycastHit2D el cual es igual a una linea que empieza desde la posicion del jugador, 
        // mira hacia abajo, tiene la longitud de longitudRaycast y que busca colisionar con la capa de suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKey(KeyCode.Space) && !recibiendoDano)
        {
            // le añado una fuerza al rigidbody y qeue la fuerza va a ser a modo de impulso con ForceMode2D
            rigidbody.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            sSaltoProta.Play();
        }
        //Animacion salto
        animator.SetBool("ensuelo", enSuelo);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }

    public void animaciones(){
        

        //Recibir Dano
        animator.SetBool("recibeDano", recibiendoDano);

        // Cambio de animaciones
        animator.SetFloat("movimiento", rigidbody.linearVelocity.x * velocidad);

        // Muerte
        animator.SetBool("muerto", muerto);
    }

    public void direccionPersonaje(){
        // Para hacer que el personaje mire a la izquierda o a la derecha cuando se mueva
        if (rigidbody.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-0.16f, 0.16f, 0.16f);

        }
        if (rigidbody.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);

        }
    }

    // metodo para cuando el prota se caiga al vacio
    public void Recolocar(){
        transform.position = new Vector3(xInicial, yInicial, 0);
    }

    public void RecibeDano(Vector2 direccion, int cantDano)
    {
        if (!recibiendoDano)
        {
            recibiendoDano = true;
            vida -= cantDano;
            sDanoProta.Play();

            if(vida <= 0){
                sMuerteProta.Play();
                muerto = true;
                animator.SetBool("ensuelo", enSuelo);
                animator.SetBool("muerto", muerto);
                Invoke(nameof(RestaurarProta), 1.5f);
            }

            if(!muerto){
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                rigidbody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }     
        }
    }

    private void RestaurarProta(){
        Recolocar(); 
        muerto = false;
        vida = 5;
        animator.SetBool("muerto", false);
        animator.Play("ProtaIdleAnimation");
        recibiendoDano = false;
    }

    public void DesactivaDano(){
        recibiendoDano = false;
        rigidbody.linearVelocity = Vector2.zero;
    }

    // para cuando el prota colisiona con el reloj
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("reloj")){
            totalPartesReloj++;
            ActualizarTexto();
            
        }
    }

    // metodo que actualiza el texto para controlar mejor esta actualizacion
    public void ActualizarTexto(){
        txtTotalRelojes.text = totalPartesReloj + "";
    }

}
