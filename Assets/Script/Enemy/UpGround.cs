using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpGround : MonoBehaviour
{
    private bool upTimeCheck = false;
    private bool downTimeCheck = false;
    private float timer = 0;
    [SerializeField] private float upSpeed = 1;
    private SpriteRenderer spr;
    private float dangerZoneTime = 0;

    private bool stopWall = false;
    private float stopTime = 1.0f;
    private float stopTimer = 0.0f;

    private BoxCollider box;

    private Transform playerTrs;
    private Player player;


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.Hit(null, 2);
        }

    }
    void Start()
    {
        //소환해줄위치를 소환자에서지정해줄것 y값만 -1.1로 해줘야함((-y*0.5)-0.1  값으로)
        transform.position = new Vector3(transform.position.x, -1.1f, transform.position.z);

        spr = GetComponentInChildren<SpriteRenderer>();
        box = GetComponent<BoxCollider>();
        box.enabled = false;

        dangerZoneTime = gameObject.GetComponentInChildren<DangerZone>().getTime();
        dangerZoneTime += 0.3f;

        playerTrs = GameManager.instance.GetPlayerTransform;
        player = playerTrs.GetComponent<Player>();
    }


    void Update()
    {

        blockUpTime();
        blockUp();

    }

    private void blockUpTime()
    {
        if (downTimeCheck)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= dangerZoneTime)
        {
            upTimeCheck = true;
        }
    }

    private void blockUp()
    {
        if (upTimeCheck)
        {
            transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
            box.enabled = true;
        }

        if (transform.position.y >= 0.5f)
        {
            upTimeCheck = false;
            downTimeCheck = true;
        }

        if (downTimeCheck)
        {
            if (stopWall)
            {
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopTime)
                {
                    transform.position += new Vector3(0, -upSpeed * Time.deltaTime, 0);
                    if (transform.position.y <= 0)
                    {
                        box.enabled = false;
                    }
                }
            }
            else
            {
                transform.position += new Vector3(0, -upSpeed * Time.deltaTime, 0);
                if (transform.position.y <= 0)
                {
                    box.enabled = false;
                }
            }
        }
        
        //리무브
        if (transform.position.y <= -1.3f)
        {

            downTimeCheck = false;
            upTimeCheck = false;
            timer = 0;
            stopTimer = 0;
            stopWall = false;
            transform.position = new Vector3(transform.position.x, -1.1f, transform.position.z);
            PoolingManager.Instance.RemovePoolingObject(gameObject);
        }
    }

    public void SetStopTime(bool _stopWall, float _stopTime)
    {
        stopWall = _stopWall;
        stopTime = _stopTime;
    }

    public Vector3 playerHitDirection()
    {
        Vector3 hitdirection = transform.position - playerTrs.position;

        bool right = playerTrs.position.x > transform.position.x;
        bool up = playerTrs.position.z > transform.position.z;


        bool isCloseX = hitdirection.x > hitdirection.z;

        if (isCloseX && right)
        {
            Debug.Log("오른쪽 방향");
            return new Vector3(1, 0, 0);
        }
        else if (isCloseX && right == false)
        {
            Debug.Log("왼쪽 방향");
            return new Vector3(-1, 0, 0);
        }
        else if (isCloseX == false && up)
        {
            Debug.Log("위 방향");
            return new Vector3(0, 0, 1);
        }
        else if (isCloseX == false && up == false)
        {
            Debug.Log("아래 방향");
            return new Vector3(0, 0, -1);
        }
        else
        {
            Debug.LogError("VectorError");//불값이이상하게들어감
            return new Vector3(0, 0, 0);
        }




    }
}
