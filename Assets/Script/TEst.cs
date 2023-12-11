using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    public GameObject cube;
    public float angle;
    public Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = (cube.transform.position - transform.position).normalized;
        angle = Mathf.Acos(Vector3.Dot(transform.forward, target)) * Mathf.Rad2Deg;
        vector= Vector3.Cross(transform.right, target);
    }
}
