using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class pPosCheck : MonoBehaviour
{
    private float speed = 3;
    [SerializeField]private bool movingStart = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingStart)
        {
            transform.position += new Vector3(0, 0, 1) * Time.deltaTime * speed;
        }

        
    }

    public void SetMove(bool _value)
    {
        movingStart = _value;
    }

    public bool GetMove()
    {
        return movingStart;
    }

    public void SetZeroPosition(Vector3 _value)
    {
        transform.position = _value;
    }
}
