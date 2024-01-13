using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPatten : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    private LineRenderer linRen;
    private Transform target;
    private Transform playerTrs;

    void Start()
    {
        linRen = GetComponent<LineRenderer>();
        target = transform.GetChild(0);
        playerTrs = GameManager.instance.GetPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        laserMove();
        tartgetMove();

    }
    private void laserMove()
    {
        linRen.SetPosition(0, transform.position);
        linRen.SetPosition(1, target.position);
    }

    private void tartgetMove()
    {
        Vector3 targetVec = playerTrs.transform.position - target.transform.position;

        target.transform.position += targetVec * Time.deltaTime * moveSpeed;
    }

}
