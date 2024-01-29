using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Launcher
{
    public Player player;
    public MonoBehaviour monoBehaviour;
    //스프라이트 렌더러
    public SpriteRenderer spriteRenderer;
    //사용되는 스프라이트 
    public Sprite sprite;
    //카드 리스트
    public List<Card> cards = new List<Card>();
    protected List<Card> activateCardList = new List<Card>();

    public Weapon(Player player, Transform launcher, Transform muzzle, float mistake, float firerate, Transform objectParent) : base (player, launcher, muzzle, mistake, firerate, objectParent)
    {
        this.player = player;
    }

    public void CardAdd(Card card)
    {
        activateCardList.Add(card);
        cards.Remove(card);
        card.Activation(this, player);
    }
    public void CardRemove(Card card)
    {
        activateCardList.Remove(card);
        cards.Add(card);
        card.Deactivation();
    }

    public override Bullet GetBullet()
    {
        return PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.PlayerBullet, objectParent).transform.GetComponent<Bullet>();
    }
}
