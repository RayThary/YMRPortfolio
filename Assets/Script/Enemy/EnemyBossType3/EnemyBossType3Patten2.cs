using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossType3Patten2 : MonoBehaviour
{
    private LineRenderer lineRen;
    //시작지점
    private Vector3 startPoint;
    private Vector3 midPoint;
    private Vector3 endPoint;

    //가로기준
    //0 의 z축은 시작지점 위치
    //1의 z 축은 끝지점 위치
    //세로면 x와 z를 바꾸면된다
    //움직이는 각각의좌표들
    private Vector3 startVec;
    private Vector3 midVec;
    private Vector3 endVec;
    [SerializeField] BoxCollider box;

    [SerializeField] private Transform upZ;
    [SerializeField] private Transform downZ;

    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        midPoint = new Vector3(box.bounds.center.x, 0.1f, box.bounds.center.z);
        startPoint = new Vector3(box.bounds.center.x, 0.1f, 0);
        endPoint = new Vector3(box.bounds.center.x, 0.1f, box.bounds.center.z * 2);

        downZ.position = startPoint;
        upZ.position = endPoint;
        lineRen.SetPosition(0, startPoint);
        lineRen.SetPosition(1, midPoint);
        lineRen.SetPosition(2, endPoint);


    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(midPoint, downZ.position, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
        {
            startVec = hit.point;
        }
        if (Physics.Raycast(midPoint, upZ.position, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
        {
            
            endVec = hit.point;
        }
        //float startXpos = box.center.
        float endXpos = box.bounds.center.x * 2;

        //if ()
        lineRen.SetPosition(0, startVec);
        lineRen.SetPosition(1, midVec);
        lineRen.SetPosition(2, endVec);

    }
}
