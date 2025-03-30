using UnityEngine;

public class PinchoLogica : MonoBehaviour
{
    private bool jugadorVivo;
    
    void Start() {
        jugadorVivo = true;    
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            Vector2 direccionDano = new Vector2(transform.position.x, 0);
            MovimientoProta movimientoProta = collision.gameObject.GetComponent<MovimientoProta>();

            movimientoProta.RecibeDano(direccionDano, 1);
            jugadorVivo = !movimientoProta.muerto;
        }
    }
}
