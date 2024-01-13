using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossCube : Unit
{
    //Å×½ºÆ®
    [SerializeField] private bool halfPattenCheck;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
        HaxagonPatten();
    }

    private void HaxagonPatten()
    {
        if (halfPattenCheck == false)
        {
            //if (stat.HP == stat.MAXHP / 2)
            //{
            //    StartCoroutine(HaxagonLaser());
            //    halfPattenCheck = true;
            //}
            StartCoroutine(HaxagonLaser());
            halfPattenCheck = true;
        }
    }

    IEnumerator HaxagonLaser()
    {
        for (int i = 0; i < 8; i++)
        {
            PoolingManager.Instance.CreateObject("HaxagonLaser", transform);
            yield return new WaitForSeconds(4);

        }

    }
}
