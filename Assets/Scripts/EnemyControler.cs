using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidad; 
    public Vector3 PosicionIzquierda;
    public Vector3 PosicionDerecha;

    private bool moviendoDerecha;
    private Animator AnimacionEnemigo; // Para controlar la animación del hijo.

    void Start()
    {
        // Inicializa la posición del enemigo en la posición izquierda.
        transform.position = PosicionIzquierda;

        // Obtener el componente Animator del hijo.
        AnimacionEnemigo = GetComponentInChildren<Animator>();


        moviendoDerecha = true; // Empieza moviéndose hacia la derecha.
    }

    void Update()
    {
        MoverEnemigo();
        AnimarEnemigo();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            Debug.Log("menos 1 vida");
            // Llamar a finDeJuego() del jugador.
            collision.gameObject.GetComponent<PlayerControler>().finDeJuego();
        }
    }

    private void MoverEnemigo()
    {
        // Determinar la posición de destino dependiendo de la dirección.
        Vector3 PosicionDestino = moviendoDerecha ? PosicionDerecha : PosicionIzquierda;

        // Mover al enemigo hacia la posición de destino.
        transform.position = Vector3.MoveTowards(transform.position, PosicionDestino, velocidad * Time.deltaTime);

        // Cambiar la dirección cuando llega a la posición de destino.
        if (transform.position == PosicionDerecha) moviendoDerecha = false;
        else if (transform.position == PosicionIzquierda) moviendoDerecha = true;
    }

    private void AnimarEnemigo()
    {
        // Si el enemigo se está moviendo
        if (transform.position != PosicionIzquierda && transform.position != PosicionDerecha)
        {
            // Reproducir la animación "enemigo-anda" si está en movimiento
            AnimacionEnemigo.Play("enemigo-anda");
           
        }
        else {
            AnimacionEnemigo.Play("enemigo-anda");
        }
    }
}
