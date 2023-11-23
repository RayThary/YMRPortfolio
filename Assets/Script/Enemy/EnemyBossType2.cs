using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossType2 : MonoBehaviour
{
    [SerializeField] private Vector3 meteorBoxSize;//���׿� ��������ũ�� ����������ġ���� ��ӹ޾ƿ���ɵ�
    [SerializeField] private Transform trsArea;//����������ġ�����������ʿ䰡����
    [SerializeField] private int MeteorSapwnCount = 3;//���׿� ��ȯ����
    private int meteorCount = 0;
    private Vector3 spawnPos;
    private Transform target;

    public GameObject G;//�ӽ� ���׿�������Ʈ

    private NavMeshAgent nav;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, meteorBoxSize);

    }
    public bool spawn = false;

    void Start()
    {
        nav = transform.parent.GetComponent<NavMeshAgent>();
        target = GameManager.instance.GetPlayerTransform;
    }


    void Update()
    {
        enemyMove();
        enemyAttack();
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
            //�ڷΰ���? or ���� 
        }
    }

    private void enemyAttack()
    {
        if (spawn)
        {
            GameObject meteor = null;
            if (meteorCount < MeteorSapwnCount)
            {
                spawnPos = GetRandomPosition();
                float meteorDis = Vector3.Distance(target.position, spawnPos);
                if (meteorDis > 5)
                {

                }
                Instantiate(G, spawnPos, Quaternion.identity, trsArea);
                meteorCount++;
            }
            else if (meteorCount == MeteorSapwnCount)
            {
                spawn = false;
                meteorCount = 0;
            }


        }

    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basicPos = transform.position;
        Vector3 size = meteorBoxSize;

        float posX = basicPos.x + Random.Range(-size.x / 2, size.x / 2);
        float posZ = basicPos.x + Random.Range(-size.z / 2, size.z / 2);
        Vector3 spawnVec = new Vector3(posX, 0, posZ);
        float meteorDis = Vector3.Distance(target.position, spawnPos);

        while (meteorDis > 5)
        {
            posX = basicPos.x + Random.Range(-size.x / 2, size.x / 2);
            posZ = basicPos.x + Random.Range(-size.z / 2, size.z / 2);
            spawnVec = new Vector3(posX, 0, posZ);
            meteorDis = Vector3.Distance(target.position, spawnPos);
            
        }

        return spawnVec;
    }

    IEnumerator s()
    {
        for (int i = 0; i < meteorCount; i++)
        {

            yield return new WaitForSeconds(1);
        }
    }
}
