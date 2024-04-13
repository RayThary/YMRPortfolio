using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    private Player _player;
    public Image hpImage;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance.GetPlayer;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = _player.STAT.HP / _player.STAT.MAXHP;

    }
}
