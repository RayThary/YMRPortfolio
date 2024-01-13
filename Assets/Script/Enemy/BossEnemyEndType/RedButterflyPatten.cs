using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButterflyPatten : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    //터지는시간
    [SerializeField] private float bombTime = 5;
    private float timer = 0;
    private bool bombCheck = false;

    private float bombtimer = 0;

    [SerializeField] private Transform parentTrs;
    private DangerZone dangerzone;
    private Transform target;

    void Start()
    {
        parentTrs = transform.parent.GetComponent<Transform>();
        target = GameManager.instance.GetPlayerTransform;

        dangerzone = GetComponent<DangerZone>();
        dangerzone.enabled = false;
    }

    void Update()
    {
        butterflyMove();
        butterflyBomb();
    }

    private void butterflyMove()
    {
        if (bombCheck == false)
        {
            timer += Time.deltaTime;
            //이동
            Vector3 moveTarget = target.position - transform.position;
            parentTrs.position += moveTarget * Time.deltaTime * moveSpeed;

            #region
            // 각도
            //Vector3 targetPos = target.position;
            //Vector3 fromPos = transform.position;
            //targetPos.y = 0f;
            //fromPos.y = 0f;
            //Vector3 targetVec = fromPos - targetPos;


            //float fixedAngle = (fromPos - targetPos).x < 0f == false ? -90f : 90;
            //float targetAgleY = Quaternion.FromToRotation(targetVec, Vector3.up).eulerAngles.y + fixedAngle;

            //parentTrs.rotation = Quaternion.Euler(new Vector3(0, targetAgleY, 0));
            #endregion

            parentTrs.LookAt(target);
            if (timer >= bombTime)
            {
                bombCheck = true;
                timer = 0;
                dangerzone.enabled = true;
            }
        }

    }


    private void butterflyBomb()
    {
        if (dangerzone.enabled == true)
        {
            bombtimer += Time.deltaTime;
            if (bombtimer >= 1)
            {
                StartCoroutine(crateButterfly());
                dangerzone.enabled = false;
            }
        }
    }

    IEnumerator crateButterfly()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject butterfly;
            float rangeX = Random.Range(-1, 1);
            float rangeZ = Random.Range(-1, 1);
            Vector3 randomPos = new Vector3(rangeX, 0, rangeZ);

            transform.position += randomPos;
            butterfly = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.RedButterflyBomb, GameManager.instance.GetenemyObjectBox);
            butterfly.transform.position = transform.position;
            
        }
    }

}
