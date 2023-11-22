using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransform : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_target.position;    
    }
}
