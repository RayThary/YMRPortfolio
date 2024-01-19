using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossCube : Unit
{
    //테스트
    [SerializeField] private bool halfPattenCheck;
    //기본스킬들 재사용 시간
    [SerializeField]private float basicAttackTimer = 3;

    private bool upGroundPattening = false;

    private Transform playerTrs;
    

    void Start()
    {
        playerTrs = GameManager.instance.GetPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {

        attackPatten();
        HaxagonPatten();
    }

    private void attackPatten()
    {
        basicAttackTimer += Time.deltaTime;
        if (basicAttackTimer >= 2.5f)
        {
            int pattenNum = Random.Range(0, 3);
            if (pattenNum == 0)
            {
                Debug.Log("patten0");
                GameObject attackObj;
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.LaserPatten, transform);
                attackObj.transform.position = transform.position;
                basicAttackTimer = 0;
            }
            else if (pattenNum == 1)
            {
                Debug.Log("patten1");
                GameObject attackObj;
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.RedButterfly, transform);
                attackObj.transform.position = transform.position;
                basicAttackTimer = 0;
            }
            //else if (pattenNum == 2 && upGroundPattening == false)
            //{
            //    upGroundPattening = true;
            //    StartCoroutine(upGroundPatten());
            //}
            //else if(pattenNum==2&& upGroundPattening == true)
            //{
            //    basicAttackTimer = 2.4f;
            //}
            else if (pattenNum == 2)
            {
                Debug.Log("patten2");
                GameObject attackObj;
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGround, transform);
                attackObj.transform.position = transform.position;
                basicAttackTimer = 0;
            }
        }
    }

    IEnumerator upGroundPatten()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject attackObj;
            attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, transform);
            attackObj.transform.position = playerTrs.position;

            if (i == 3)
            {
                basicAttackTimer = 0;
            }

            if (i == 4)
            {
                upGroundPattening = false;
            }
            
            yield return new WaitForSeconds(2f);
            
        }
    }


    private void HaxagonPatten()
    {
        if (halfPattenCheck == false)
        {   
            StartCoroutine(HaxagonLaser());
            halfPattenCheck = true;
        }
    }

    IEnumerator HaxagonLaser()
    {
        for (int i = 0; i < 8; i++)
        {
            PoolingManager.Instance.CreateObject("HaxagonLaser", transform);
            yield return new WaitForSeconds(4);

        }

    }
}
