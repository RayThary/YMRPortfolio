using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType1Patten1 : MonoBehaviour
{
    private List<Vector3> pointVec = new List<Vector3>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 point1 = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
        Vector3 point2 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2);
        Vector3 point3 = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z + 2);
        Vector3 point4 = new Vector3(transform.position.x - 4, transform.position.y, transform.position.z);
        Vector3 point5 = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z - 2);
        Vector3 point6 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z - 2);

        Gizmos.DrawLine(point1, point2);
        Gizmos.DrawLine(point2, point3);
        Gizmos.DrawLine(point3, point4);
        Gizmos.DrawLine(point4, point5);
        Gizmos.DrawLine(point5, point6);
        Gizmos.DrawLine(point6, point1);

    }

    void Start()
    {
        Vector3 point1 = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
        Vector3 point2 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2);

        Debug.DrawLine(point1, point2, Color.red);



    }

    void Update()
    {

    }
}
