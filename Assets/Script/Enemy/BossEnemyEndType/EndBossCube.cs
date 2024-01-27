using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossCube : Unit
{
    //테스트
    [SerializeField] private bool halfPattenCheck;
    //기본스킬들 재사용 시간
    [SerializeField] private float basicAttackTime = 2.5f;
    private float basicAttackTimer = 3;
    private int pattenNum = 0;
    private bool colorChange = false;

    [SerializeField] private float UpGroundRange = 2;//반지름

    [SerializeField]private SpriteRenderer spr;
    private bool upGroundPattening = false;

    //외부참조
    private Transform playerTrs;
    

    void Start()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
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
        if (basicAttackTimer >= basicAttackTime - 0.5f && colorChange == false)
        {
            pattenNum = Random.Range(0, 3);

            if (pattenNum == 0)
            {
                spr.color = Color.black;
                colorChange = true;
            }
            else if (pattenNum == 1)
            {
                spr.color = Color.red;
                colorChange = true;
            }
            else if (pattenNum == 2)
            {
                spr.color = Color.cyan;
                colorChange = true;
            }

        }
        if (basicAttackTimer >= basicAttackTime)
        {
            
            if (pattenNum == 0)
            {
                GameObject attackObj;
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.LaserPatten, GameManager.instance.GetEnemyAttackObjectPatten);
                attackObj.transform.position = transform.position;
                basicAttackTimer = 0;
                colorChange = false;
            }
            else if (pattenNum == 1)
            {
                GameObject attackObj;
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.RedButterfly, GameManager.instance.GetEnemyAttackObjectPatten);
                attackObj.transform.position = transform.position;
                basicAttackTimer = 0;
                colorChange = false;
            }
            else if (pattenNum == 2)
            {
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
                colorChange = false; 
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
