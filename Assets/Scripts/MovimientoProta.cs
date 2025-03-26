using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoProta : MonoBehaviour
{
    public float velocidad = 2;
    public float salto = 3;
    public float fuerzaRebote = 10f;

    public int vida = 3;
    public bool muerto;

    public Animator animator;
    private bool recibiendoDano;
    Rigidbody2D rigidbody;
    // Variables proyectil
    public float velocidadProyectil = 1000f;
    public GameObject proyectil;
    //Esta variable es para saber si el proyectil tiene que moverse a la derecha o a la izquierda
    // 1 derecha / 2 izquierda
    public int direccionProyectil = 1;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        movimiento();

        saltoProta();

        animaciones();

        direccionPersonaje();  
    }

    // Utilizo la funcion update para la bala debido a que FixedUpdate se ejecuta a un ritmo fijo,
    // lo que hace que al pulsar la tecla de disparo no siempre haga salir una bala
    void Update(){
        // GetKeyUp para que no se espameen las balas
        // utilizo el KeyCode.E porque funciona mejor
        if(Input.GetKeyDown(KeyCode.E)){
            //aqui se especifica la direccion y la rotacion
            GameObject goProyectil = Instantiate(proyectil, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation);

            if(direccionProyectil == 1){
                goProyectil.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(velocidadProyectil * Time.deltaTime, 0f);
            }else if(direccionProyectil == 2){
                goProyectil.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-velocidadProyectil * Time.deltaTime, 0f);
            }

            Destroy(goProyectil, 3f);
        }
    }

    public void movimiento(){
        // if(!recibiendoDano){
            // movimiento
        if(!muerto){
            if (Input.GetKey("d") && !recibiendoDano)
            {
                rigidbody.linearVelocity = new Vector2(velocidad, rigidbody.linearVelocity.y);
                // al pulsar la d (derecha) indico que la bala se mueva a la derecha
                direccionProyectil = 1;
            }
            else if (Input.GetKey("a") && !recibiendoDano)
            {
                rigidbody.linearVelocity = new Vector2(-velocidad, rigidbody.linearVelocity.y);
                // al pulsar la a (izquierda) indico que la bala se mueva a la derecha
                direccionProyectil = 2;
            }
            else
            {
                rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);
            }
        } 
        // }
    }

    public void saltoProta(){
        // salto
        if (Input.GetKey("space") && ComprobarSuelo.siToca && !recibiendoDano)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, salto);
        }
    }

    public void animaciones(){
         //Animacion salto
        animator.SetBool("ensuelo", ComprobarSuelo.siToca);

        //Recibir Dano
        animator.SetBool("recibeDano", recibiendoDano);

        // Cambio de animaciones
        animator.SetFloat("movimiento", rigidbody.linearVelocity.x * velocidad);

        // Muerte
        animator.SetBool("muerto", muerto);
    }

    public void direccionPersonaje(){
        // Para hacer que el personaje mire a la izquierda o a la derecha cuando se mueva
        if (rigidbody.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-0.16f, 0.16f, 0.16f);

        }
        if (rigidbody.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);

        }
    }

    public void RecibeDano(Vector2 direccion, int cantDano)
    {
        if (!recibiendoDano)
        {
            recibiendoDano = true;
            vida -= cantDano;
            if(vida <= 0){
                muerto = true;
            }

            if(!muerto){
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                rigidbody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }     
        }

    }

    public void DesactivaDano(){
        recibiendoDano = false;
        rigidbody.linearVelocity = Vector2.zero;
    }

}
