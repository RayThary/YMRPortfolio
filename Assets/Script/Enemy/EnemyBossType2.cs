using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossType2 : MonoBehaviour
{
    private Transform target;
    //private

    /// <summary>
    /// ��� ������ ���ӿ�����Ʈ�� ���߿� Ǯ�����־��־ ����� �����ִ°� �ٲ������ 
    /// </summary>
    //����1
    public GameObject objPatten1;//�ӽ� ���׿�������Ʈ

    [SerializeField] private Transform trsArea;//����������ġ�����������ʿ䰡����
    [SerializeField] private int MeteorSapwnCount = 4;//���׿� ��ȯ����

    private Vector3 meteorBoxSize;
    private Vector3 spawnPos;

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

    void Start()
    {
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
            //2�ʰ������� ���׿�ī��Ʈ��ŭ ��3�������ļ� ��ȯ�Ѵ�
            for (int i = 0; i < MeteorSapwnCount; i++)
            {

                //spawnPos = GetRandomPosition();

                //Instantiate(G, spawnPos, Quaternion.identity, trsArea);


                StartCoroutine("patten1");
                if (i == MeteorSapwnCount - 1)
                {
                    patten1Check = false;
                }
            }

        }

    }

    IEnumerator patten1()
    {
        for (int i = 0; i < 3; i++)
        {

            spawnPos = GetRandomPosition();

            Instantiate(objPatten1, spawnPos, Quaternion.identity, trsArea);

            yield return new WaitForSeconds(3f);

        }

        yield return new WaitForSeconds(2);
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

            Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z+1 );
            patten2ObjCheck = Instantiate(objPatten2, spawnPos, Quaternion.identity, transform.parent);

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
            patten3ObjCheck =Instantiate(objPatten3, transform.position, Quaternion.identity, transform.parent);
            patten3ObjCheck.transform.rotation = Quaternion.LookRotation(target.position - patten3ObjCheck.transform.position);
            patten3Check = false;
        }
    }

}
