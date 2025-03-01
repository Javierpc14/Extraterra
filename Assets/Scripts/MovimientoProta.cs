using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoProta : MonoBehaviour
{
    public float velocidad = 2;
    public float salto = 3;

    Rigidbody2D rigidbody;

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate(){
        if(Input.GetKey("d")){

            rigidbody.linearVelocity = new Vector2(velocidad, rigidbody.linearVelocity.y);

        }else if(Input.GetKey("a")){

            rigidbody.linearVelocity = new Vector2(-velocidad, rigidbody.linearVelocity.y);

        }else{

            rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);

        }

        if(Input.GetKey("space") && ComprobarSuelo.siToca){
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, salto);
        }

    }

}
