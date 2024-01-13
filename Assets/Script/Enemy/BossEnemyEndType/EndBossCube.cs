using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossCube : Unit
{
    //Å×½ºÆ®
    [SerializeField] private bool halfPattenCheck;
    private float basicAttackTimer = 3;

    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        attackPatten();
        HaxagonPatten();
    }

    private void attackPatten()
    {
        basicAttackTimer += Time.deltaTime;
        if (basicAttackTimer >= 2)
        {
            int pattenNum = Random.Range(0, 3);
            if (pattenNum == 0)
            {
                GameObject attackObj; 
                attackObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.LaserPatten, transform);
                attackObj.transform.position = transform.position;
                basicAttackTimer = 0;
            }
            else if(pattenNum == 1)
            {

            }
        }
    }

    private void HaxagonPatten()
    {
        if (halfPattenCheck == false)
        {   
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
