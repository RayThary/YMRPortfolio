using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField]private Image hp;
    [SerializeField] Unit boss;
    [SerializeField]private float maxHp;
    [SerializeField]private float nowHp;

    void Start()
    {
        hp = GetComponent<Image>();
         
    }

    // Update is called once per frame
    void Update()
    {
        hp.fillAmount = boss.STAT.HP / boss.STAT.MAXHP;
    }
}
