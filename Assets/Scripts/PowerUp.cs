using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip powerup; // Clip de sonido del power-up
    public int puntosPowerup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            // Obtén la referencia al PlayerControler
            PlayerControler jugador = other.gameObject.GetComponent<PlayerControler>();
            if (jugador != null)
            {
                // Incrementa los puntos del jugador
                jugador.IncrementarPuntos(puntosPowerup);
                
                // Reproduce el sonido del power-up usando el AudioSource del PlayerControler
                jugador.GetComponent<AudioSource>().PlayOneShot(powerup, 0.7F);
                
                // Destruye el objeto power-up
                Destroy(gameObject);
                
                // Verifica si quedan power-ups en la escena
                VerificarPowerUpsRestantes();
            }
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