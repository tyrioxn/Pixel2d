using UnityEngine;

public class Disparo : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad del disparo
    public int puntosPorImpacto = 10; // Puntos que se sumarán al impactar
    private Vector2 direccion; // Para almacenar la dirección del disparo

    // Método para establecer la dirección del disparo
    public void SetDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion.normalized; // Normalizar para asegurar que tenga una longitud de 1
    }

    void Start()
    {
        // Destruir el disparo después de 3 segundos si no ha impactado
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        // Mover el disparo en la dirección especificada
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Comprobar si el disparo colisiona con un enemigo
        if (col.CompareTag("Enemigo")) // Asegúrate de que tus enemigos tengan la etiqueta "Enemigo"
        {
            // Lógica para sumar puntuación
            PlayerControler jugador = FindObjectOfType<PlayerControler>();
            if (jugador != null)
            {
                jugador.IncrementarPuntos(puntosPorImpacto);
            }

            // Destruir el enemigo
            Destroy(col.gameObject);

            // Destruir el disparo
            Destroy(gameObject);
        }
    }
}