using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{
    private Transform Target;
    private NavMeshAgent nav;
    private Rigidbody rigd;
    private Animator animator;

    
    void Start()
    {
        nav = GetComponentInParent<NavMeshAgent>();
        rigd = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Target = GameManager.instance.GetPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
    }
    private void enemyMove()
    {
        if (transform.position.x + 0.2f <= Target.position.x)
        {
            transform.localScale = new Vector3(-2, 1, 1);
        }
        else if (transform.position.x - 0.2 >= Target.position.x)
        {
            transform.localScale = new Vector3(2, 1, 1);
        }
        nav.SetDestination(Target.position);
    }
}
