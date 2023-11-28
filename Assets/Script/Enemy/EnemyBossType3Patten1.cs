using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType3Patten1 : MonoBehaviour
{
    private LineRenderer lineRen;
    [SerializeField] private Transform midPoint;
    private Transform startPoint;
    [SerializeField] private float speed;
    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        lineRen.SetPosition(1, midPoint.position);
        startPoint = transform;
        transform.position = lineRen.GetPosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        startPoint.RotateAround(midPoint.position, Vector3.up, speed * Time.deltaTime);
        lineRen.SetPosition(0, startPoint.position);
    }
}
