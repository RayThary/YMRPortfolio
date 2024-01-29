using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wand : Weapon
{
    public Wand(Player player, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base(player, launcher, muzzle, mistake, firerate, objectParent)
    {
        sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/Soon_Spear");

        spriteRenderer = launcher.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

        cards.Add(new Laser());
        cards.Add(new Guided());
        cards.Add(new ShieldRotation());
        //CardAdd(new ShieldRotation());
    }

    public override void BulletControl()
    {
        if (firerateCoroutine == null)
        {
            Bullet b = GetBullet();
            b.damage = player.STAT.AD;
            b.unit = parent;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Vector3 hitPos = hit.point;
                hitPos.y = 0.3f;

                b.transform.localRotation = Quaternion.Euler(0, 90, 0);
                b.transform.position = hitPos;
            }

            b.gameObject.SetActive(true);
            b.timer = 1.5f;
            b.TimerStart();
        }
    }
}

[System.Serializable]
public class ShieldRotation : Card
{
    Shield[] shields = null;

    public ShieldRotation() 
    {
        exp = "주위에 방패가 돈다";
        shields = new Shield[5];    
    }

    public override void Activation(Weapon launcher, Player player)
    {
        base.Activation(launcher, player);
        for (int i = 0; i < shields.Length; i++)
        {
            shields[i] = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Shield, launcher.objectParent).GetComponent<Shield>();
            shields[i].Init(shields.Length, i, 2, 50, player.transform);
        }
    }

    public override void Impact()
    {

    }

    public override void Deactivation()
    {

    }
}

[System.Serializable]
public class Guided : Card
{
    public Guided() 
    {
        exp = "기본공격이 상대를 따라간다";
    }

    public override void Activation(Weapon launcher, Player player)
    {
        base.Activation(launcher, player); 
        
        launcher.FireCallbackAdd(Impact);
        launcher.FireCallbackRemove(launcher.BulletControl);
    }

    public override void Deactivation()
    {
        launcher.FireCallbackAdd(launcher.BulletControl);
        launcher.FireCallbackRemove(Impact);
    }

    public override void Impact()
    {
        Bullet b = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.GuidedBullet, launcher.objectParent).transform.GetComponent<GuidedBullet>();
        b.unit = user;
        b.transform.position = launcher.muzzle.position;
        b.transform.eulerAngles = new Vector3(0, launcher.angle, 0);
        b.Straight();
    }
}

[System.Serializable]
public class Laser : Card
{
    int layer = 1 << 7;
    private float maxdistance = 10;
    public Laser()
    {
        figure = 5;
        exp = "공격시 기본공격 대신 레이저를 발사한다 \n" +
            "최대거리는 10이다";
    }

    public override void Activation(Weapon launcher, Player player)
    {
        base.Activation(launcher, player);
       
        launcher.FireCallbackAdd(Impact);
        launcher.FireCallbackRemove(launcher.BulletControl);
    }

    public override void Deactivation()
    {
        launcher.FireCallbackAdd(launcher.BulletControl);
        launcher.FireCallbackRemove(Impact);
    }

    public override void Impact()
    {
        //이 모든과정은 최대길이가 정해져있기에 반사를 하지 않을수도 있음

        //유저가 발사방향을 알려주지 않기에 직접 알아내야함

        //각도를 수로 변환 후 cos값을 얻기
        float cosY = Mathf.Cos(launcher.angle * Mathf.Deg2Rad);
        //... sin값을 얻기
        float sinY = Mathf.Sin(launcher.angle * Mathf.Deg2Rad);
        //각도를 방향벡터로 전환
        Vector3 dir = new Vector3(sinY, 0, cosY);
        RaycastHit hit;
        //레이저의 거리
        float distance = maxdistance;
        //발사될 위치
        Vector3 positoin = launcher.muzzle.position /*+ launcher.muzzle.up*/;
        //크기를 줄인거의 반만큼 앞으로 이동

        //레이저의 각도
        float angle = launcher.angle;

        //일단 거리를 재고 레이저를 발사해야함
        while(distance > 0)
        {
            //거리설정 발사위치에서 방향으로 거리만큼 발사
            if (Physics.Raycast(launcher.muzzle.position, dir, out hit, distance, layer))
            {
                //충돌을 했다는것은 남은 거리가 있다는것
                Bullet b = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Laser, launcher.objectParent).transform.GetComponent<Bullet>();
                b.unit = user;
                b.transform.localScale = new Vector3(0.5f, 0.5f, hit.distance);

                //레이저가 발사될 위치
                positoin = hit.point;
                //발사될 거리
                distance = distance - hit.distance;
                //레이저의 방향
                Vector2 vector = Vector2.Reflect(new Vector2(dir.x, dir.z), new Vector2(hit.normal.x, hit.normal.z));
                dir = new Vector3(vector.x, 0, vector.y);
                //레이저의 각도
                angle = a.DirAngle(vector);
            }
            else
            {
                Bullet b = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Laser, launcher.objectParent).transform.GetComponent<Bullet>();
                b.damage = launcher.player.STAT.AD;
                b.unit = user;
                b.transform.localScale = new Vector3(0.5f, 0.5f, distance);
                //중돌을 하지 않았다는 것은 거리가 끝났다는것
                break;
            }
        }
    }


}