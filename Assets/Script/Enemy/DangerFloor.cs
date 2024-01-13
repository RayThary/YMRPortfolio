using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerFloor : MonoBehaviour
{
    [SerializeField] private float time = 2;

    private SpriteRenderer spr;
    private Color color;
    private bool alphaCheck;

    private float alpha = 0.9f;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        color = spr.color;

    }

    
    void Update()
    {
        alphaControl();
        objCheck();
    }

    private void alphaControl()
    {
        color.a = alpha;
        if (alpha <= 0.5f)
        {
            alphaCheck = true;
        }
        else if (alpha >= 0.8f)
        {
            alphaCheck = false;
        }

        if (alphaCheck)
        {
            alpha += Time.deltaTime * 0.5f;
        }
        else
        {
            alpha -= Time.deltaTime * 0.5f;
        }
        spr.color = color;
    }

    private void objCheck()
    {
        if (spr.enabled == true)
        {
            StartCoroutine(offObj());
        }
    }
    IEnumerator offObj()
    {
        yield return new WaitForSeconds(time);
        spr.enabled = false;
    }
}
