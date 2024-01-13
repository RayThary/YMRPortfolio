using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swrod : Weapon
{
    //Į�� �ٸ� ����ʹ� �ٸ��� ���� �浹�� �ؾ��ϱ⿡ ������ �浹�� �Ұ����� ���س���
    // 1 << 8 == Unit , 1 << 9 == Bullet
    int layer = 1 << 8 | 1 << 9;
    public Action bulletCutting;
    public float compareAngle = 30;

    public Swrod(Player player, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base(player, launcher, muzzle, mistake, firerate, objectParent)
    {
        sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/Sword_3");

        spriteRenderer = launcher.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

        cards.Add(new Sense(this));
    }

    public override void BulletControl()
    {
        if (firerateCoroutine == null)
        {
            Collider[] colliders = Physics.OverlapSphere(launcher.position, 2, layer);
            
            for (int i = 0; i < colliders.Length; i++)
            {
                Vector3 target = (colliders[i].transform.position - launcher.transform.position).normalized;
                float angle = Mathf.Acos(Vector3.Dot(launcher.up, target)) * Mathf.Rad2Deg;
                if (angle < compareAngle && angle > -compareAngle)
                {
                    //�� �̰� 9�� ���´°��� 1 >> 9 �� �ƴ϶�
                    if(colliders[i].gameObject.layer == 9)
                    {
                        //�Ѿ��� �� ���
                        Debug.Log("�Ѿ��� ��");
                        if (bulletCutting != null)
                            bulletCutting();
                        PoolingManager.Instance.RemovePoolingObject(colliders[i].gameObject);
                    }
                    else
                    {
                        colliders[i].GetComponent<Unit>().Hit(parent, 1);
                    }
                }
            }
        }
    }
}

[System.Serializable]
public  class Health : Card
{
    public Health()
    {
        figure = 10;
        exp = "�ִ� ü���� " + figure + " ��ŭ �����մϴ�";
    }

    public override void Activation(Launcher launcher, Unit unit)
    {
        base.Activation(launcher, unit);
        user.STAT.MAXHP = figure;
    }

    public override void Deactivation()
    {
        user.STAT.MAXHP = -figure;
    }

    public override void Impact()
    {

    }
}

[System.Serializable]
public class Sense : Card
{
    public Sense(Swrod swrod)
    {
        figure = 0;
        swrod.bulletCutting = Impact;
        exp = "�Ѿ��� �ϳ� ��������� ���� ���ݿ� 1�� ������� �߰��˴ϴ�";
    }

    public override void Activation(Launcher launcher, Unit unit)
    {
        base.Activation(launcher, unit);
        this.figure = 0;
        user.STAT.AddAttack(Attack);
    }

    public override void Deactivation()
    {
        user.STAT.RemoveAttack(Attack);
    }

    public void Attack(Unit unit, float figure)
    {
        unit.STAT.MinusHp(this.figure);
        this.figure = 0;
    }

    public override void Impact()
    {
        figure++;
    }
}

[System.Serializable]
public class Run : Card
{
    public Run()
    {
        exp = "�̵��ӵ��� �������ϴ�.";
    }

    public override void Activation(Launcher launcher, Unit unit)
    {
        base.Activation(launcher, unit);
        GameManager.instance.GetPlayer.moveSpeed += 1;
    }

    public override void Deactivation()
    {
        GameManager.instance.GetPlayer.moveSpeed -= 1;
    }

    public override void Impact()
    {

    }
}
