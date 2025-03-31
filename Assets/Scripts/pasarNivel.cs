using UnityEngine;
using UnityEngine.SceneManagement;

public class pasarNivel : MonoBehaviour
{
    // verifico que el jugador colisiona con la puerta para saber si ha llegado al final del nivel
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            // cuando la puerta colisione con el jugador:
            // lo primero que se hara sera cargar la escena
            // el SceneManager.GetActiveScene().buildIndex identifica en que escena estamos nosotros y lo que hace es que le suma 1
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
