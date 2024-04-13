using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossAppear : MonoBehaviour
{
    private RectTransform rectTrs;

    [SerializeField] private float speed = 20;

    private TextMeshProUGUI bossName;
    private TextMeshProUGUI anyKeyText;

    [SerializeField]private List<Sprite> bossSprite = new List<Sprite>();
    private Sprite stageSprite;
    private Image bossImgge;
    private bool stopCheck = false;

    void Start()
    {
        rectTrs = transform.GetChild(1).GetComponent<RectTransform>();
        bossName = rectTrs.GetComponentInChildren<TextMeshProUGUI>();
        //bossName.text =  겟스테이지 수정후에 스테이지에맞게 설정
        bossImgge = transform.GetChild(2).GetComponent<Image>();

        int bulidNum = SceneManager.GetActiveScene().buildIndex;
        for(int i =0; i < 4; i++)
        {
            if (i == bulidNum - 1)
            {
                //bossImgge = bossSprite[i]
            }
        }
        
        anyKeyText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        anyKeyText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        moveBackGround();

    }

    private void moveBackGround()
    {
        if (stopCheck == false)
        {
            rectTrs.position += rectTrs.right * speed;

        }

        if (rectTrs.localPosition.x >= 80)
        {
            stopCheck = true;
            anyKeyText.text = "Press Any Key";
        }
    }



}
