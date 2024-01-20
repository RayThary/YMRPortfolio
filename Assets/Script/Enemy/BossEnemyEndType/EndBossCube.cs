using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossCube : Unit
{
    //�׽�Ʈ
    [SerializeField] private bool halfPattenCheck;
    //�⺻��ų�� ���� �ð�
    [SerializeField]private float basicAttackTimer = 3;
    [SerializeField] private float UpGroundRange = 2;//������

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
                GameObject attackObj;
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.LaserPatten, GameManager.instance.GetEnemyAttackObjectPatten);
                attackObj.transform.position = transform.position;
                basicAttackTimer = 0;
            }
            else if (pattenNum == 1)
            {
                GameObject attackObj;
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.RedButterfly, GameManager.instance.GetEnemyAttackObjectPatten);
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

                Debug.Log("2");
                for (int i = 0; i < 8; i++)
                {
                    GameObject attackObj;
                    float rangeX = Random.Range(-UpGroundRange, UpGroundRange);
                    float rnageZ = Random.Range(-UpGroundRange, UpGroundRange);

                    Vector3 randomSpawnVec = playerTrs.position;
                    randomSpawnVec.x += rangeX;
                    randomSpawnVec.z += rnageZ;
                    attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
                    attackObj.transform.position = randomSpawnVec;
                }
                
                basicAttackTimer = 0;
            }
        }
    }

    IEnumerator upGroundPatten()
    {
        for(int i = 0; i < 8; i++)
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
