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
        exp = "������ ���а� ����";
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
        exp = "�⺻������ ��븦 ���󰣴�";
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
        exp = "���ݽ� �⺻���� ��� �������� �߻��Ѵ� \n" +
            "�ִ�Ÿ��� 10�̴�";
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
        //�� �������� �ִ���̰� �������ֱ⿡ �ݻ縦 ���� �������� ����

        //������ �߻������ �˷����� �ʱ⿡ ���� �˾Ƴ�����

        //������ ���� ��ȯ �� cos���� ���
        float cosY = Mathf.Cos(launcher.angle * Mathf.Deg2Rad);
        //... sin���� ���
        float sinY = Mathf.Sin(launcher.angle * Mathf.Deg2Rad);
        //������ ���⺤�ͷ� ��ȯ
        Vector3 dir = new Vector3(sinY, 0, cosY);
        RaycastHit hit;
        //�������� �Ÿ�
        float distance = maxdistance;
        //�߻�� ��ġ
        Vector3 positoin = launcher.muzzle.position /*+ launcher.muzzle.up*/;
        //ũ�⸦ ���ΰ��� �ݸ�ŭ ������ �̵�

        //�������� ����
        float angle = launcher.angle;

        //�ϴ� �Ÿ��� ��� �������� �߻��ؾ���
        while(distance > 0)
        {
            //�Ÿ����� �߻���ġ���� �������� �Ÿ���ŭ �߻�
            if (Physics.Raycast(launcher.muzzle.position, dir, out hit, distance, layer))
            {
                //�浹�� �ߴٴ°��� ���� �Ÿ��� �ִٴ°�
                Bullet b = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Laser, launcher.objectParent).transform.GetComponent<Bullet>();
                b.unit = user;
                b.transform.localScale = new Vector3(0.5f, 0.5f, hit.distance);

                //�������� �߻�� ��ġ
                positoin = hit.point;
                //�߻�� �Ÿ�
                distance = distance - hit.distance;
                //�������� ����
                Vector2 vector = Vector2.Reflect(new Vector2(dir.x, dir.z), new Vector2(hit.normal.x, hit.normal.z));
                dir = new Vector3(vector.x, 0, vector.y);
                //�������� ����
                angle = a.DirAngle(vector);
            }
            else
            {
                Bullet b = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Laser, launcher.objectParent).transform.GetComponent<Bullet>();
                b.damage = launcher.player.STAT.AD;
                b.unit = user;
                b.transform.localScale = new Vector3(0.5f, 0.5f, distance);
                //�ߵ��� ���� �ʾҴٴ� ���� �Ÿ��� �����ٴ°�
                break;
            }
        }
    }


}