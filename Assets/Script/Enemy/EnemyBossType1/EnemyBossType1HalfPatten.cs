using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBossType1HalfPatten : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private List<Transform> pPositions = new List<Transform>();

    private bool lineFirstPositionCheck = false;
    private int pPosLastNumber;

    private bool allMoveCheck = false;
    private float maxZ = 3.4f;
    private int nowMovePoint = 8;

    [SerializeField] private Transform posX;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        int count = line.positionCount - 1;

        for (int i = 0; i < count; i++)
        {
            pPositions.Add(transform.GetChild(i));
        }
        pPosLastNumber = pPositions.Count - 1;

    }

    // Update is called once per frame
    void Update()
    {
        laserPatten();
        movingZ();
        returnPosition();
    }

    private void laserPatten()
    {
        if (lineFirstPositionCheck == false)
        {


            for (int i = 0; i < pPosLastNumber; i++)
            {
                pPositions[i].position = posX.position;
            }

            transform.GetChild(pPosLastNumber).GetComponent<pPosCheck>().SetMove(true);
            lineFirstPositionCheck = true;
        }

        line.SetPosition(0, posX.position);
        int count = line.positionCount - 1;

        for (int i = count; i > 0; i--)
        {
            line.SetPosition(i, pPositions[i - 1].position);
        }
    }


    private void movingZ()
    {
        if (allMoveCheck)
        {
            return;
        }
        Debug.Log(pPositions[pPosLastNumber].position.z);
        if (pPositions[pPosLastNumber].position.z > maxZ)
        {
            transform.GetChild(nowMovePoint).GetComponent<pPosCheck>().SetMove(true);
            nowMovePoint--;
            maxZ += 2.9f;
        }

        if (transform.GetChild(0).GetComponent<pPosCheck>().GetMove() == true)
        {
            allMoveCheck = true;
        }
    }

    private void returnPosition()
    {
        for (int i = 0; i <= pPosLastNumber; i++)
        {
            if (pPositions[i].position.z > 29.5f)
            {
                pPositions.Add(pPositions[i]);
                pPositions[i].GetComponent<pPosCheck>().SetZeroPosition(posX.position);
                pPositions.RemoveAt(0);
            }
        }
    }

    public void eset()
    {
        maxZ = 3.4f;
        nowMovePoint = 1;
        lineFirstPositionCheck = false;
        allMoveCheck = false;
    }
}
