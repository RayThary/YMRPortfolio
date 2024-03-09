using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalPatten : MonoBehaviour
{
    [SerializeField]private bool playerTrsCheck = false;

    private Transform nowTrs;
    private Vector3 postionVec;
    void Start()
    {

    }

    
    void Update()
    {
        nowPostion();

    }

    private void nowPostion()
    {
        if (playerTrsCheck)
        {
            nowTrs = GameManager.instance.GetPlayerTransform;
            postionVec = nowTrs.position.normalized;
            playerTrsCheck = false;
            Debug.Log(postionVec);
        }
    }

}
