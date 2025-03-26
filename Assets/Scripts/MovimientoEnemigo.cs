using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform jugador;
    public float RadioDeDeteccion = 5.0f;
    public float velocidad = 2.0f;

    private Rigidbody2D rigidbody;
    private Vector2 movimiento;

    private bool recibiendoDano;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
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

            collision.gameObject.GetComponent<MovimientoProta>().RecibeDano(direccionDano, 1);
        }
    }

    // Para cuando le dispare el prota
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Proyectil"){
            //esto elimina el proyectil del prota si impacta al enemigo
            Destroy(other.gameObject);
            // // esto elimina al enemigo
            // Destroy(gameObject);

            if(other.gameObject.CompareTag("Proyectil")){
                Vector2 direccionDano = new Vector2(other.gameObject.transform.position.x, 0);

                RecibeDano(direccionDano, 1);
            }
        }
    }

    public void RecibeDano(Vector2 direccion, int cantDano)
    {
        if (!recibiendoDano)
        {
            recibiendoDano = true;
            // para que rebote
            // Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            // rigidbody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
        }

    }

}
