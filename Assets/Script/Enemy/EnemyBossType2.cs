using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossType2 : MonoBehaviour
{
    [SerializeField] private Vector3 meteorBoxSize;
    [SerializeField] private Transform trsArea;
    [SerializeField] private int MeteorSapwnCount = 3;
    private int meteorCount = 0;
    private Vector3 spawnPos;
    private Transform target;
    public GameObject G;

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

    }


    void Update()
    {
        enemyAttack();
    }

    private void enemyAttack()
    {
        if (spawn)
        {
            GameObject meteor = null;
            if (meteorCount < MeteorSapwnCount)
            {
                spawnPos = GetRandomPosition();
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

        return new Vector3(posX, 0.1f, posZ);
    }

    IEnumerator s()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            
            yield return new WaitForSeconds(1);
        }
    }
}
