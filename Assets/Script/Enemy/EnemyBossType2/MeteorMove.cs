using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMove : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 0.4f;
 
    void Start()
    {
       
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        float posY = transform.position.y;

        if (posY <= 0.1f)
        {
            PoolingManager.Instance.RemovePoolingObject(gameObject);

        }
    }
}
