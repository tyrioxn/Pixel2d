using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float velocidad;
    private  Rigidbody2D fisica;
    public float arriba;
    private SpriteRenderer giro;
    public GameObject disparao1;
    void Start(){
        fisica = GetComponent<Rigidbody2D>();
        giro = GetComponent<SpriteRenderer>();
    }
    //todas las fisicas del juego aqui
    void FixedUpdate(){
         //movimiento horizontal
        float move = Input.GetAxis("Horizontal") *  velocidad * Time.deltaTime;
        transform.Translate(move, 0f, 0f);
        fisica.velocity = new Vector2(move * velocidad, fisica.velocity.y);
    }
    void Update()
    {
       
        //ASIGNAMOS TECLA PARA SALTAR
        if (Input.GetKeyDown(KeyCode.UpArrow))
            //AÑADIMOS FUERZA DE SALTO
        if (Suelo()) fisica.AddForce(Vector2.up * arriba, ForceMode2D.Impulse);
          
         if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instancia el proyectil en la posición del jugador y con su rotación
             Instantiate(disparao1, transform.position, transform.rotation );
        }
        //hace flip en el eje x para dar la vuelta al sprite
        if (fisica.velocity.x < 0f)giro.flipX = true;
        else if(fisica.velocity.x >0f) giro.flipX = false;   
    }
    //mira si el jugador toca el suelo para el siguiente salto
    private bool Suelo(){
        RaycastHit2D siToca = Physics2D.Raycast(transform.position + new Vector3(0f,-2f,0f), Vector2.down, 0.2f);
       return siToca.collider !=  null;
    }

}