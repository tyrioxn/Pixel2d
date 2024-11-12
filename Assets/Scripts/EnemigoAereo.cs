/*using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float velocidad; 
    public Vector3 PosicionAbajo;
    public Vector3 PosicionArriba;
#region 
    //public float MaxY;
    //private float minY = -0.87f;
#endregion
    private bool subiendo;

  
    void Start()
    {
        #region 
        // Inicializa la posición del enemigo en el valor mínimo
      //  transform.position = new Vector3(transform.position.x, minY, transform.position.z);
      #endregion
      Vector3 PosicionAbajo = transform.position;
             subiendo = true;
        

    }

    void Update()
    {
    MoverEnemigo();
        #region 
        // Mueve el enemigo hacia arriba o hacia abajo
        /*if (subiendo)
        {
            transform.position += new Vector3(0, velocidad * Time.deltaTime, 0);
            if (transform.position.y >= MaxY)
            {
                subiendo = false; 
                Debug.Log("1");
            }
        }
        else
        {
            transform.position -= new Vector3(0, velocidad * Time.deltaTime, 0);
            if (transform.position.y <= minY)
            {
                subiendo = true;
            }
        }

    
    #endregion
    //hace lo mismo que lo comentado
     
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Jugador")){
            Debug.Log("menos 1 vida");
            //se llamara a finDeJuego();
            collision.gameObject.GetComponent<PlayerControler>().finDeJuego();
    
        
        }
    }

    private void MoverEnemigo(){
          Vector3 PosicionDestino = subiendo ? PosicionArriba : PosicionAbajo;
    transform.position = Vector3.MoveTowards(transform.position,PosicionDestino,velocidad*Time.deltaTime);
    if (transform.position == PosicionArriba) subiendo = false;
    else  if (transform.position == PosicionAbajo) subiendo = true;
    }
   
}
*/