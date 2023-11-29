using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBossType3Patten1 : MonoBehaviour
{
    private LineRenderer lineRen;
    [SerializeField] BoxCollider box;//현재맵크기를알아오는곳 
    private Vector3 midPoint;//가운데위치
    [SerializeField] private Transform startPoint;//움직이는곳 중앙기준 한쪽
    [SerializeField] private Transform endPoint;//움직이는곳 중앙기준 나머지한쪽
    [SerializeField] private float speed;

    private Vector3 startVec;
    private Vector3 endVec;
    [SerializeField] private bool Lenght;
    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        midPoint = new Vector3(box.bounds.center.x, 0.1f, box.bounds.center.z);

        lineRen.SetPosition(1, midPoint);
        transform.position = midPoint;
        if (Lenght == true)
        {
            startPoint.position = new Vector3(midPoint.x, midPoint.y, midPoint.z + 5);
            endPoint.position = new Vector3(midPoint.x, midPoint.y, midPoint.z - 5);
        }
        else
        {
            startPoint.position = new Vector3(midPoint.x + 5, midPoint.y, midPoint.z);
            endPoint.position = new Vector3(midPoint.x - 5, midPoint.y, midPoint.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        startPoint.RotateAround(midPoint, Vector3.up, speed * Time.deltaTime);
        endPoint.RotateAround(midPoint, Vector3.up, speed * Time.deltaTime);

        RaycastHit hit;

        if (Physics.Raycast(midPoint, startPoint.position - midPoint, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
        {
            startVec = hit.point;
            Debug.DrawLine(midPoint, startVec, Color.red);
        }
        if (Physics.Raycast(midPoint, endPoint.position - midPoint, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
        {
            endVec = hit.point;
            Debug.DrawLine(midPoint, endVec, Color.red);
        }

        lineRen.SetPosition(0, startVec);
        lineRen.SetPosition(2, endVec);
    }
}
