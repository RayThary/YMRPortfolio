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
            Debug.Log("º®");
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
        laserMove();
        tartgetMove();
        hitCheck();
    }
    private void laserMove()
    {
        linRen.SetPosition(0, parentTrs.position);
        linRen.SetPosition(1, transform.position);
    }

    private void tartgetMove()
    {
        if (firstCheck == false)
        {
            transform.position= parentTrs.position;
            targetVec = playerTrs.transform.position - transform.position;
            firstCheck = true;
        }
        transform.position += targetVec * Time.deltaTime * moveSpeed;

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
