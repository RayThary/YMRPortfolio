using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : Unit
{
    [SerializeField] private int patternType; // 임시패턴설정용
    private BombingPatten bombing;

    private bool bombingStart = false;

    private int bombingType;
    private int beforeBombingType = -1;

    [SerializeField] private float nextBombingTime = 0.2f;
    private List<GameObject> pullObject = new List<GameObject>();

    private NavMeshAgent nav;
    private Rigidbody rigd;
    private Animator animator;

    private Transform playerTrs;//플레이어위치

    protected new void Start()
    {
        base.Start();
        nav = GetComponentInParent<NavMeshAgent>();
        rigd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        bombing = GetComponent<BombingPatten>();
        playerTrs = GameManager.instance.GetPlayerTransform;

    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
        enemyAttackPattern();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            patternType = 1;
        }

    }

    private void enemyMove()
    {
        if (transform.position.x + 0.2f <= playerTrs.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (transform.position.x - 0.2 >= playerTrs.position.x)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        nav.SetDestination(playerTrs.position);
    }

    private void enemyAttackPattern()
    {
        if (patternType == 1)
        {
            if(playerTrs.position.x>=7&&playerTrs.position.x<=22&& playerTrs.position.z >= 7 && playerTrs.position.z <= 22)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector3 playerVec = playerTrs.position;
                    Vector3 targetVec;
                    if (i == 0)
                    {
                        targetVec = new Vector3(playerVec.x + 4, playerVec.y, playerVec.z);

                    }
                    else if (i == 1)
                    {
                        targetVec = new Vector3(playerVec.x - 4, playerVec.y, playerVec.z);

                    }
                    else if (i == 2)
                    {
                        targetVec = new Vector3(playerVec.x, playerVec.y, playerVec.z + 4);

                    }
                    else
                    {
                        targetVec = new Vector3(playerVec.x, playerVec.y, playerVec.z - 4);

                    }

                    pullObject.Add(PoolingManager.Instance.CreateObject("PullObject", GameManager.instance.GetEnemyAttackObjectPatten));
                    pullObject[i].transform.position = targetVec;

                }
                patternType = 0;

            }
            else
            {
                Debug.Log("1");
                patternType = 0;
            }

            

        }


        if (patternType == 2)
        {
            StartCoroutine(bombingPatten());
            patternType = 0;
        }


    }

    IEnumerator bombingPatten()
    {

        for (int i = 0; i < 4; i++)
        {
            bombingType = Random.Range(0, 4);
            while (bombingType == beforeBombingType)
            {
                bombingType = Random.Range(0, 4);
            }
            beforeBombingType = bombingType;

            if (beforeBombingType == 0)
            {
                bombing.BombingStart(BombingPatten.BombingType.Horizontal);
            }
            else if (beforeBombingType == 1)
            {
                bombing.BombingStart(BombingPatten.BombingType.Vertical);
            }
            else if (beforeBombingType == 2)
            {
                bombing.BombingStart(BombingPatten.BombingType.RightDiagonal);
            }
            else if (beforeBombingType == 3)
            {
                bombing.BombingStart(BombingPatten.BombingType.LeftDiagonal);
            }

            yield return new WaitForSeconds(nextBombingTime);
        }
    }



}
