using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestGun : Weapon
{

    public TestGun(Player player, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base(player, launcher, muzzle, mistake, firerate, objectParent)
    {
        this.firerate = 0.3f;
        sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/Bow_1");

        spriteRenderer = launcher.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

        cards.Add(new ShotGun());
        cards.Add(new PoisonBullet());
        cards.Add(new Bloodsucking());
    }

    public override void Direction_Calculation_Screen(Vector3 screen)
    {
        //������ ��ġ�� ��ũ����ġ�� ��ȯ�ؼ� ������ ���
        //raycast�� �浹�� ���� �ʴ� ��쿡 ������ ������ �ʱ⿡
        //ScreenToWorld�� ���̸� ��������ϴ� ������ �ֱ⿡ ���̰� �ٸ� �� ����
        angle = a.ScreenAngleCalculate(screen, Camera.main.WorldToScreenPoint(launcher.position));

        //localEulerAnlge�� ���ٰ� ������ �߻� ���⸶�� ������ ������ �ٸ�
        //local��ǥ�� �ƴ� world��ǥ�� �����
        //Ȱ�� �߰��� 90���� ���ߴµ� Ȱ�� �⺻������ ������ �����ֱ⿡ 90���� ������
        launcher.eulerAngles = new Vector3(90, angle + 90, 0);
    }

    public override void BulletControl()
    {
        if (firerateCoroutine == null)
        {
            Bullet b = GetBullet();
            b.transform.position = muzzle.position;
            b.damage = player.STAT.AD;
            //Ȱ�� ���� �ִ� ������ ������ ���� ������ 90�� ���߱⿡ ���⼱ ����� ��Ȯ�� ������
            b.transform.eulerAngles = new Vector3(0, angle, 0);
            b.unit = parent;
            b.Straight();
        }
    }
}



[System.Serializable]
public class ShotGun : Card
{
    public ShotGun()
    {
        exp = "�Ѿ��� �ѹ��� 3�߾� ������ �ȴ� \n" +
            "�Ѿ��� �ӵ��� 2��ŭ ������ �Ѿ��� 0.5���� ������� \n" +
            "���ݼӵ��� 0.5�� ��������";
    }

    public override void Activation(Weapon launcher, Player unit)
    {
        base.Activation(launcher, unit);
        launcher.FireRate += 0.5f;
        launcher.FireCallbackAdd(Impact);
        launcher.FireCallbackRemove(launcher.BulletControl);
    }

    public override void Deactivation()
    {
        launcher.FireCallbackRemove(Impact);
        launcher.FireCallbackAdd(launcher.BulletControl);
    }

    public override void Impact()
    {
        if(launcher.CanShot())
        {
            SpecialFire(10);
            SpecialFire(0);
            SpecialFire(-10);
        }
    }

    public void SpecialFire(float angle)
    {
        Bullet b = launcher.GetBullet();
        b.gameObject.SetActive(true);
        b.timer = 0.5f;
        b.speed += 2;
        b.transform.position = launcher.muzzle.position;
        b.transform.localEulerAngles = new Vector3(0, launcher.angle + angle, 0);
        b.Straight();
    }
}

[System.Serializable]
public class PoisonBullet : Card
{
    public PoisonBullet()
    {
        figure = 3;
        exp = "���ݽ� ���濡�� ����� 3, 5ȸ �ߵ��ϴ� ���������� �ο��Ѵ�";
    }

    public override void Activation(Weapon launcher, Player unit)
    {
        base.Activation(launcher, unit);
        user.STAT.AddAttack(Poison);
    }
    public override void Deactivation()
    {
        user.STAT.RemoveAttack(Poison);
    }

    public override void Impact()
    {

    }

    public void Poison(Unit unit, float figure)
    {
        unit.HitDot(_DOT.POISON, 5, this.figure, user);
    }
}

[System.Serializable]
public class Bloodsucking : Card
{
    public Bloodsucking()
    {
        figure = 1;
        exp = "���� ���߽ø��� ü���� 1 ȸ���Ѵ�";
    }
    public override void Activation(Weapon launcher, Player unit)
    {
        base.Activation(launcher, unit);

        user.STAT.AddAttack(Blood);
    }
    public override void Deactivation()
    {
        user.STAT.RemoveAttack(Blood);
    }

    public override void Impact()
    {

    }

    public void Blood(Unit unit, float f)
    {
        user.STAT.RecoveryHP(figure, GameManager.instance.GetPlayer);
    }
}




