using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType2 : MonoBehaviour
{
    [SerializeField] private Vector3 meteorBoxSize;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, meteorBoxSize);
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void enemyAttack()
    {
        //RaycastHit box = Physics.BoxCast(transform.position, meteorBoxSize / 2);
    }
}
