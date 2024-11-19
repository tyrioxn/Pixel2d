using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI textoTemporizador; // Referencia al componente TextMeshProUGUI
    public float tiempoLimite = 60f; // Tiempo en segundos (1 minuto)
    private bool juegoEnCurso = true; // Estado del juego

    void Start()
    {
        ActualizarTextoTemporador(tiempoLimite); // Muestra el tiempo inicial
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

            // Verifica si quedan power-ups en la escena
            VerificarPowerUpsRestantes();
        }
    }

    private void ActualizarTextoTemporador(float tiempo)
{
    int minutos = Mathf.FloorToInt(tiempo / 60); // Calcula los minutos
    int segundos = Mathf.FloorToInt(tiempo % 60); // Calcula los segundos

    // Formatea el texto para que siempre muestre dos dígitos en los segundos
    textoTemporizador.text = string.Format("Tiempo: {0}:{1:00}", minutos, segundos);
}

    public float GetTiempoRestante()
    {
        return tiempoLimite; // Devuelve el tiempo restante
    }

    private void FinDelJuego()
    {
        if (!juegoEnCurso) return; // Evita que se ejecute más de una vez

        juegoEnCurso = false; // Cambia el estado del juego a no en curso

        // Aquí puedes llamar al método de fin de juego en tu PlayerControler
        PlayerControler jugador = FindObjectOfType<PlayerControler>();
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
}