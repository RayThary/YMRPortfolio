using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType3 : MonoBehaviour
{
    private Animator anim;
    private Transform playerTrs;
    [SerializeField]private GroundPatten groundpatten;

    void Start()
    {
        playerTrs = GameManager.instance.GetPlayerTransform;
        anim = GetComponent<Animator>();
        groundpatten = GetComponent<GroundPatten>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            int a = Random.Range(0, 3);

            if (a == 0)
            {
                groundpatten.GroundPattenStart(GroundPatten.PattenName.HorizontalAndVerticalPatten, true);
            }
            else if (a == 1)
            {
                groundpatten.GroundPattenStart(GroundPatten.PattenName.WavePatten, true);
            }
            else if (a == 2)
            {
                groundpatten.GroundPattenStart(GroundPatten.PattenName.OpenWallGroundPatten, true);
            }
            Debug.Log(a);
        }
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
