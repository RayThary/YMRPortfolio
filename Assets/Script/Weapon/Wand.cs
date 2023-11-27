using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapon
{
    public Wand(Unit unit, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base(unit, launcher, muzzle, mistake, firerate, objectParent)
    {
        sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/Sword_3");

        spriteRenderer = launcher.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

        CardAdd(new Laser());
    }

    public override void BulletControl()
    {
        if (firerateCoroutine == null)
        {
            Bullet b = GetBullet();
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

public class Laser : Card
{
    int layer = 1 << 7;
    private Bullet laser;
    private float maxdistance = 10;
    public override void Activation(Launcher launcher)
    {
        base.Activation(launcher);
        figure = 5;
        launcher.FireCallbackAdd(Impact);
        launcher.FireCallbackRemove(launcher.BulletControl);
        laser = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Laser, launcher.objectParent).transform.GetComponent<Bullet>();
    }

    public override void Deactivation()
    {

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
                Bullet b = GameObject.Instantiate(laser, positoin + dir * hit.distance * 0.5f, Quaternion.Euler(new Vector3(0, angle, 0)));
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
                Bullet b = GameObject.Instantiate(laser, positoin + dir * distance * 0.5f, Quaternion.Euler(new Vector3(0, angle, 0)));
                b.transform.localScale = new Vector3(0.5f, 0.5f, distance);
                //�ߵ��� ���� �ʾҴٴ� ���� �Ÿ��� �����ٴ°�
                break;
            }
        }
    }


}