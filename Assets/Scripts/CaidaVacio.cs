using UnityEngine;

public class CaidaVacio : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        //para cuando el prota interactue con el box colider 
        FindObjectOfType<MovimientoProta>().SendMessage("Recolocar");
    }
}
