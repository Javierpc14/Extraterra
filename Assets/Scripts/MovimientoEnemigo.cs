using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform jugador;
    public float RadioDeDeteccion = 5.0f;
    public float velocidad = 2.0f;
    public Animator animator;
    private Rigidbody2D rigidbody;
    private Vector2 movimiento;
    private bool recibiendoDano;
    private bool jugadorVivo;
    // variables de la vida del enemigo
    private bool muerto;
    public int vida = 3;


    void Start()
    {
        jugadorVivo = true;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(jugadorVivo && !muerto){
            Movimiento();
        }

        animator.SetBool("RecibeDano", recibiendoDano);
    }

    private void Movimiento(){
        float distanciaHastaJugador = Vector2.Distance(transform.position, jugador.position);

        if(distanciaHastaJugador < RadioDeDeteccion){
            Vector2 direccion = (jugador.position - transform.position).normalized;

            movimiento = new Vector2(direccion.x, transform.position.y);
        }else{
            movimiento = Vector2.zero;
        }

        rigidbody.MovePosition(rigidbody.position + movimiento * velocidad * Time.deltaTime);
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
                muerto = true;
                Destroy(gameObject);
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

}
