using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public Transform objetivo;
    public float velocidadCamara = 0.025f;
    public Vector3 desplazamiento;
    public float limiteInferiorY = -5f;


    private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + desplazamiento;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);

        // Mathf.Max compara la posicion en y de la camara con el limiteinferiorY
        // si la camara esta por encima del limite se mantiene en su posicion
        // si la camara baja del limite se fuerza su detencion
        posicionSuavizada.y = Mathf.Max(posicionSuavizada.y, limiteInferiorY);

        //posicion de la camara
        transform.position = posicionSuavizada;
    }
}
