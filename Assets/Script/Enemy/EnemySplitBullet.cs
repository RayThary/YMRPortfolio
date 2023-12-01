using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EnemySplitBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool splitCheck = false;
    private bool stop = false;
    [SerializeField] private Transform parentTrs;
    [SerializeField] private List<GameObject> bulletObj = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.GetMask("Wall"))
        {
            splitCheck = true;
        }
    }
    void Start()
    {
        Transform playerTrs = GameManager.instance.GetPlayerTransform;
        transform.rotation = Quaternion.LookRotation(playerTrs.position - transform.position);
    }


    void Update()
    {
        if (stop)
        {
            return;
        }
        bulletSplit();
        splitBulletMove();
    }

    private void splitBulletMove()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void bulletSplit()
    {
        if (splitCheck)
        {

            float angle = 0;

            for (int i = 0; i < 8; i++)
            {
                bulletObj.Add(PoolingManager.Instance.CreateObject("aaa", parentTrs));
                bulletObj[i].transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                angle += 45;
            }


            //bulletObj.AddRange(PoolingManager.Instance.CreateObject("aaa", transform, 4));

            stop = true;
        }
    }

}
