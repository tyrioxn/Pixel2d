using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour{
    public float velocidad; 
    public float MaxY;
    private float minY = -0.87f; 
    private bool subiendo = true; 

    void Start()
    {
        transform.position = new Vector3(transform.position.x, minY, transform.position.z);
    }

    void Update()
    {
        if (subiendo)  transform.position += new Vector3(0, velocidad * Time.deltaTime, 0);
            if (transform.position.y >= MaxY)  subiendo = false; 
          
        else
         transform.position -= new Vector3(0, velocidad * Time.deltaTime, 0);
         
       if (transform.position.y <= minY) subiendo = true;
        
                
        
    
}
}