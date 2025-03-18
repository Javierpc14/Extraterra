using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoProta : MonoBehaviour
{
    public float velocidad = 2;
    public float salto = 3;

    public Animator animator;

    Rigidbody2D rigidbody;

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate(){
        
        // movimiento
        if(Input.GetKey("d")){

            rigidbody.linearVelocity = new Vector2(velocidad, rigidbody.linearVelocity.y);

        }else if(Input.GetKey("a")){

            rigidbody.linearVelocity = new Vector2(-velocidad, rigidbody.linearVelocity.y);

        }else{

            rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);

        }
        
        // salto
        if(Input.GetKey("space") && ComprobarSuelo.siToca){
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, salto);
        }

        // Cambio de animaciones
        animator.SetFloat("movimiento", rigidbody.linearVelocity.x * velocidad);

        // Para mover que el personaje mire a la izquierda o a la derecha cuando se mueva
        if(rigidbody.linearVelocity.x < 0){
            transform.localScale = new Vector3(-0.16f, 0.16f, 0.16f);

        } if(rigidbody.linearVelocity.x > 0){
            transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);
            
        }

    }

}
