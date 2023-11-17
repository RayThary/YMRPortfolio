using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestGun : Weapon
{

    public TestGun(Unit unit, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base(unit, launcher, muzzle, mistake, firerate, objectParent)
    {
        this.firerate = 0.2f;
        sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/Bow_1");

        spriteRenderer = launcher.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

        cards.Add(new ThreeStrokeAttack());
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
            //Ȱ�� ���� �ִ� ������ ������ ���� ������ 90�� ���߱⿡ ���⼱ ����� ��Ȯ�� ������
            b.transform.eulerAngles = new Vector3(0, angle, 0);
            b.unit = parent;
            b.Straight();
        }
    }
}




public class ThreeStrokeAttack : Card
{
    public override void Activation(Launcher launcher)
    {
        base.Activation(launcher);
        figure = 0;
        launcher.FireCallbackAdd(Impact);
    }

    public override void Deactivation()
    {
        launcher.FireCallbackRemove(Impact);
    }

    public override void Impact()
    {
        figure++;
        if (figure >= 3)
        {
            SpecialFire(30);
            SpecialFire(-30);
            figure = 0;
        }
    }

    public void SpecialFire(float angle)
    {
        Bullet b = launcher.GetBullet();
        b.gameObject.SetActive(true);
        b.transform.position = launcher.muzzle.position;
        b.transform.localEulerAngles = new Vector3(0, launcher.launcher.eulerAngles.y - 90 - 90 + angle, 0);
        b.Straight();
    }
}

public class PoisonBullet : Card
{
    public override void Activation(Launcher launcher)
    {
        base.Activation(launcher);
        figure = 3;
        GameManager.instance.GetPlayer.STAT.AddAttack(Poison);
    }
    public override void Deactivation()
    {
        GameManager.instance.GetPlayer.STAT.RemoveAttack(Poison);
    }

    public override void Impact()
    {

    }

    public void Poison(Unit unit, float figure)
    {
        unit.STAT.Be_Attacked_Poison(5, this.figure, GameManager.instance.GetPlayer);
    }
}

public class Bloodsucking : Card
{
    public override void Activation(Launcher launcher)
    {
        base.Activation(launcher);
        figure = 1;
        GameManager.instance.GetPlayer.STAT.AddAttack(Blood);
    }
    public override void Deactivation()
    {
        GameManager.instance.GetPlayer.STAT.RemoveAttack(Blood);
    }

    public override void Impact()
    {

    }

    public void Blood(Unit unit, float f)
    {
        GameManager.instance.GetPlayer.STAT.RecoveryHP(figure, GameManager.instance.GetPlayer);
    }
}

public class Armor_Of_Thorns : Card
{
    public override void Activation(Launcher launcher)
    {
        base.Activation(launcher);
        figure = 1;
        GameManager.instance.GetPlayer.STAT.AddHit(Thorns);
    }
    public override void Deactivation()
    {
        GameManager.instance.GetPlayer.STAT.RemoveHit(Thorns);
    }

    public override void Impact()
    {

    }

    public void Thorns(Unit unit, float f)
    {
        unit.STAT.Be_Attacked_TRUE(figure, GameManager.instance.GetPlayer);
    }
}


