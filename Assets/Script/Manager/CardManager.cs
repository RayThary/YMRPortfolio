
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
        exp = "피해를 받을때마다 피해를 준 유닛에게 " + figure + "만큼 피해를 준다";
    }
    public override void Activation(Launcher launcher, Unit unit)
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
        exp = "상대를 처치 할때마다 체력을 " + figure + " 회복한다";
    }

    public override void Activation(Launcher launcher, Unit unit)
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
        exp = "공격속도가 빨라집니다";
        figure = -0.5f;
    }

    public override void Activation(Launcher launcher, Unit unit)
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

// 발사할때마다 호출해줬으면 한다
// Impact에 원하는 내용 작성후 Activation에 
// launcher.FireCallbackAdd(Impact); 작성
// Deactivation에 
// launcher.FireCallbackRemove(Impact); 작성

// 총알이 상대에게 명중했을때 호출 했으면 한다
// 매개변수가 Unit unit, float figure인 함수를 하나 만들고
// 이때 unit은 내가 누굴 공격했는지 figure는 그 수치는 얼마인지에 대한 정보
// 원하는 내용 작성후 Activation에
// player.stat.AddAttack(만든함수이름); 작성
// Deactivation에
// player.stat.RemoveAttack(만든함수이름); 작성

// 내가 누군가에게 대미지를 입었을때 호출 했으면 한다
// 매개변수가 Unit unit, float figure인 함수를 하나 만들고
// 이때 unit은 내가 누구에게 공격받았는지 figure는 그 수치는 얼마인지에 대한 정보
// 원하는 내용 작성후 Activation에
// player.stat.AddHit(만든함수이름); 작성
// Deactivation에
// player.stat.RemoveHit(만든함수이름); 작성

//예시
//public class ThreeStrokeAttack : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 0;
//        launcher.FireCallbackAdd(Impact);
//    }

//    public override void Deactivation()
//    {
//        launcher.FireCallbackRemove(Impact);
//    }

//    public override void Impact()
//    {
//        figure++;
//        if (figure >= 3)
//        {
//            SpecialFire(30);
//            SpecialFire(-30);
//            figure = 0;
//        }
//    }

//    public void SpecialFire(float angle)
//    {
//        Bullet b = launcher.GetBullet();
//        b.gameObject.SetActive(true);
//        b.transform.position = launcher.muzzle.position;
//        b.transform.localEulerAngles = new Vector3(0, launcher.launcher.eulerAngles.y - 90 - 90 + angle, 0);
//        b.Straight();
//    }
//}

//public class PoisonBullet : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 3;
//        GameManager.instance.GetPlayer.stat.AddAttack(Poison);
//    }
//    public override void Deactivation()
//    {
//        GameManager.instance.GetPlayer.stat.RemoveAttack(Poison);
//    }

//    public override void Impact()
//    {

//    }

//    public void Poison(Unit unit, float figure)
//    {
//        unit.stat.Be_Attacked_Poison(5, this.figure, GameManager.instance.GetPlayer);
//    }
//}

//public class Bloodsucking : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 1;
//        GameManager.instance.GetPlayer.stat.AddAttack(Blood);
//    }
//    public override void Deactivation()
//    {
//        GameManager.instance.GetPlayer.stat.RemoveAttack(Blood);
//    }

//    public override void Impact()
//    {

//    }

//    public void Blood(Unit unit, float f)
//    {
//        GameManager.instance.GetPlayer.stat.RecoveryHP(figure, GameManager.instance.GetPlayer);
//    }
//}

//public class Armor_Of_Thorns : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 1;
//        GameManager.instance.GetPlayer.stat.AddHit(Thorns);
//    }
//    public override void Deactivation()
//    {
//        GameManager.instance.GetPlayer.stat.RemoveHit(Thorns);
//    }

//    public override void Impact()
//    {

//    }

//    public void Thorns(Unit unit, float f)
//    {
//        unit.stat.Be_Attacked_TRUE(figure, GameManager.instance.GetPlayer);
//    }
//}


