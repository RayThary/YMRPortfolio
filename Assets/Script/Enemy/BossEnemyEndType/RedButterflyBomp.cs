using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButterflyBomp : MonoBehaviour
{
    private SpriteRenderer spr;
    private Color color;
    private bool alphaRemove = false;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        color = spr.color;
    }


    void Update()
    {

        
        alphaCheck();
        objRemove();
        
    }

    private void alphaCheck()
    {
        if (spr.color.a == 1)
        {
            alphaRemove = true;
        }

        if (alphaRemove)
        {
            color.a -= Time.deltaTime;
        }

        spr.color = color;
    }

    private void objRemove()
    {
        if (color.a == 0)
        {
            
            PoolingManager.Instance.RemovePoolingObject(gameObject);
            color.a = 1;
        }
    }

}
