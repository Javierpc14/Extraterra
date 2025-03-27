using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void Jugar(){
        // se encarga de cargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir(){
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
