using System;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float velocidad;
    public Vector3 PosicionAbajo;
    public Vector3 PosicionArriba;

    private bool subiendo;

    void Start()
    {
        // Inicializa la posición del enemigo en PosicionAbajo
        transform.position = PosicionAbajo;
        subiendo = true;
    }

    void Update()
    {
        MoverEnemigo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            PlayerControler jugador = other.GetComponent<PlayerControler>();
            jugador.QuitarVidas();
            Debug.Log("menos 1 vida");
            // Se llamará a finDeJuego();
        }
    }

    private void MoverEnemigo()
    {
        // Mueve el enemigo hacia la posición de destino
        Vector3 PosicionDestino = subiendo ? PosicionArriba : PosicionAbajo;
        transform.position = Vector3.MoveTowards(transform.position, PosicionDestino, velocidad * Time.deltaTime);

        // Cambia la dirección si se ha alcanzado la posición de destino
        if (transform.position == PosicionArriba)
        {
            subiendo = false;
        }
        else if (transform.position == PosicionAbajo)
        {
            subiendo = true;
        }
    }
}