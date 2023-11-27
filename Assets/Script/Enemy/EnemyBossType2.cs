using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossType2 : Unit
{
    private Transform target;
    //private

    /// <summary>
    /// ��� ������ ���ӿ�����Ʈ�� ���߿� Ǯ�����־��־ ����� �����ִ°� �ٲ������ 
    /// </summary>
    //����1
    public GameObject objPatten1;//�ӽ� ���׿�������Ʈ
    private GameObject patten1ObjCheck = null;

    [SerializeField] private Transform trsArea;//����������ġ�����������ʿ䰡����
    [SerializeField] private int MeteorSapwnCount = 3;//���׿� ��ȯ����

    private Vector3 meteorBoxSize;
    private Vector3 spawnPos;

    private float timer = 0.0f;
    private bool allSpawnCheck = false;
    //����2 
    public GameObject objPatten2;//�ӽ� �÷��̾������ ��ü4����ȯ
    private GameObject patten2ObjCheck = null;

    [SerializeField] private float patten2DurationTime = 4;//����2���ӽð�
    private float patten2Timer = 0.0f;//����2 Ÿ�̸�

    //����3
    public GameObject objPatten3;//�ӽ� ���ǵǸ龲������ 

    private GameObject patten3ObjCheck = null;


    //�̵���
    private NavMeshAgent nav;
    //�׽�Ʈ��
    public bool patten1Check = false;//���׿� ����1�Ẹ�°�
    public bool patten2Check = false;//�������� ���̿��� ������ü3����ȯ�ϴ°�
    public bool patten3Check = false;

    protected new void Start()
    {
        base.Start();
        nav = transform.parent.GetComponent<NavMeshAgent>();
        target = GameManager.instance.GetPlayerTransform;
        meteorBoxSize = trsArea.GetComponent<BoxCollider>().bounds.size;
    }


    void Update()
    {
        enemyMove();
        enemyAttack();
        enemyMeleeAttack();
        enemyHalfHealthPatten();
    }

    private void enemyMove()
    {
        float playerDistance = Vector3.Distance(transform.position, target.position);
        if (playerDistance >= 4)
        {
            nav.enabled = true;
            nav.SetDestination(target.position);
        }
        else
        {
            nav.enabled = false;
            if (patten2ObjCheck != null)
            {
                //  ���� 2���� ä���������� ��ȯ���̴Ͽ��������ȵ�
            }
        }
    }



    private void enemyAttack()
    {
        if (patten1Check)
        {
            allSpawnCheck = false;
            //2�ʰ������� ���׿�ī��Ʈ��ŭ ��3�������ļ� ��ȯ�Ѵ�
            for (int i = 0; i < MeteorSapwnCount; i++)
            {

                StartCoroutine("patten1");
                if (i == MeteorSapwnCount - 1)
                {
                    patten1Check = false;
                }
            }

        }


        if (patten1Check == false && allSpawnCheck == true) 
        {
            timer += Time.deltaTime / 3;
            if (timer > 1.0f)
            {
                timer = 0.0f;
                patten1Check = true;
            }
        }
    }

    IEnumerator patten1()
    {
        for (int i = 0; i < 3; i++)
        {
            spawnPos = GetRandomPosition();
            patten1ObjCheck = PollingManager.Instance.CreateObject("Meteor", transform.parent);
            patten1ObjCheck.transform.position = spawnPos;
            yield return new WaitForSeconds(1f);
            if (i == 2)
            {
                allSpawnCheck = true;
            }
        }
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 basicPos = transform.position;
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


    private void enemyMeleeAttack()
    {
        if (patten2Check)
        {

            Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z + 1);
            patten2ObjCheck = PollingManager.Instance.CreateObject("RotatingSphere", transform.parent);
            patten2ObjCheck.transform.position = spawnPos;
            //patten2ObjCheck = Instantiate(objPatten2, spawnPos, Quaternion.identity, transform.parent);

            patten2Check = false;
        }
        if (patten2ObjCheck != null)
        {
            patten2Timer += Time.deltaTime;
            if (patten2Timer >= patten2DurationTime)
            {
                Destroy(patten2ObjCheck);
                patten2Timer = 0;
            }
        }
    }

    private void enemyHalfHealthPatten()
    {
        if (patten3Check)
        {
            patten3ObjCheck = PollingManager.Instance.CreateObject(PollingManager.ePoolingObject.BigBullet, transform.parent);
            patten3ObjCheck.transform.position = transform.position;
            //patten3ObjCheck =Instantiate(objPatten3, transform.position, Quaternion.identity, transform.parent);
            patten3ObjCheck.transform.rotation = Quaternion.LookRotation(target.position - patten3ObjCheck.transform.position);
            patten3Check = false;
        }
    }

}
