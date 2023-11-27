using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : Unit
{
    private Transform Target;
    private NavMeshAgent nav;
    private Rigidbody rigd;
    private Animator animator;

    [SerializeField] private int patternType; // 임시패턴설정용

    private bool continuousAttack;

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
        enemyAttackPattern();

    }

    private void enemyMove()
    {
        if (transform.position.x + 0.2f <= Target.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (transform.position.x - 0.2 >= Target.position.x)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        nav.SetDestination(Target.position);
    }

    private void enemyAttackPattern()
    {
        if (patternType == 1)
        {
            GameObject bullet = null;
            Transform parent = transform.parent;

            for (int i = 0; i < 4; i++)
            {

                bullet = PollingManager.Instance.CreateObject("EnemyBullet", parent);
                bullet.transform.position = parent.position;
                bullet.transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
                Vector3 rot = bullet.transform.eulerAngles;
                if (i != 0)
                {
                    if (i == 1)
                    {
                        rot.y += 90;
                    }
                    else if (i == 2)
                    {
                        rot.y += 180;
                    }
                    else if (i == 3)
                    {
                        rot.y += 270;
                    }

                    bullet.transform.rotation = Quaternion.Euler(rot);
                }




            }

            patternType = 0;
        }


        if (patternType == 2)
        {
            GameObject bullet = null;
            if (continuousAttack)
            {
                Transform parent = transform.parent;

                bullet = PollingManager.Instance.CreateObject("EnemyBullet", parent);
                bullet.transform.position = parent.position;
                bullet.transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
            }

            for (int i = 0; i < 6; i++)
            {

            }
        }
    }

}
