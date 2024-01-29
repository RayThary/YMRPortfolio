
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardVeiw[] view;
    public List<Card> publicCards = new List<Card>();

    private void Start()
    {
        for(int i = 0; i < view.Length; i++)
        {
            view[i].Init(this);
        }
        publicCards.Add(new Armor_Of_Thorns());
        publicCards.Add(new Absorption());
    }

    public void ViewCards()
    {
        List<Card> list = new List<Card> ();
        list.AddRange(publicCards);
        Debug.Log(GameManager.instance.GetPlayer.weapon);
        list.AddRange(GameManager.instance.GetPlayer.weapon.cards);
        Card[] cards = a.GetRandomCards(list, view.Length);
        if (cards == null )
            return;
        for (int i = 0; i < view.Length; i++)
        {
            view[i].gameObject.SetActive(true);
            view[i].card = cards[i];
            view[i].transform.GetChild(0).GetComponent<Text>().text = view[i].card.ToString();
        }
    }

    public void Selected()
    {
        for(int i = 0; i < view.Length; i++)
        {
            view[i].gameObject.SetActive(false);
            view[i].card = null;
        }
    }
}



[System.Serializable]
public class Armor_Of_Thorns : Card
{
    public Armor_Of_Thorns()
    {
        figure = 1;
        exp = "���ظ� ���������� ���ظ� �� ���ֿ��� " + figure + "��ŭ ���ظ� �ش�";
    }
    public override void Activation(Weapon launcher, Player unit)
    {
        base.Activation(launcher, unit);

        user.STAT.AddHit(Thorns);
    }
    public override void Deactivation()
    {
        user.STAT.RemoveHit(Thorns);
    }

    public override void Impact()
    {

    }

    public void Thorns(Unit unit, float f)
    {
        user.STAT.Be_Attacked_TRUE(figure, GameManager.instance.GetPlayer);
    }
}

public class Absorption : Card
{
    public Absorption()
    {
        figure = 1;
        exp = "��븦 óġ �Ҷ����� ü���� " + figure + " ȸ���Ѵ�";
    }

    public override void Activation(Weapon launcher, Player unit)
    {
        base.Activation(launcher, unit);
        user.STAT.AddAttack(death);
    }

    public override void Deactivation()
    {
        user.STAT.RemoveAttack(death);
    }

    public override void Impact()
    {

    }

    public void death(Unit unit, float figure)
    {
        if (unit.STAT.HP <= 0)
        {
            user.STAT.RecoveryHP(this.figure, GameManager.instance.GetPlayer);
        }
    }
}

[System.Serializable]
public class QuickAttack : Card
{
    public QuickAttack()
    {
        exp = "���ݼӵ��� �������ϴ�";
        figure = -0.5f;
    }

    public override void Activation(Weapon launcher, Player unit)
    {
        base.Activation(launcher, unit);
        launcher.FireRate += figure;
    }

    public override void Deactivation()
    {
        launcher.FireRate -= figure;
    }

    public override void Impact()
    {

    }
}
