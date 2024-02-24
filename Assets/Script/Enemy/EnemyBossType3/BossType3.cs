using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType3 : MonoBehaviour
{
    private Animator anim;
    private Transform playerTrs;


    void Start()
    {
        playerTrs = GameManager.instance.GetPlayerTransform;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
      
    }

    private void attackPatten()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundLaserObj, GameManager.instance.GetEnemyAttackObjectPatten);
            obj.transform.position = playerTrs.position;
            UpGround upGround = obj.GetComponent<UpGround>();
            upGround.SetStopTime(true, 2);
        }
    }

    private void pattenAnimator()
    {

    }


    
}
