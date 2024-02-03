using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBossType2 : Unit
{

    private Transform target;
    private Animator anim;
    [SerializeField] private float attsp = 0.5f;

    //���׿�����(����1)
    private GameObject patten1ObjCheck = null;

    [SerializeField] private Transform trsArea;//����������ġ�����������ʿ䰡����
    [SerializeField] private int MeteorSapwnCount = 3;//���׿� ��ȯ����

    private Vector3 meteorBoxSize;
    private Vector3 spawnPos;

    private float timer = 0.0f;
    private bool allSpawnCheck = false;
    //����2(�����������ۺ��۵��� ������Ʈ) 
    private GameObject patten2ObjCheck = null;

    [SerializeField] private float patten2DurationTime = 4;//����2���ӽð�
    private float patten2Timer = 0.0f;//����2 Ÿ�̸�
    private bool patten2MotionCheck = false;

    private bool patten2ObjSpawnCheck = false;


    //����3(���Ǻ��� ǳ��������� ���� ������)
    public GameObject objPatten3;//�ӽ� ���ǵǸ龲������ 

    private GameObject patten3ObjCheck = null;

    //����4 (ū�Ѿ˼�ȯ 4�������� �Ѿ˳���)
    private bool basicAttackCheck = true;
    private int attackType;
    private bool bigBulletCheck = false;

    [SerializeField] private float basicAttackTime = 6;
    private float basicAttackTimer = 0.0f;

    private bool noMeleeCheck = false;

    //�̵���
    private NavMeshAgent nav;
    private bool noMove = false;


    private Transform playerTrs;

    //�׽�Ʈ��
    private bool patten1Check = false;//���׿� ����1�Ẹ�°�
    private bool patten2Check = false;//�������� ���̿��� ������ü3����ȯ�ϴ°�
    private bool patten3Check = true;//�������� x�ڷ� ���������������Եȴ�
    private bool patten4Check = false;//ū�Ѿ�����
    protected new void Start()
    {
        base.Start();
        nav = transform.parent.GetComponent<NavMeshAgent>();
        playerTrs = GameManager.instance.GetPlayerTransform;
        target = GameManager.instance.GetPlayerTransform;
        anim = GetComponent<Animator>();
        meteorBoxSize = GameManager.instance.enemyManager.GetStage.BoxCollider.size;
    }


    void Update()
    {
        enemyMove();
        enemyAttack();//�⺻����
        bigBullet();
        enemyMeleeAttack();
        enemyHalfHealthPatten();
    }

    private void enemyMove()
    {

        if (noMove)
        {
            anim.SetFloat("RunState", 0);
            nav.enabled = false;

        }
        else
        {

            float playerDistance = Vector3.Distance(transform.position, target.position);
            if (playerDistance >= 4)
            {
                anim.SetFloat("RunState", 0.5f);
                nav.enabled = true;
                nav.SetDestination(target.position);
            }
            else
            {
                nav.enabled = false;
                anim.SetFloat("RunState", 0);
            }
        }
    }


    private void enemyAttack()
    {

        if (basicAttackCheck)
        {

            attackType = Random.Range(0, 2);
            if (attackType == 0)
            {
                patten1Check = true;
                meteorAttack();
                basicAttackCheck = false;
                basicAttackTime = 6;
            }
            else if (attackType == 1)
            {
                bigBulletCheck = true;
                bigBulletAttack();
                basicAttackCheck = false;
                basicAttackTime = 5;
            }
        }
        else
        {
            basicAttackTimer += Time.deltaTime;
            if (basicAttackTimer >= basicAttackTime)
            {
                basicAttackTimer = 0;
                basicAttackCheck = true;
            }
        }

    }

    private void meteorAttack()
    {

        if (patten1Check)
        {
            noMeleeCheck = true;
            attsp = 0.5f;
            anim.SetTrigger("Attack");
            anim.SetFloat("AttackState", 0);
            anim.SetFloat("NormalState", 1);
            anim.SetFloat("AttackSpeed", attsp);


            allSpawnCheck = false;
            patten1Check = false;

            StartCoroutine(patten1Spawn());
        }

    }

    IEnumerator patten1Spawn()
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(patten1());
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator patten1()
    {
        for (int i = 0; i < 9; i++)
        {
            spawnPos = GetRandomPosition();
            patten1ObjCheck = PoolingManager.Instance.CreateObject("Meteor", transform.parent.parent);
            patten1ObjCheck.transform.position = spawnPos;

            if (i % 2 == 0)
            {
                yield return new WaitForSeconds(0.5f);
            }

            if (i == 8)
            {
                allSpawnCheck = true;
            }
        }
    }


    private Vector3 GetRandomPosition()
    {
        Vector3 basicPos = GameManager.instance.enemyManager.GetStage.Map.transform.position;
        Vector3 size = meteorBoxSize;

        int count = 0;
        float posX = basicPos.x + Random.Range(-size.x / 2, size.x / 2);
        float posZ = basicPos.x + Random.Range(-size.z / 2, size.z / 2);
        Vector3 spawnVec = new Vector3(posX, 0, posZ);
        float meteorDis = Vector3.Distance(target.position, spawnVec);

        while (meteorDis > 5)
        {
            posX = basicPos.x + Random.Range(-size.x / 2, size.x / 2);
            posZ = basicPos.x + Random.Range(-size.z / 2, size.z / 2);
            spawnVec = new Vector3(posX, 0, posZ);
            meteorDis = Vector3.Distance(target.position, spawnVec);
            count++;
            if (count > 10)
            {
                break;
            }
        }

        return spawnVec;
    }

    private void bigBullet()
    {
        if (patten4Check)
        {
            StartCoroutine(bigBulletSpawn());
            patten4Check = false;
        }
    }

    private void bigBulletAttack()
    {

        if (bigBulletCheck)
        {
            noMeleeCheck = true;
            attsp = 0.3f;
            anim.SetTrigger("Attack");
            anim.SetFloat("AttackState", 1);
            anim.SetFloat("SkillState", 1);
            anim.SetFloat("AttackSpeed", attsp);
            bigBulletCheck = false;
        }

    }

    IEnumerator bigBulletSpawn()
    {
        GameObject bigBulletObj;
        bigBulletObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BigBullet, GameManager.instance.GetEnemyAttackObjectPatten);
        bigBulletObj.GetComponent<BigBulletPatten>().Boss = this;

        bigBulletObj.transform.position = transform.position;
        Vector3 player = playerTrs.transform.position - transform.position;

        bigBulletObj.transform.rotation = Quaternion.LookRotation(player);
        yield return new WaitForSeconds(10);
        if (bigBulletObj != null)
        {
            PoolingManager.Instance.RemovePoolingObject(bigBulletObj);
        }
    }
    private void enemyMeleeAttack()
    {


        if (patten2ObjCheck != null)
        {
            patten2Timer += Time.deltaTime;
            if (patten2Timer >= patten2DurationTime)
            {
                PoolingManager.Instance.RemovePoolingObject(patten2ObjCheck);
                patten2Timer = 0;
                patten2MotionCheck = false;
            }
        }
        else
        {
            float dis = Vector3.Distance(transform.position, target.position);
            if (dis < 3)
            {
                patten2Check = true;
            }

            if (patten2Check)
            {
                if (patten2MotionCheck == false)
                {
                    attsp = 0.5f;

                    anim.SetTrigger("Attack");
                    anim.SetFloat("AttackState", 0);
                    anim.SetFloat("NormalState", 0.5f);
                    anim.SetFloat("AttackSpeed", attsp);
                    patten2MotionCheck = true;
                    patten2Check = false;
                }
            }


            if (patten2ObjSpawnCheck)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, 0.1f, transform.position.z + 1);
                patten2ObjCheck = PoolingManager.Instance.CreateObject("RotatingSphere", transform.parent);
                patten2ObjCheck.transform.position = spawnPos;
                patten2ObjCheck.GetComponentInChildren<Type2Patten2RedStart>().setType2(this);
                patten2ObjSpawnCheck = false;
            }
        }
    }

    private void enemyHalfHealthPatten()
    {

        if (patten3Check)
        {
            return;
        }
        if (stat.HP <= stat.MAXHP / 2)
        {
            patten3ObjCheck = PoolingManager.Instance.CreateObject("WindMillPatten", transform.parent.parent);
            patten3Check = true;
        }

    }

    //�ִϸ��̼ǿ�

    private void enemyAttackMotionStart()
    {
        noMove = true;
    }
    private void enemyAttackMotionEnd()
    {
        noMove = false;
        noMeleeCheck = false;
    }

    private void bigBulletMotion()
    {
        patten4Check = true;
    }

    private void meleeAttack()
    {
        patten2ObjSpawnCheck = true;

    }
}
