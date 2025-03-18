using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoProta : MonoBehaviour
{
    public float velocidad = 2;
    public float salto = 3;
    public float fuerzaRebote = 10f;
    public int vida = 3;
    public Animator animator;
    private bool recibiendoDano;

    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // if(!recibiendoDano){
            // movimiento
            if (Input.GetKey("d") && !recibiendoDano)
            {
                rigidbody.linearVelocity = new Vector2(velocidad, rigidbody.linearVelocity.y);
            }
            else if (Input.GetKey("a") && !recibiendoDano)
            {
                rigidbody.linearVelocity = new Vector2(-velocidad, rigidbody.linearVelocity.y);
            }
            else
            {
                rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);
            }
        // }

        // salto
        if (Input.GetKey("space") && ComprobarSuelo.siToca && !recibiendoDano)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, salto);
        }
        //Animacion salto
        animator.SetBool("ensuelo", ComprobarSuelo.siToca);

        //Recibir Dano
        animator.SetBool("recibeDano", recibiendoDano);

        // Cambio de animaciones
        animator.SetFloat("movimiento", rigidbody.linearVelocity.x * velocidad);

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
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rigidbody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
        }

    }

    public void DesactivaDano(){
        recibiendoDano = false;
        rigidbody.linearVelocity = Vector2.zero;
    }

}
