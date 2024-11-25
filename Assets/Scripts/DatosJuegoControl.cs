using UnityEngine;

public class DatosJuegoControl : MonoBehaviour
{
    private int puntuacion;
    private bool haGanado;

    public int Puntuacion { get => puntuacion; set => puntuacion = value; }
    public bool HaGanado { get => haGanado; set => haGanado = value; }

    void Awake()
    {
        // Asegurarse de que no haya múltiples instancias de este objeto
        int numeroInstancias = FindObjectsOfType<DatosJuegoControl>().Length;

        if (numeroInstancias > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);  // No destruir el objeto entre escenas
        }
    }
}
