using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textoTemporizador; // Referencia al componente TextMeshProUGUI para el temporizador
    public TextMeshProUGUI textoVidas; // Referencia al componente TextMeshProUGUI para mostrar las vidas
    public TextMeshProUGUI textoPuntuacion; // Referencia al componente TextMeshProUGUI para mostrar la puntuación
    public float tiempoLimite = 60f; // Tiempo en segundos (1 minuto)
    private bool juegoEnCurso = true; // Estado del juego

    private PlayerControler jugador; // Referencia al script PlayerControler

    void Start()
    {
        jugador = FindObjectOfType<PlayerControler>(); // Encuentra el script PlayerControler
        ActualizarTextoTemporador(tiempoLimite); // Muestra el tiempo inicial
        ActualizarTextoVidas(); // Actualiza el texto de vidas al inicio
        ActualizarTextoPuntuacion(); // Actualiza el texto de puntuación al inicio
    }

    void Update()
    {
        if (juegoEnCurso)
        {
            tiempoLimite -= Time.deltaTime; // Resta el tiempo transcurrido

            if (tiempoLimite <= 0)
            {
                tiempoLimite = 0; // Asegúrate de que no se vuelva negativo
                FinDelJuego(); // Llama a la función para terminar el juego
            }

            ActualizarTextoTemporador(tiempoLimite); // Actualiza el texto del temporizador
            VerificarPowerUpsRestantes(); // Verifica si quedan power-ups en la escena
            ActualizarTextoVidas(); // Actualiza el texto de vidas en cada frame
            ActualizarTextoPuntuacion(); // Actualiza el texto de puntuación en cada frame
        }
    }

      private void ActualizarTextoTemporador(float tiempo)
{
    int minutos = Mathf.FloorToInt(tiempo / 60); // Calcula los minutos
    int segundos = Mathf.FloorToInt(tiempo % 60); // Calcula los segundos

    // Formatea el texto para que siempre muestre dos dígitos en los segundos
    textoTemporizador.text = string.Format("Tiempo: {0}:{1:00}", minutos, segundos);
}
    private void ActualizarTextoVidas()
    {
        if (jugador != null)
        {
            textoVidas.text = "Vidas: " + jugador.vidas.ToString(); // Actualiza el texto con el número de vidas
        }
    }

    private void ActualizarTextoPuntuacion()
    {
        if (jugador != null)
        {
            textoPuntuacion.text = "Puntuación: " + jugador.Puntuacion.ToString(); // Actualiza el texto con la puntuación del jugador
        }
    }

    private void FinDelJuego()
    {
        if (!juegoEnCurso) return; // Evita que se ejecute más de una vez

        juegoEnCurso = false; // Cambia el estado del juego a no en curso

        // Suma el tiempo restante a la puntuación
        if (jugador != null)
        {
            jugador.IncrementarPuntos((int)tiempoLimite); // Suma el tiempo restante a la puntuación
            jugador.finDeJuego(); // Asegúrate de que haya un método finDeJuego en PlayerControler
        }
    }

    private void VerificarPowerUpsRestantes()
    {
        // Busca todos los objetos con la etiqueta "PowerUp"
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        // Si no quedan power-ups, detén el juego
        if (powerUps.Length == 0)
        {
            FinDelJuego(); // Llama a la función para terminar el juego
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            // Aquí puedes hacer que el jugador recoja el power-up
            int puntosPowerup = 10; // Define cuántos puntos da el power-up
            other.gameObject.GetComponent<PlayerControler>().IncrementarPuntos(puntosPowerup);
            Destroy(gameObject); // Destruye el power-up al recogerlo

            // Verifica si quedan power-ups en la escena
            VerificarPowerUpsRestantes();
        }
    }
}