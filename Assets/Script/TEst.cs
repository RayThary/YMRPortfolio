using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{

    private Player player;
    void Start()
    {
        player = GameManager.instance.GetPlayer;
        player.Pull(transform.position, 1000, 2);
    }

    void Update()
    {
        
    }

}
