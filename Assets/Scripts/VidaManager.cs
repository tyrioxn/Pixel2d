using UnityEngine;
using TMPro;

public class VidaManager : MonoBehaviour
{
    public TextMeshProUGUI textoVidas; // Referencia al componente TextMeshProUGUI para mostrar las vidas
    private PlayerControler jugador; // Referencia al script PlayerControler

    void Start()
    {
        jugador = FindObjectOfType<PlayerControler>(); // Encuentra el script PlayerControler
        ActualizarTextoVidas(); // Actualiza el texto al inicio
    }

    void Update()
    {
        // Actualiza el texto de vidas en cada frame
        ActualizarTextoVidas();
    }

    private void ActualizarTextoVidas()
    {
        if (jugador != null)
        {
            textoVidas.text = "Vidas: " + jugador.vidas.ToString(); // Actualiza el texto con el número de vidas
        }
    }
}