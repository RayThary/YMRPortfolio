using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{

    private Player player;
    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.RhombusLaser, transform);
        } 
    }

}
