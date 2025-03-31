using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSystem : MonoBehaviour
{
    [SerializeField] private GameObject objMusicaFondo;
    private AudioSource musicaFondo;

    private void Start() {
        musicaFondo = objMusicaFondo.GetComponent<AudioSource>();
        musicaFondo.Play();
    }
    
    public void Inicio(){
        // se encarga de cargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Salir(){
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
