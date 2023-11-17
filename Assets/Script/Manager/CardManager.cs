using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardVeiw[] view;
    

    private void Start()
    {
        for(int i = 0; i < view.Length; i++)
        {
            view[i].Init(this);
        }
        //ViewCards();
    }

    public void ViewCards()
    {
        Card[] cards = a.GetRandomCards(GameManager.instance.GetPlayer.weapon.cards, view.Length);
        for (int i = 0; i < view.Length; i++)
        {
            view[i].gameObject.SetActive(true);
            view[i].card = cards[i];
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
