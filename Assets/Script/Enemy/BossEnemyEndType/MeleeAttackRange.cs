using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackRange : MonoBehaviour
{
    //endBoss스크립트
    private BossEnemyEndType endType;
    private SphereCollider sphere;

    private bool vicinityAttackCheck = false;//애니메이션의 공격중인시간
    private bool attackStartCheck = false;//애니메이션의 시작시간

    [SerializeField]private bool attackAnimCheck = false;
    //반격없게할듯?
    private Player player;
    private Unit boss;
    public Unit Boss { set { boss = value; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (vicinityAttackCheck)
            {
                player.Hit(boss, 1);
            }
        }
    }


    void Start()
    {
        player = GameManager.instance.GetPlayer;
        endType = GetComponentInParent<BossEnemyEndType>();
        sphere = GetComponent<SphereCollider>();
        sphere.enabled = false;
    }

    void Update()
    {
        vicinityAttackCheck = endType.GetVicinityAttack();
        attackStartCheck = endType.GetvicinityAttackRangeCheck();

        if( attackStartCheck )
        {
            attackAnimCheck = true;
        }
        else
        {
            if (attackAnimCheck)
            {
                Invoke("attackStart", 0.05f);
            }
        }

        if (vicinityAttackCheck)
        {
            sphere.enabled = true;
        }
        else
        {
            sphere.enabled = false;
        }




    }
    private void attackStart()
    {
        PoolingManager.Instance.RemovePoolingObject(gameObject);
        attackAnimCheck = false;
    }
}
