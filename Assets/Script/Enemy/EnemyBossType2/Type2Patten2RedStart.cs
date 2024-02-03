using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Patten2RedStart : MonoBehaviour
{

    private Player player;
    [SerializeField]private EnemyBossType2 type2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.Hit(type2, 2);
        }
    }
    void Start()
    {
        player = GameManager.instance.GetPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("alive");
    }
    public void setType2(EnemyBossType2 _type2)
    {
        type2 = _type2;
    }
}