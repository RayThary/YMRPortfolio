using System.Collections;
using System.Collections.Generic;
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
    private bool attackPlayerCheck;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

    }


    new void Start()
    {
        base.Start();
        nav = GetComponentInParent<NavMeshAgent>();
        rigd = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
        Target = GameManager.instance.GetPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
        enemyAttack();
        enemyAttackDelay();
        enemyAttackPlayerCheck();
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

        if (attackCheck)
        {
            //Target.GetComponent<Unit>().Hit(this, 3);//대미지입히는부분
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
    private void enemyAttackPlayerCheck()
    {
        if (attackPlayerCheck)
        {
            Vector3 center = new Vector3(transform.position.x - 0.6f, transform.position.y + 0.23f, transform.position.z);
            Vector3 enemyAttackBox = new Vector3(0.65f, 1.3f, 0);
            Collider[] col = Physics.OverlapBox(center, enemyAttackBox / 2, Quaternion.identity, LayerMask.GetMask("Player"));
            for (int i = 0; i < col.Length; i++)
            {
                Transform tf_Target = col[i].transform;
                if (tf_Target.tag == "Player")
                {
                    Target.GetComponent<Unit>().Hit(this, 3);
                    attackPlayerCheck = false;
                }
            }


        }
    }

    //애니메이션용
    private bool EnemyAttackDamageTrue()
    {
        return attackPlayerCheck = true;
    }

    private bool EnemyAttackDamageFalse()
    {
        return attackPlayerCheck = false;
    }
}
