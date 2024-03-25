using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bezir : MonoBehaviour
{
    [SerializeField] private int dot;

    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject p3;

    LineRenderer line;
    public Vector3 bezier(Vector3 P0, Vector3 P1, Vector3 P2, float t)
    {
        Vector3 m0 = Vector3.Lerp(P0, P1, t);
        Vector3 m1 = Vector3.Lerp(P1, P2, t);

        return Vector3.Lerp(m0, m1, t);
    }

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        s();
    }

    private void s()
    {

        line.positionCount = dot;

        for (int i = 0; i < dot; i++)
        {
            float t;
            t = i / dot;
            Vector3 be = bezier(p1.transform.position, p2.transform.position, p3.transform.position, t);

            line.SetPosition(i, be);
        }
    }
}
