using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int puntosPowerup;
    private void OnTriggerEnter2D (Collider2D other)
    {        
        if (other.CompareTag("Jugador"))
        {
            other.gameObject.GetComponent<PlayerControler>().IncrementarPuntos(puntosPowerup);
            Destroy(gameObject);
        }
    }
}
