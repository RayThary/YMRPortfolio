using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swrod : Weapon
{
    //칼은 다른 무기와는 다르게 직접 충돌을 해야하기에 무엇과 충돌을 할것인지 정해놓음
    // 1 << 8 == Unit , 1 << 9 == Bullet
    int layer = 1 << 8 | 1 << 9;

    public Swrod(Unit unit, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base(unit, launcher, muzzle, mistake, firerate, objectParent)
    {
        sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/Sword_3");

        spriteRenderer = launcher.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

    }

    public override void BulletControl()
    {
        if (firerateCoroutine == null)
        {
            Collider[] colliders = Physics.OverlapSphere(launcher.position, 2, layer);
            float myangle = a.NormalizeAngle(launcher.eulerAngles.y);
            float angle;
            Debug.Log("Fire");
            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log($"{colliders[i].transform.name}");
                angle = a.NormalizeAngle(a.WorldAngleCalculate(launcher.position, colliders[i].transform.position));
                Debug.Log($"{myangle}, {angle}");
                if (angle > myangle - 30 && angle < myangle + 30)
                {
                    Debug.Log("Hit");
                }
            }
        }
    }
    
}
