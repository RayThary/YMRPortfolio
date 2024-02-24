using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;

    public float x;


    void Start()
    {

    }
    public float rotationSpeed = 100f;

    void Update()
    {

            transform.position += transform.forward * Time.deltaTime * speed * 0.5f;
            transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime);
        if (speed < 8)
        {
            speed += Time.deltaTime * x;
        }
        
      
    }
 
}
