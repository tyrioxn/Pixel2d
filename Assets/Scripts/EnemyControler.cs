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
    private SpriteRenderer spriteRenderer; // Para controlar la inversión del sprite.

    void Start()
    {
        // Inicializa la posición del enemigo en la posición izquierda.
        transform.position = PosicionIzquierda;

        // Obtener el componente Animator del hijo.
        AnimacionEnemigo = GetComponentInChildren<Animator>();

        // Obtener el SpriteRenderer del hijo.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Verificar si el spriteRenderer es null
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró un SpriteRenderer en el objeto enemigo o en sus hijos.");
        }

        moviendoDerecha = true; // Empieza moviéndose hacia la derecha.
    }

    void Update()
    {
        MoverEnemigo();
        AnimarEnemigo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
             PlayerControler jugador = other.GetComponent<PlayerControler>();
            jugador.QuitarVidas();
            Debug.Log("menos 1 vida");
            // Llamar a finDeJuego() del jugador.
        }
    }

    private void MoverEnemigo()
    {
        // Determinar la posición de destino dependiendo de la dirección.
        Vector3 PosicionDestino = moviendoDerecha ? PosicionIzquierda : PosicionDerecha;

        // Mover al enemigo hacia la posición de destino.
        transform.position = Vector3.MoveTowards(transform.position, PosicionDestino, velocidad * Time.deltaTime);

        // Cambiar la dirección cuando llega a la posición de destino.
        if (transform.position == PosicionIzquierda) 
        {
            moviendoDerecha = false;
            spriteRenderer.flipX = true; // Hacer flip en el sprite.
        }
        else if (transform.position == PosicionDerecha) 
        {
            moviendoDerecha = true;
            spriteRenderer.flipX = false; // Quitar flip en el sprite.
        }
    }

    private void AnimarEnemigo()
    {
        // Si el enemigo se está moviendo
        if (transform.position != PosicionIzquierda && transform.position != PosicionDerecha)
        {
            // Reproducir la animación "enemigo-anda" si está en movimiento
            AnimacionEnemigo.Play("enemigo-anda");
        }
        else 
        {
            AnimacionEnemigo.Play("enemigo-anda");
        }
    }
}