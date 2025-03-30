using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform jugador;

    public Animator animator;
    private Rigidbody2D rigidbody;
    private bool recibiendoDano;
    private bool jugadorVivo;

    // variables de la vida del enemigo
    private bool muerto;
    public int vida = 3;

    // Variables de disparo
    public Transform controladorProyectil;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool jugadorEnRango;
    public GameObject proyectilEnemigo;
    public float tiempoEntreDisparos;
    public float tiempoUltimoDisparo;
    public float tiempoEsperaDisparo;

    //variables sonidos
    [SerializeField] private GameObject objDisparoEnemigo;
    [SerializeField] private GameObject objMuerteEnemigo;
    private AudioSource sDisparoEnemigo;
    private AudioSource sMuerteEnemigo;

    void Start()
    {
        jugadorVivo = true;

        sDisparoEnemigo = objDisparoEnemigo.GetComponent<AudioSource>();
        sMuerteEnemigo = objMuerteEnemigo.GetComponent<AudioSource>();

        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(jugadorVivo && !muerto){
            // para dibujar la linea con la cual sabe si atacar o no
            jugadorEnRango = Physics2D.Raycast(controladorProyectil.position, transform.right, distanciaLinea, capaJugador);
            // si la variable de jugador en rango es true significa que el enemigo debe disparar
            if(jugadorEnRango){
                if(Time.time > tiempoEntreDisparos + tiempoUltimoDisparo){
                    tiempoUltimoDisparo = Time.time;
                    Invoke(nameof(Disparar), tiempoEsperaDisparo);
                }
            }
        }

        animator.SetBool("RecibeDano", recibiendoDano);
    }

    // Para cuando el prota colisiona con el enemigo
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            Vector2 direccionDano = new Vector2(transform.position.x, 0);
            MovimientoProta movimientoProta = collision.gameObject.GetComponent<MovimientoProta>();

            movimientoProta.RecibeDano(direccionDano, 1);
            jugadorVivo = !movimientoProta.muerto;
        }
    }

    // Para cuando le dispare el prota por que el proyectil es un trigger
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Proyectil")){
            //esto elimina el proyectil del prota si impacta al enemigo
            Destroy(collision.gameObject);
            // esto elimina al enemigo
            // Destroy(gameObject);

            if(collision.gameObject.CompareTag("Proyectil")){
                Vector2 direccionDano = new Vector2(collision.gameObject.transform.position.x, 0);
                RecibeDano(direccionDano, 1);
            }
        }
    }

    public void RecibeDano(Vector2 direccion, int cantDano)
    {
        if (!recibiendoDano)
        {
            vida -= cantDano;
            recibiendoDano = true;
            
            if(vida <= 0){
                // en vez de eliminar el gameObject desactivo el sprite y el collider 
                // para que se reproduzca bien el sonido al matar al enemigo
                sMuerteEnemigo.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                muerto = true;
                //Destroy(gameObject);
            }else{
                // para que rebote
                // Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                // rigidbody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }   
        }

    }

    public void DesactivaDano(){
        recibiendoDano = false;
        rigidbody.linearVelocity = Vector2.zero;
    }

    private void Disparar(){
        sDisparoEnemigo.Play();
        Instantiate(proyectilEnemigo, controladorProyectil.position, controladorProyectil.rotation);
    }

}
