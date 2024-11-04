using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float velocidad;
    private  Rigidbody2D fisica;

    public GameObject disparao1;
    void Start(){
        fisica = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //movimiento horizontal
        float move = Input.GetAxis("Horizontal") *  velocidad * Time.deltaTime;
        transform.Translate(move, 0f, 0f);
        fisica.velocity = new Vector2(move * velocidad, fisica.velocity.y);
   
       

         if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instancia el proyectil en la posición del jugador y con su rotación
            GameObject projectileInstance = Instantiate(disparao1, transform.position, transform.rotation );
        }
    }
}
