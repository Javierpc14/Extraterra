using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform jugador;
    public float RadioDeDeteccion = 5.0f;
    public float velocidad = 2.0f;

    private Rigidbody2D rigidbody;
    private Vector2 movimiento;
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            Vector2 direccionDano = new Vector2(transform.position.x, 0);

            collision.gameObject.GetComponent<MovimientoProta>().RecibeDano(direccionDano, 1);
        }
    }
}
