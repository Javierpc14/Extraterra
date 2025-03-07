using UnityEngine;

public class MovimientoParralax : MonoBehaviour
{
    //Posicion de la camara para que se mueva el fondo
    Transform cam;
    // posicion inicial de la camara, para que el parallax identifique donde se encuentra y asi la repeticion de fondo se vea mejor
    Vector3 camPosInicial;
    // Distancia que se recorrio desde la camara hasta la posicion inicial
    float distancia;

    // Lista de onjetos con cada fondo
    GameObject[] fondos;
    // Materiales de cada fondo (shaders)
    Material[] mat;
    // array con la velocidad que va a tener cada fondo
    float[] velocidadFondos;

    // Obtiene el fondo mas lejano
    float fondoMasLejano;

    // Un rango para poder modificar la velocidad del parallax
    [Range(0.01f, 1f)]
    public float velocidadParallax;

    void Start()
    {
        // Inicializo las variables de las camaras con sus respectivas posiciones
        cam = Camera.main.transform;
        camPosInicial = cam.position;

        // Obtengo el numero total de hijos que hay (el numero total de fondos)
        int contadorFondos = transform.childCount;
        // Asigno a cda Array el numero total de fondos que hay
        mat = new Material[contadorFondos];
        velocidadFondos = new float[contadorFondos];
        fondos = new GameObject[contadorFondos];

        // Este for se asigna cada fondo del array obteniendo los hijos del gameObject padre
        // esto igualmente con el material
        for (int i = 0; i < contadorFondos; i++)
        {
            fondos[i] = transform.GetChild(i).gameObject;
            mat[i] = fondos[i].GetComponent<Renderer>().material;
        }

        // Llamo al metodo calcularVelocidadFondos que se encarga de calcular la velocidad que tendra cada fondo
        calcularVelocidadFondos(contadorFondos);
    }

    void calcularVelocidadFondos(int contadorFondos)
    {
        // Primero obtiene la posicion del fondo mas lejano
        for (int i = 0; i < contadorFondos; i++)
        {
            if ((fondos[i].transform.position.z - cam.position.z) > fondoMasLejano)
            {
                // Para esto resta la posicion de los fondos - la posicion de la camara
                fondoMasLejano = fondos[i].transform.position.z - cam.position.z;
            }
        }
        // Despues obtengo la velocidad de cada fondo
        for (int i = 0; i < contadorFondos; i++)
        {
            // Esto haciendo que sea 1 - la posicion del fondo en z - la de la camara / su distancia segun su lejania con la camara
            // Cuanto mas lejana sea la posicion del fondo mayor sera su valor, por lo que al dividir un numero cada vez mas mayor el movimiento se ira reduciendo
            velocidadFondos[i] = 1 - (fondos[i].transform.position.z - cam.position.z) / fondoMasLejano;
        }
    }

    // Este metodo ya se encarga de mover el fondo
    private void LateUpdate()
    {
        // Esta es la distancia que hay segun las posiciones de las camaras
        distancia = cam.position.x - camPosInicial.x;
        // Esto es para que el fondo siga la dirección de la camara
        transform.position = new Vector3(cam.position.x - 1, transform.position.y, 1.97f);

        // Aqui ya se mueve cada fondo asignandole su velocidad
        for (int i = 0; i < fondos.Length; i++)
        {
            float speed = velocidadFondos[i] * velocidadParallax;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distancia, 0) * speed);
        }
    }
}
