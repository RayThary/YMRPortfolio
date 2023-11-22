using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType2 : MonoBehaviour
{
    [SerializeField] private Vector3 meteorBoxSize;
    [SerializeField] private Transform trsArea;
    [SerializeField] private int MeteorSapwnCount = 3;
    private int meteorCount = 0;
    private Vector3 spawnPos;
    public GameObject G;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, meteorBoxSize);

    }
    public bool spawn = false;
    void Start()
    {

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
}
