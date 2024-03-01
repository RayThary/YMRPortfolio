using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType3 : MonoBehaviour
{
    [SerializeField] private float AttackSpeed = 1;

    //기본공격 딜레이
    [SerializeField] private float basicAttackTime = 5f;
    private float basicAttackTimer = 0;

    //패턴의 딜레이
    [SerializeField] private float attackPattenTime = 5;
    private float attackPattenTimer = 0;
    private float beforeAttackPattenTime;
    private bool attackPattenStart = false;

    private bool patten0Anim = false;
    private bool patten1Anim = false;
    private bool patten2Anim = false;
    private bool patten3Anim = false;

    private int pattenNum = 0;

    private Animator anim;
    private Transform playerTrs;
    [SerializeField] private GroundPatten groundpatten;

    void Start()
    {
        beforeAttackPattenTime = attackPattenTime;
        playerTrs = GameManager.instance.GetPlayerTransform;
        anim = GetComponent<Animator>();
        groundpatten = GetComponent<GroundPatten>();
    }

    void Update()
    {
        basicAttackPatten();
        attackPatten();
        pattenAnimator();


    }


    private void basicAttackPatten()
    {
        basicAttackTimer += Time.deltaTime;
        if (basicAttackTimer >= basicAttackTime)
        {
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundLaserObj, GameManager.instance.GetEnemyAttackObjectPatten);
            obj.transform.position = playerTrs.position;
            UpGround upGround = obj.GetComponent<UpGround>();
            upGround.SetStopTime(true, 2);
            basicAttackTimer = 0;
        }
    }

    private void attackPatten()
    {
        if (attackPattenStart == false)
        {
            attackPattenTimer += Time.deltaTime;
            if (attackPattenTimer >= attackPattenTime)
            {
                attackPattenStart = true;

                //중복제거방법 질문
                if (pattenNum == 0)
                {
                    pattenNum = Random.Range(0, 3);
                    if (pattenNum == 0)
                    {
                        pattenNum = 1;
                    }
                    else if(pattenNum == 1)
                    {
                        pattenNum = 2;
                    }
                    else
                    {
                        pattenNum = 3;
                    }
                }
                else if (pattenNum == 1)
                {
                    pattenNum = Random.Range(0, 3);
                    if (pattenNum == 0)
                    {
                        pattenNum = 0;
                    }
                    else if (pattenNum == 1)
                    {
                        pattenNum = 2;
                    }
                    else
                    {
                        pattenNum = 3;
                    }
                }
                else if(pattenNum == 2)
                {
                    pattenNum = Random.Range(0, 3);
                    if (pattenNum == 0)
                    {
                        pattenNum = 0;
                    }
                    else if (pattenNum == 1)
                    {
                        pattenNum = 1;
                    }
                    else
                    {
                        pattenNum = 3;
                    }
                }
                else if (pattenNum == 3)
                {
                    pattenNum = Random.Range(0, 3);
                    pattenNum = Random.Range(0, 3);
                    if (pattenNum == 0)
                    {
                        pattenNum = 0;
                    }
                    else if (pattenNum == 1)
                    {
                        pattenNum = 1;
                    }
                    else
                    {
                        pattenNum = 2;
                    }

                }

                int groundPattenBool = Random.Range(0, 2);


                if (pattenNum == 0)
                {
                    if (groundPattenBool == 0)
                    {
                        groundpatten.GroundPattenStart(GroundPatten.PattenName.HorizontalAndVerticalPatten, true);
                    }
                    else
                    {
                        groundpatten.GroundPattenStart(GroundPatten.PattenName.HorizontalAndVerticalPatten, false);
                    }
                    patten0Anim = true;
                    attackPattenTime = beforeAttackPattenTime;
                }
                else if (pattenNum == 1)
                {
                    if (groundPattenBool == 0)
                    {
                        groundpatten.GroundPattenStart(GroundPatten.PattenName.WavePattenHrizontal, true);
                    }
                    else
                    {
                        groundpatten.GroundPattenStart(GroundPatten.PattenName.WavePattenHrizontal, false);
                    }
                    attackPattenTime = attackPattenTime * 2;
                    patten1Anim = true;
                }
                else if (pattenNum == 2)
                {
                    if (groundPattenBool == 0)
                    {
                        groundpatten.GroundPattenStart(GroundPatten.PattenName.WavePattenVitical, true);
                    }
                    else
                    {
                        groundpatten.GroundPattenStart(GroundPatten.PattenName.WavePattenVitical, false);
                    }
                    attackPattenTime = attackPattenTime * 2;
                    patten1Anim = true;

                }
                else if (pattenNum == 3)
                {
                    groundpatten.GroundPattenStart(GroundPatten.PattenName.OpenWallGroundPatten, true);
                    patten2Anim = true;
                    attackPattenTime = beforeAttackPattenTime;
                }
                attackPattenTimer = 0;
            }
        }
    }

    private void pattenAnimator()
    {
        if (patten0Anim)
        {
            anim.SetTrigger("Attack");
            anim.SetFloat("AttackState", 0);
            anim.SetFloat("NormalState", 1);
            patten0Anim = false;
        }
        else if (patten1Anim)
        {
            anim.SetTrigger("Attack");
            anim.SetFloat("AttackState", 0);
            anim.SetFloat("NormalState", 0);
            AttackSpeed = 0.5f;
            patten1Anim = false;
        }
        else if (patten2Anim)
        {
            anim.SetTrigger("Attack");
            anim.SetFloat("AttackState", 0);
            anim.SetFloat("NormalState", 0);
            AttackSpeed = 0.5f;
            patten2Anim = false;
        }
        else if (patten3Anim)
        {
            anim.SetTrigger("Attack");
            anim.SetFloat("AttackState", 1);
            anim.SetFloat("SkillState", 0);
            patten3Anim = false;
        }
    }

    //애니메이션용
    private void EnventEnd()
    {
        attackPattenStart = false;
    }

    private void PattenWaveEnvent()
    {
        anim.SetFloat("AttackSpeed", 0.2f);
    }
    private void PattenWaveEnventReturen()
    {
        anim.SetFloat("AttackSpeed", AttackSpeed);
    }

}
