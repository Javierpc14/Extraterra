using UnityEngine;

public class SonidoReloj : MonoBehaviour
{
    
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            audioSource.Play();
        }
    }
}
