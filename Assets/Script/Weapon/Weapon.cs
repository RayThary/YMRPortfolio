using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Launcher
{
    protected Player player;
    public MonoBehaviour monoBehaviour;
    //��������Ʈ ������
    public SpriteRenderer spriteRenderer;
    //���Ǵ� ��������Ʈ 
    public Sprite sprite;
    //ī�� ����Ʈ
    public List<Card> cards = new List<Card>();
    protected List<Card> activateCardList = new List<Card>();

    public Weapon(Unit unit, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base (unit, launcher, muzzle, mistake, firerate, objectParent)
    {

    }

    public void CardAdd(Card card)
    {
        activateCardList.Add(card);
        card.Activation(this);
    }
    public void CardRemove(Card card)
    {
        activateCardList.Remove(card);
        card.Deactivation();
    }

    public override Bullet GetBullet()
    {
        return PollingManager.Instance.CreateObject(PollingManager.ePoolingObject.PlayerBullet, objectParent).transform.GetComponent<Bullet>();
    }
}
