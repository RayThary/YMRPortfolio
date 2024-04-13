using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : Bullet
{
    private Coroutine guided = null;
    protected EnemyManager enemyManager = null;

    public override void Straight()
    {
        base.Straight();
        if (guided != null)
        {
            StopCoroutine(guided);
        }

        enemyManager = FindObjectOfType<EnemyManager>();
        guided = StartCoroutine(GuidedCoroutine());
    }

    protected IEnumerator GuidedCoroutine()
    {
        //���� �� �׾��ִ��� üũ�ϴ� ��
        List<GameObject> list = enemyManager.GetStage.ActiveEnemy();
        while (list != null && list.Count > 0)
        {
            Transform target = list[0].transform;
            for (int i = 1; i < list.Count; i++)
            {
                if(Vector3.Distance(transform.position , list[i].transform.position) < Vector3.Distance(transform.position, target.position))
                {
                    target = list[i].transform;
                }
            }


            Vector3 dir = (target.transform.position - transform.position).normalized;
            // ����(dot)�� ���� ������ ����. (Acos�� ���� ������ ������ �� ���� ����)
            float dot = Vector3.Dot(transform.forward, dir);
            if (dot < 1.0f)
            {
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                // ������ ���� ������ ������ �Ǻ�.
                Vector3 cross = Vector3.Cross(transform.forward, dir);
                // ���� ��� ���� ���� ���� �ݿ�
                if (cross.y < 0)
                {
                    angle = transform.rotation.eulerAngles.z - Mathf.Min(10, angle);
                }
                else
                {
                    angle = transform.rotation.eulerAngles.z + Mathf.Min(10, angle);
                }
                transform.eulerAngles = transform.eulerAngles + new Vector3(0, angle * 0.1f, 0);
            }
            yield return null;
        }
    }

    protected void StopGuided()
    {
        if(guided != null)
        {
            StopCoroutine(guided);
        }
    }

    public override void Judgment(Collider other)
    {
        base.Judgment(other);
        StopGuided();
    }
}
