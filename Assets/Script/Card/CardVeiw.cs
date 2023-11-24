using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVeiw : MonoBehaviour
{
    private CardManager CardManager;
    public Card card;


    public void Init(CardManager c)
    {
        CardManager = c;
    }

    public void Select()
    {
        GameManager.instance.GetPlayer.weapon.CardAdd(card);
        CardManager.Selected();
        //GameManager.instance.GetPlayerTransform.position = new Vector3();
    }
}
