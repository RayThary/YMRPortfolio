using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    private Image hp;
    [SerializeField] GameObject BossParent;
    [SerializeField] Unit boss;
    [SerializeField] private float maxHp;
    [SerializeField] private float nowHp;

    void Start()
    {
        //boss = BossParent.transform.Find("UnitRoot").GetComponent<>;
        hp = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null)
        {
            hp.fillAmount = boss.STAT.HP / boss.STAT.MAXHP;
        }
    }
}
