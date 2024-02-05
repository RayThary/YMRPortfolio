using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMove : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 0.4f;
    private Player player;
    [SerializeField] private Unit boss;
    public Unit Boss { set => boss = value; }

    private void OnTriggerStay(Collider other)
    {
        if (transform.position.y <= 0.2)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                player.Hit(boss, 1);
            }
        }
    }
    void Start()
    {
        player = GameManager.instance.GetPlayer;
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        float posY = transform.position.y;

        if (posY <= 0.1f)
        {
            PoolingManager.Instance.RemovePoolingObject(gameObject);

        }
    }
}
