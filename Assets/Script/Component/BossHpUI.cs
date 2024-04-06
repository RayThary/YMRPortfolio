using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpUI : MonoBehaviour
{
    public Unit Boss;
    Image hp;
    // Start is called before the first frame update
    void Start()
    {

        hp = transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss != null && Boss.STAT != null)
         hp.fillAmount =  Boss.STAT.HP / Boss.STAT.MAXHP;
    }
}
