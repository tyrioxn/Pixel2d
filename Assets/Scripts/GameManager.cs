using UnityEngine;
using UnityEngine.SceneManagement; // Para manejar escenas
using TMPro; // Para trabajar con TextMeshPro

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textoTemporador; // Para TextMeshPro
    public TextMeshProUGUI textoVidas; // Para TextMeshPro
    public TextMeshProUGUI textoPuntuacion; // Para TextMeshPro

    public float tiempoLimite = 60f;
    private bool juegoEnCurso = true;

    private PlayerControler jugador;
    private bool haGanado;

    void Start()
    {
        // Verificar que las referencias a los componentes TextMeshProUGUI estén asignadas en el Inspector
        if (textoTemporador == null || textoVidas == null || textoPuntuacion == null)
        {
            Debug.LogError("Uno o más componentes TextMeshProUGUI no están asignados en el Inspector.");
        }

        // Obtener el componente PlayerControler
        jugador = FindObjectOfType<PlayerControler>();

        if (jugador == null)
        {
            Debug.LogError("No se ha encontrado un PlayerControler en la escena.");
        }

        // Actualizar los textos iniciales
        ActualizarTextoTemporador(tiempoLimite);
        ActualizarTextoVidas();
        ActualizarTextoPuntuacion();
    }

    void Update()
    {
        if (juegoEnCurso)
        {
            tiempoLimite -= Time.deltaTime;

            if (tiempoLimite <= 0)
            {
                tiempoLimite = 0;
                haGanado = false;
                FinDelJuego();
            }

            ActualizarTextoTemporador(tiempoLimite);
            VerificarPowerUpsRestantes();
            ActualizarTextoVidas();
            ActualizarTextoPuntuacion();
        }
    }

    private void ActualizarTextoTemporador(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        textoTemporador.text = $"Tiempo: {minutos}:{segundos:00}";
    }

    private void ActualizarTextoVidas()
    {
        if (jugador != null)
        {
            textoVidas.text = "Vidas: " + jugador.vidas.ToString();
        }
    }

    private void ActualizarTextoPuntuacion()
    {
        if (jugador != null)
        {
            textoPuntuacion.text = "Puntuación: " + jugador.Puntuacion.ToString();
        }
    }

    private void FinDelJuego()
    {
        if (!juegoEnCurso) return;

        juegoEnCurso = false;

        if (jugador != null)
        {
            jugador.IncrementarPuntos((int)tiempoLimite);
        }

        haGanado = jugador != null && jugador.vidas > 0;

        PlayerPrefs.SetInt("PuntuacionFinal", jugador.Puntuacion);
        PlayerPrefs.SetInt("HaGanado", haGanado ? 1 : 0);

        SceneManager.LoadScene("FinNivel");
    }

    private void VerificarPowerUpsRestantes()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        if (powerUps.Length == 0)
        {
            haGanado = true;
            FinDelJuego();
        }
    }
}
