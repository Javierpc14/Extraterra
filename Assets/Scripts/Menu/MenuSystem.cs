using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{

    [SerializeField] private GameObject objMusicaFondo;
    private AudioSource musicaFondo;

    private void Start() {
        musicaFondo = objMusicaFondo.GetComponent<AudioSource>();
        musicaFondo.Play();
    }

    public void Jugar(){
        // se encarga de cargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir(){
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
