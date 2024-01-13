using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swrod : Weapon
{
    //칼은 다른 무기와는 다르게 직접 충돌을 해야하기에 무엇과 충돌을 할것인지 정해놓음
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
                    //왜 이건 9로 나온는거지 1 >> 9 가 아니라
                    if(colliders[i].gameObject.layer == 9)
                    {
                        //총알을 벤 경우
                        Debug.Log("총알을 벰");
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
        exp = "최대 체력이 " + figure + " 만큼 증가합니다";
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
        exp = "총알을 하나 베어낼때마다 다음 공격에 1의 대미지가 추가됩니다";
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
        exp = "이동속도가 빨리집니다.";
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
