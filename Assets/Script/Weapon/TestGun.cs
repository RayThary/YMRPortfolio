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
        //무기의 위치를 스크린위치로 변환해서 각도를 계산
        //raycast는 충돌을 하지 않는 경우에 각도를 구하지 않기에
        //ScreenToWorld는 길이를 정해줘야하는 문제가 있기에 높이가 다를 수 있음
        angle = a.ScreenAngleCalculate(screen, Camera.main.WorldToScreenPoint(launcher.position));

        //localEulerAnlge을 쓰다가 문제가 발생 무기마다 각도의 기준이 다름
        //local좌표가 아닌 world좌표를 써야함
        //활은 추가로 90도를 더했는데 활은 기본적으로 왼쪽을 보고있기에 90도를 더해줌
        launcher.eulerAngles = new Vector3(90, angle + 90, 0);
    }

    public override void BulletControl()
    {
        if (firerateCoroutine == null)
        {
            Bullet b = GetBullet();
            b.transform.position = muzzle.position;
            b.damage = player.STAT.AD;
            //활은 보고 있는 방향의 문제로 따로 각도를 90도 더했기에 여기선 빼줘야 정확한 각도임
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
        exp = "총알이 한번에 3발씩 나가게 된다 \n" +
            "총알의 속도가 2만큼 오르고 총알이 0.5초후 사라진다 \n" +
            "공격속도가 0.5초 느려진다";
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
        exp = "공격시 상대방에게 대미지 3, 5회 발동하는 독데미지를 부여한다";
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
        exp = "공격 적중시마다 체력을 1 회복한다";
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




