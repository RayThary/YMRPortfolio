using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    private Unit boss;
    public Unit Boss { set => boss = value; }

    [SerializeField] private float Damge = 2;
    [SerializeField] private float Speed;

    private Player player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (boss == null)
            {
                player.Hit(null, Damge);
            }
            else
            {
                player.Hit(boss, Damge);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameManager.instance.GetPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Speed;
    }
}
