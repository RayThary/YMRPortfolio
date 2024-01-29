using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JyBossBigBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(1 << other.gameObject.layer ==  LayerMask.GetMask("Wall"))
        {
            Vector2 reflect;
            Vector2 dir = (new Vector2(other.transform.position.x, other.transform.position.z) - new Vector2(transform.position.x, transform.position.z)).normalized;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, new Vector3(dir.x,0, dir.y) , out hit, float.MaxValue, LayerMask.GetMask("Wall")))
            {
                reflect = Vector2.Reflect(dir, new Vector2(hit.normal.x, hit.normal.z));
                float angle = Mathf.Atan2(reflect.x, reflect.y) * Mathf.Rad2Deg;
                transform.localEulerAngles = new Vector3(0, angle, 0);
            }
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().Hit(unit, damage);
        }
    }
}
