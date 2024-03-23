using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlueLaser : MonoBehaviour
{

    private List<Transform> pPositions = new List<Transform>();
    [SerializeField]private float maxZ = 3.4f;

    [SerializeField]private bool allMoveCheck = false;
    [SerializeField] private int nowP = 1;

    [SerializeField]private Transform posX;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            pPositions.Add(transform.GetChild(i));
            pPositions[i].position = transform.position;
            if (i == 0)
            {
                transform.GetChild(i).GetComponent<pPosCheck>().SetMove(true);
            }
        }
    }

    void Update()
    {
        movingZ();
        returnPosition();

    }

    private void movingZ()
    {
        if (allMoveCheck)
        {
            return;
        }

        if (pPositions[0].position.z > maxZ)
        {
            transform.GetChild(nowP).GetComponent<pPosCheck>().SetMove(true);
            nowP++;
            maxZ += 2.9f;
        }

        if (transform.GetChild(9).GetComponent<pPosCheck>().GetMove() == true)
        {
            allMoveCheck = true;
        }

       
        
    }

    private void returnPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            if (pPositions[i].position.z > 29.5f)
            {
                pPositions[i].GetComponent<pPosCheck>().SetZeroPosition(posX.position);
            }
        }
    }
}
