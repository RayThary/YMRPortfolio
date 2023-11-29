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

    
    [SerializeField] BoxCollider box;

    //위아래로 움직이면 true 좌우로움직이면 false
    [SerializeField] private bool UPAndDown;
    //오른쪽시작이면 true 왼쪽시작이면 false
    [SerializeField] private bool Right;
    //위에서시작이면 true 왼쪽시작이면 false
    [SerializeField] private bool Up;

    [SerializeField] private Transform UpAndLeft;
    [SerializeField] private Transform DownAndRight;

    private float rightX;

    private float RightAndLeftMove;
    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        if (UPAndDown)
        {

        }
        else
        {
            if (Right)
            {
                rightX = (box.bounds.max.x - box.bounds.min.x) * 0.7f;

                startPoint = new Vector3(box.bounds.min.x + rightX, 0.1f, 0);
                midPoint = new Vector3(box.bounds.min.x + rightX, 0.1f, box.bounds.center.z);
                endPoint = new Vector3(box.bounds.min.x + rightX, 0.1f, box.bounds.center.z * 2);
                //RightAndLeftMove
            }
            else
            {
                rightX = (box.bounds.max.x - box.bounds.min.x) * 0.3f;

                startPoint = new Vector3(box.bounds.min.x + rightX, 0.1f, 0);
                midPoint = new Vector3(box.bounds.min.x + rightX, 0.1f, box.bounds.center.z);
                endPoint = new Vector3(box.bounds.min.x + rightX, 0.1f, box.bounds.center.z * 2);

            }
        }


        DownAndRight.position = startPoint;
        UpAndLeft.position = endPoint;
        lineRen.SetPosition(0, startPoint);
        lineRen.SetPosition(1, midPoint);
        lineRen.SetPosition(2, endPoint);


    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(midPoint, Vector3.up, Color.red);

        Debug.DrawLine(midPoint, Vector3.down, Color.blue);

        RaycastHit hit;
        if (UPAndDown)
        {
            if (Physics.Raycast(midPoint, Vector3.right, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
            {
                //startVec.x = hit.point.x;
            }
            if (Physics.Raycast(midPoint, Vector3.left, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
            {
                //endVec.x = hit.point.x;
            }
        }
        else
        {
            if (Physics.Raycast(midPoint, Vector3.up, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
            {
                //startVec.z = hit.point.z;
                 
            }
            if (Physics.Raycast(midPoint, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Wall")))
            {
                //endVec.z = hit.point.z;
            }
            if (Right)
            {
                

            }

            lineRen.SetPosition(0, startPoint);
            lineRen.SetPosition(1, midPoint);
            lineRen.SetPosition(2, endPoint);
        }




    }
}
