using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType3Patten1 : MonoBehaviour
{
    private LineRenderer lineRen;
    [SerializeField] BoxCollider box;//현재맵크기를알아오는곳 
    private Vector3 midPoint;//가운데위치
    [SerializeField]private Transform startPoint;//움직이는곳 중앙기준 한쪽
    [SerializeField]private Transform endPoint;//움직이는곳 중앙기준 나머지한쪽
    [SerializeField] private float speed;

    private Vector3 startVec;
    private Vector3 endVec;

    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        midPoint = new Vector3(box.bounds.center.x, 0.1f, box.bounds.center.z);
        
        lineRen.SetPosition(1, midPoint);
        transform.position = midPoint;
        startPoint.position = new Vector3(midPoint.x, midPoint.y, midPoint.z +5);
        endPoint.position = new Vector3(midPoint.x,midPoint.y,midPoint.z-5);
        
    }

    // Update is called once per frame
    void Update()
    {
        startPoint.RotateAround(midPoint, Vector3.up, speed * Time.deltaTime);
        endPoint.RotateAround(midPoint, Vector3.up, speed * Time.deltaTime);
        
        RaycastHit hit;
        float angle = Quaternion.FromToRotation(Vector3.up, startPoint.position - midPoint).x;
        Debug.Log(angle);
        transform.rotation = Quaternion.LookRotation(startPoint.position - midPoint);
        Vector3 angleS = transform.rotation.eulerAngles;
        //if(Physics.Raycast(midPoint,angleS,out hit))
        //{
        //    startVec = hit.point;
        //    Debug.DrawLine(midPoint, startVec, Color.red);
        //}

        if(Physics.Raycast(midPoint, startPoint.position-midPoint,out hit))
        {
            startVec = hit.point;
            Debug.DrawLine(midPoint, startVec, Color.red);
        }

        if (Physics.Raycast(midPoint, endPoint.position-midPoint, out hit))
        {
            endVec = hit.point;
            Debug.DrawLine(midPoint, endVec, Color.red);
        }
        lineRen.SetPosition(0, startVec);
        lineRen.SetPosition(2, endVec);
    }
}
