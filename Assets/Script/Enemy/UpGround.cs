using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGround : MonoBehaviour
{
    private bool upTimeCheck = false;
    private bool downTimeCheck = false;
    private float timer = 0;
    [SerializeField] private float upSpeed = 1;
    private SpriteRenderer spr;

    void Start()
    {
        //소환해줄위치를 소환자에서지정해줄것 y값만 -1.1로 해줘야함((-y*0.5)-0.1  값으로)
        transform.position =  new Vector3(transform.position.x,-1.1f,transform.position.z);

        spr = GetComponentInChildren<SpriteRenderer>();
    }

    
    void Update()
    {

        blockUpTime();
        blockUp();
       
    }

    private void blockUpTime()
    {
        if (downTimeCheck)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 2.3f)
        {
            upTimeCheck = true;
        }
    }

    private void blockUp()
    {
        if (upTimeCheck)
        {
            transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
        }

        if (transform.position.y >= 0.5f)
        {
            upTimeCheck = false;
            downTimeCheck = true;
        }

        if (downTimeCheck)
        {
            transform.position += new Vector3(0, -upSpeed * Time.deltaTime, 0);
        }

        //리무브
        if (transform.position.y <= -1.3f)
        {
            
            downTimeCheck = false;
            upTimeCheck = false;
            timer = 0;
            
            PoolingManager.Instance.RemovePoolingObject(gameObject);
        }
    }

   
}
