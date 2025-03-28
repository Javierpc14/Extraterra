using UnityEngine;
using System.Timers;
using UnityEngine.UI;

public class CambiarFondo : MonoBehaviour
{
    public Texture nuevaTextura;
    public Texture texturaOriginal;

    public Text txtTemporizador;

    private float temporizador = 0f;
    private bool fondoCambiado = false;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Q)){

            GameObject plane = GameObject.Find("Fondo1");
            if (plane != null)
            {
                Renderer planeRenderer = plane.GetComponent<Renderer>();
                planeRenderer.material.mainTexture = nuevaTextura;
            }

            temporizador = 5f;
            fondoCambiado = true;
        }

        if(fondoCambiado){

            // revisar txtTemporizador.text = temporizador + "";
            temporizador -= Time.deltaTime;
            

            if(temporizador <= 0f){
                GameObject plane = GameObject.Find("Fondo1");
                if(plane != null){
                    Renderer planeRenderer = plane.GetComponent<Renderer>();
                    planeRenderer.material.mainTexture = texturaOriginal;
                }

                fondoCambiado = false;
            }
        }
    }
}
