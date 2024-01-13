using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public CardManager cardManager;
    public static GameManager instance;

    private Player player;
    public Player GetPlayer { get { return player; } }    
    public Transform GetPlayerTransform { get { return player.transform; } }

    public bool b = false;

    public Transform GetenemyObjectBox { get { return transform.GetChild(0); } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        player = FindObjectOfType<Player>();
        enemyManager.Init();
        enemyManager.GetStage = enemyManager.Stages[0];
        enemyManager.GetStage.spawnEnemy();
    }

    void Start()
    {
       
    }

    private void Update()
    {
        if(b)
        {
            b = false;
            cardManager.ViewCards();
        }
        //if(enemyManager.EnemyClear())
        //{
        //    Debug.Log("다음 스테이지로");
        //    //    //카드 고르고 다음 라운드로
        //    //    cardManager.ViewCards();
        //    //    //스테이지 바꾸고 spawnEnemy
        //    //    stageindex++;
        //    //    nowStage = stages[stageindex];
        //    //    spawnEnemy();
        //    // stageindex = 0;
        //    //cardManager.ViewCards();
        //    //nowStage = stages[0];
        //    //nowStage.spawnEnemy(EnemyParent);
        //}
    }

    public void CardSelectStep()
    {

    }

    public void NextStageStep()
    {

    }
}
