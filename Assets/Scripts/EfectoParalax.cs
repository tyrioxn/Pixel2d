using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoParalax : MonoBehaviour
{
    public float velocidad;
    private Transform camara;
    private Vector3 ultimaPosionCamara;
    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main.transform.transform;
        ultimaPosionCamara = camara.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 movimientoFondo = camara.position - ultimaPosionCamara;
        transform.position += new Vector3(movimientoFondo.x * velocidad, movimientoFondo.y, 0);
        ultimaPosionCamara = camara.position;
    }
}
