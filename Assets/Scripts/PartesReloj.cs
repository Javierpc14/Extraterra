using UnityEngine;

public class PartesReloj : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player")){
            // en vez de eliminar el gameObject desactivo el sprite y el collider 
            // para que se reproduzca bien el sonido al coger los relojes
            audioSource.Play();
            GetComponent<SpriteRenderer>().enabled = false; // Oculta el objeto para que parezca destruido
            GetComponent<Collider2D>().enabled = false;
            //Destroy(gameObject);
            
        }
    }
}
