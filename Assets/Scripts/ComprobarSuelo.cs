using UnityEngine;

public class ComprobarSuelo : MonoBehaviour
{
    public static bool siToca;

    private void OnTriggerEnter2D(Collider2D other) {
        siToca = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        siToca = false;
    }
}
