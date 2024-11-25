using UnityEngine;
using UnityEngine.UI; // Para trabajar con textos Legacy

public class ControlFinNivel : MonoBehaviour
{
    public Text mensajeFinalTxt; // Referencia al texto para mostrar el mensaje

    void Start()
    {
        int puntuacion = PlayerPrefs.GetInt("PuntuacionFinal", 0);
        bool haGanado = PlayerPrefs.GetInt("HaGanado", 0) == 1;

        string mensaje = haGanado ? "HAS GANADO!" : "UPS... HAS PERDIDO";
        mensaje += $"\nPUNTUACIÓN: {puntuacion}";

        mensajeFinalTxt.text = mensaje;
    }
}
