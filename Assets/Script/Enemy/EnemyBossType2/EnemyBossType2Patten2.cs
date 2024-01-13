using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType2Patten2 : MonoBehaviour
{
    [SerializeField] private float speed=2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //플레이어대미지입힌다 아마도 몇초무적시는 플레이어에서할지도?
        }
    }


    
    void Update()
    {
        transform.Rotate(new Vector3(0,1,0) * speed * Time.deltaTime);
    }
}
