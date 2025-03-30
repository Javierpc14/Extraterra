using UnityEngine;

public class CaidaVacio : MonoBehaviour
{
    private MovimientoProta movimientoProta;
    public GameObject prota;

    void Start() {
        movimientoProta = prota.GetComponent<MovimientoProta>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //para cuando el prota interactue con el box colider
        movimientoProta.vida = 5;
        FindObjectOfType<MovimientoProta>().SendMessage("Recolocar");
        
    }
}
