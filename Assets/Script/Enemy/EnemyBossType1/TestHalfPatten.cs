using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHalfPatten : MonoBehaviour
{
    private Vector3 startTrs;//시작지점
    private List< Vector3>startTrsList = new List< Vector3 >();

    private Vector3 midTrs;//중간 
    private List<Vector3> midTrsList = new List<Vector3>();

    private Vector3 targetTrs;//도착지점

    [SerializeField] private float speed;
    [SerializeField][Range(0, 1)] private float value = 0;
    [SerializeField] private List<Transform> testObj = new List<Transform>();

    private Vector3 bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 P1 = Vector3.Lerp(p0, p1, t);
        Vector3 P2 = Vector3.Lerp(p1, p2, t);

        return Vector3.Lerp(P1, P2, t);
    }
    void Start()
    {
        startTrsList.Clear();
        startTrsList.Add(Vector3.zero); 
        startTrsList.Add(new Vector3(0, 0, 29));//첫좌표 위로
        startTrsList.Add(new Vector3(29, 0, 29));//첫좌표 대각선
        startTrsList.Add(new Vector3(29, 0, 0));//첫좌표 오른쪽

        targetTrs = GameManager.instance.GetPlayerTransform.position;

        for(int i = 0; i < startTrsList.Count; i++)
        {
            midTrsList.Add(Vector3.Lerp(startTrsList[i], targetTrs, 0.5f));
            float x;
            float z;
            if (i ==0)
            {
                x = Random.Range(startTrsList[i].x, targetTrs.x);
                z = Random.Range(targetTrs.z, 30);
                midTrsList[i] = new Vector3(x, 0, z);
            }
            else if (i == 1)
            {
                x = Random.Range(targetTrs.x, 30);
                z = Random.Range(targetTrs.z, startTrsList[i].z);
                midTrsList[i] = new Vector3(x, 0, z);
            }
            else if(i == 2)
            {
                x = Random.Range(targetTrs.x, 30);
                z = Random.Range(targetTrs.z, 0);
                midTrsList[i] = new Vector3(x, 0, z);
            }
            else if (i == 3)
            {
                x = Random.Range(0,targetTrs.x);
                z = Random.Range(startTrsList[i].z, targetTrs.z);
                midTrsList[i] = new Vector3(x, 0, z);
            }
        }
       

    }

    void Update()
    {
        for (int i = 0; i < startTrsList.Count; i++)
        {
            testObj[i].position = bezier(startTrsList[i], midTrsList[i], targetTrs, value);
        }
        value += Time.deltaTime * speed * 0.1f;
        if (value > 1)
        {

        }
    }


}
