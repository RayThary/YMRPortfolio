using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class EnemySword : Unit
{
    private Transform Target;
    private NavMeshAgent nav;
    private Rigidbody rigd;
    private Animator animator;
    private Vector3 size = new Vector3(2, 0, 2.2f);
    private Vector3 upsize = new Vector3(0, 0, 0.3f);

    [SerializeField] private bool attackCheck = false;
    private bool attackDelayCheck = false;
    [SerializeField] private float AttackDelayTime = 2f;
    private float attackDelayTimer = 0.0f;
    private bool right = false;


    new void Start()
    {
        base.Start();
        nav = GetComponent<NavMeshAgent>();
        rigd = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Target = GameManager.instance.GetPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
       
        enemyMove();
        enemyAttack();
        enemyAttackDelay();
    }

    private void FixedUpdate()
    {
        rigd.velocity = Vector3.zero;
        rigd.angularVelocity = Vector3.zero;
    }

    private void enemyMove()
    {
        if (transform.position.x + 0.2f <= Target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            right = false;

        }
        else if (transform.position.x - 0.2 >= Target.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            right = true;
        }
        nav.SetDestination(Target.position);
    }
    private void enemyAttack()
    {
        Collider[] col = Physics.OverlapBox(transform.position + upsize, size / 2, Quaternion.identity, LayerMask.GetMask("Player"));

        if (col.Length > 0)
        {
            for (int i = 0; i < col.Length; i++)
            {
                Transform tf_Target = col[i].transform;
                if (tf_Target.tag == "Player")
                {
                    if (attackDelayCheck == false)
                    {
                        attackCheck = true;
                    }
                }
            }
        }
       

        if (attackCheck)
        {
            attackDelayCheck = true;
            animator.SetFloat("AttackState", 0);
            animator.SetFloat("NormalState", 0);
            animator.SetTrigger("Attack");
            attackCheck = false;
        }
    }

    private void enemyAttackDelay()
    {
        if (attackDelayCheck)
        {
            attackDelayTimer += Time.deltaTime;
            if (attackDelayTimer >= AttackDelayTime)
            {
                attackDelayTimer = 0;
                attackDelayCheck = false;
            }
        }
    }
}
