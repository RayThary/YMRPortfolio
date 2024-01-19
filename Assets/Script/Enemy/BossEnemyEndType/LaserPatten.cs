using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LaserPatten : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    private LineRenderer linRen;
    private Transform parentTrs;

    private BoxCollider box;

    private Transform playerTrs;
    private Vector3 targetVec;

    private bool firstCheck = false;
    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")) 
        {
            firstCheck = false;
            transform.position = parentTrs.position;
            PoolingManager.Instance.RemovePoolingObject(parentTrs.gameObject);
        }
    }
    void Start()
    {
        linRen = GetComponentInParent<LineRenderer>();
        parentTrs = linRen.GetComponent<Transform>();

        box = GetComponent<BoxCollider>();

        playerTrs = GameManager.instance.GetPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        tartgetMove();
        laserMove();
        hitCheck();
    }

    private void tartgetMove()
    {
        if (firstCheck == false)
        {
            transform.position= parentTrs.position;
            targetVec = playerTrs.transform.position - transform.position;
            targetVec.y = 0f;
            firstCheck = true;
        }
        transform.position += targetVec.normalized * Time.deltaTime * moveSpeed;
        
    }
    private void laserMove()
    {
        Vector3 spawnPos = parentTrs.position;
        spawnPos.y = 0f;
        linRen.SetPosition(0, spawnPos);
        linRen.SetPosition(1, transform.position);
    }


    private void hitCheck()
    {
        if (Physics.Linecast(parentTrs.position, transform.position, LayerMask.GetMask("Player")))
        {
            Debug.Log("hit");
            Player player = GameManager.instance.GetPlayer;
            player.Hit(null, 1);
        }
    }
}
