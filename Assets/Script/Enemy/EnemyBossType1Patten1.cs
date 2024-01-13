using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossType1Patten1 : MonoBehaviour
{
    private List<Vector3> pointVec = new List<Vector3>();
    BoxCollider box;
    private Vector3 center;
    private Vector3 endVec1;

    public Transform trs;
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Vector3 point1 = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
        Vector3 point2 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 3);
        Vector3 point3 = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z + 3);
        Vector3 point4 = new Vector3(transform.position.x - 4, transform.position.y, transform.position.z);
        Vector3 point5 = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z - 3);
        Vector3 point6 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z - 3);

        Gizmos.DrawLine(point1, point2);
        Gizmos.DrawLine(point2, point3);
        Gizmos.DrawLine(point3, point4);
        Gizmos.DrawLine(point4, point5);
        Gizmos.DrawLine(point5, point6);
        Gizmos.DrawLine(point6, point1);

        Vector3 p1 = (point1 + point2) / 2;
        Gizmos.DrawCube(p1, new Vector3(1, 1, 1));

        Gizmos.color = Color.blue;
        Vector3 p2 = (point2 + point3) / 2;
        Gizmos.DrawCube(p2, new Vector3(1, 1, 1));

        Gizmos.color = Color.white;
        Vector3 p3 = (point3 + point4) / 2;
        Gizmos.DrawCube(p3, new Vector3(1, 1, 1));

        Gizmos.color = Color.gray;
        Vector3 p4 = (point4 + point5) / 2;
        Gizmos.DrawCube(p4, new Vector3(1, 1, 1));

        Gizmos.color = Color.black;
        Vector3 p5 = (point5 + point6) / 2;
        Gizmos.DrawCube(p5, new Vector3(1, 1, 1));

        Gizmos.color = Color.cyan;
        Vector3 p6 = (point6 + point1) / 2;
        Gizmos.DrawCube(p6, new Vector3(1, 1, 1));

    }

    void Start()
    {
        box = GameManager.instance.enemyManager.GetStage.BoxCollider;
        center = box.center;
        //trs.rotation = Quaternion.Euler(0, 60, 0);


    }
    
    void Update()
    {
        halfHpPatten();
    }

    private void halfHpPatten()
    {

        RaycastHit hit;
        
        //if (Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity, LayerMask.GetMask("Wall"))) 
        //{
        //    endVec1 = hit.point;
        //}
        //Debug.DrawLine(transform.position, endVec1) ;
        //if(Physics.Linecast(transform.position,))
        

        for(int i=0;i>6; i++)
        {

        }
    }
}
