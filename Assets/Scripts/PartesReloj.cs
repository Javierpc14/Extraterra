using UnityEngine;

public class PartesReloj : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
