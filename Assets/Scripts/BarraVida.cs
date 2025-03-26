using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private MovimientoProta movimientoProta;
    private float vidaMaxima;

    void Start()
    {
        movimientoProta = GameObject.Find("Protagonista").GetComponent<MovimientoProta>();
        vidaMaxima = movimientoProta.vida;
    }

    void Update()
    {
        rellenoBarraVida.fillAmount = movimientoProta.vida / vidaMaxima;
    }
}
