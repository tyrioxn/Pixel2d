using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int puntosPowerup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            other.gameObject.GetComponent<PlayerControler>().IncrementarPuntos(puntosPowerup);
            Destroy(gameObject);

            // Verifica si quedan power-ups en la escena
            VerificarPowerUpsRestantes();
        }
    }

    private void VerificarPowerUpsRestantes()
    {
        // Busca todos los objetos con la etiqueta "PowerUp"
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        // Si no quedan power-ups, detén el juego
        if (powerUps.Length == 0)
        {
            // Llama al método de fin de juego en PlayerControler
            PlayerControler jugador = FindObjectOfType<PlayerControler>();
            if (jugador != null)
            {
                jugador.finDeJuego(); // Asegúrate de que haya un método finDeJuego en PlayerControler
            }
        }
    }
}