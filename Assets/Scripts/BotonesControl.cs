using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesControl : MonoBehaviour
{
    public void OnBotonJugar()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void OnBotonCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void OnBotonMenu()
{
    // Asegurarse de que no haya instancias duplicadas del objeto DatosJuegoControl
    DatosJuegoControl[] instances = FindObjectsOfType<DatosJuegoControl>();
    if (instances.Length > 1)
    {
        Destroy(instances[1].gameObject); // Elimina las instancias adicionales
    }

    // Luego cargar la escena "Menu"
    SceneManager.LoadScene("Menu");
}

    public void OnBotonSalir()
    {
        Application.Quit();
    }
}
