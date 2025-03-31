using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using System.Collections.Generic;

public class CambiarFondo : MonoBehaviour
{
    public Texture nuevaTextura;
    public Texture texturaOriginal;

    public Text txtTemporizador;

    private float temporizador = 0f;
    private bool fondoCambiado = false;

    public GameObject enemigo;
    private MovimientoEnemigo movimientoEnemigo;

    //variables para el cambio de enemigo a aliado
    // creo una lista que contenga los enemigos y los aliados
    public List<GameObject> enemigosLista = new List<GameObject>();
    public List<GameObject> aliadosLista = new List<GameObject>();

    // variable partes del reloj
    public GameObject prota;
    private MovimientoProta movimientoProta;

    void Start(){
        // añado a las listas los gameobject con los tag enemigo y aliado
        enemigosLista.AddRange(GameObject.FindGameObjectsWithTag("Enemigo"));
        aliadosLista.AddRange(GameObject.FindGameObjectsWithTag("aliado"));

        movimientoProta = prota.GetComponent<MovimientoProta>();
        movimientoEnemigo = enemigo.GetComponent<MovimientoEnemigo>();
    }

    void Update(){
        
        // reloj
        if(movimientoProta.totalPartesReloj >= 5){
            if (Input.GetKeyDown(KeyCode.Q)){

                GameObject plane = GameObject.Find("Fondo1");
                if (plane != null)
                {
                    Renderer planeRenderer = plane.GetComponent<Renderer>();
                    planeRenderer.material.mainTexture = nuevaTextura;
                }
                TransformarEnemigo(true);
                
                temporizador = 5f;
                fondoCambiado = true;
            }

            if(fondoCambiado){

                temporizador -= Time.deltaTime;

                //mostrar correctamente el tiempo por pantalla
                int tiempoMostrar = Mathf.Max(0, Mathf.CeilToInt(temporizador));
                txtTemporizador.text = tiempoMostrar.ToString();
                

                if(temporizador <= 0f){
                    GameObject plane = GameObject.Find("Fondo1");
                    if(plane != null){
                        Renderer planeRenderer = plane.GetComponent<Renderer>();
                        planeRenderer.material.mainTexture = texturaOriginal;
                    }

                    TransformarEnemigo(false);

                    txtTemporizador.text = "";
                    fondoCambiado = false;

                    // resto la cantidad de relojes que hay y las que se muestran por pantalla
                    // no se por que si lo pongo al principio no funciona
                    movimientoProta.totalPartesReloj -= 5;
                    movimientoProta.ActualizarTexto();
                }
            }
        }
    }

    void TransformarEnemigo(bool transformado)
    {
        for(int i = 0; i < enemigosLista.Count; i++){

            GameObject enemigo = enemigosLista[i];
            GameObject aliado = aliadosLista[i];

            // El spriteRenderer y el collider de los aliados empiezan desactivados por que 
            // al empezar la partida si matas a un enemigo sin utilizar la q el aliado se quedaba ahi ya que estaba detras del enemigo respecto a capas
            // y al usar la q el gameobject del aliado se desactivava al final por lo que funcionaba bien,
            // pero no podia empezar la partida con el gameobject de los aliados desactivados por que si no no entraban en la lista
            // por lo que no se cambiaba el sprite del enemigo al pulsar la Q
            // por lo que los aliados empiezan la partida con el spriteRenderer y el collider desactivados y cuando se pulsa la q se activan o desactivan para no interferir

            SpriteRenderer aliadoRenderer = aliado.GetComponent<SpriteRenderer>();
            Collider2D aliadoCollider = aliado.GetComponent<Collider2D>();
            
            if (transformado)
            {
                if(enemigo != null && aliado != null ){
                    enemigo.SetActive(false);

                    // si el enemigo no esta muerto aparece el aliado
                    if(!movimientoEnemigo.muerto){
                        aliadoRenderer.enabled = true;
                        aliadoCollider.enabled = true;
                    }
                    
                    aliado.SetActive(true);
                   
                }
                
            }
            else
            {
                if(enemigo != null && aliado != null){
                    enemigo.SetActive(true);
                    aliadoRenderer.enabled = false;
                    aliadoCollider.enabled = false;
                    aliado.SetActive(false);
                }
                
            }
        }
    }
}
