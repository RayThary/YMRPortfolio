using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGround : MonoBehaviour
{
    private bool upTimeCheck = false;
    private bool downTimeCheck = false;
    private float timer = 0;
    [SerializeField] private float upSpeed = 1;
    private SpriteRenderer spr;
    private float dangerZoneTime = 0;

    private Transform playerTrs;
    void Start()
    {
        //��ȯ������ġ�� ��ȯ�ڿ����������ٰ� y���� -1.1�� �������((-y*0.5)-0.1  ������)
        transform.position = new Vector3(transform.position.x, -1.1f, transform.position.z);

        spr = GetComponentInChildren<SpriteRenderer>();

        dangerZoneTime = gameObject.GetComponentInChildren<DangerZone>().getTime();
        dangerZoneTime += 0.3f;

        playerTrs = GameManager.instance.GetPlayerTransform;
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
        }

        if (transform.position.y >= 0.5f)
        {
            upTimeCheck = false;
            downTimeCheck = true;
        }

        if (downTimeCheck)
        {
            transform.position += new Vector3(0, -upSpeed * Time.deltaTime, 0);
        }

        //������
        if (transform.position.y <= -1.3f)
        {

            downTimeCheck = false;
            upTimeCheck = false;
            timer = 0;
            transform.position = new Vector3(transform.position.x, -1.1f, transform.position.z);
            PoolingManager.Instance.RemovePoolingObject(gameObject);
        }
    }

    private (bool isCloseX, bool right, bool up) playerHitDirection()
    {
        Vector3 hitdirection = transform.position - playerTrs.position;

        return (hitdirection.x > hitdirection.y
        , playerTrs.position.x > transform.position.x
        , playerTrs.position.y > transform.position.y);
    }
}
