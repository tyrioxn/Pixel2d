using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion; // Referencia al componente TextMeshProUGUI
    private PlayerControler jugador; // Referencia al script del jugador

    void Start()
    {
        jugador = FindObjectOfType<PlayerControler>(); // Encuentra el objeto del jugador
        ActualizarPuntuacion(); // Muestra la puntuación inicial
    }

    void Update()
    {
        // Actualiza la puntuación cada vez que cambia
        ActualizarPuntuacion();
    }

    private void ActualizarPuntuacion()
    {
        if (jugador != null)
        {
            textoPuntuacion.text = "Puntuación: " + jugador.Puntuacion.ToString(); // Actualiza el texto con la puntuación actual
        }
    }
}