using UnityEngine;

public class proyectilEnemigo : MonoBehaviour
{
    public float velocidad;
    public int dano;

    private void Update(){
        transform.Translate(Time.deltaTime * velocidad * Vector2.right);
        // Elimina el proyectil despues de un tiempo si no impacta en el prota
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.TryGetComponent(out MovimientoProta movimientoProta)){

            Vector2 direccionDano = new Vector2(transform.position.x, 0);
            movimientoProta.RecibeDano(direccionDano, dano);
            Destroy(gameObject);
        }
    }
}
