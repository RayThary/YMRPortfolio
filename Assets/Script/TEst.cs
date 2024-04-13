using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("2");
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
