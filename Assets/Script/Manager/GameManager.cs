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

    private Transform enemyAttackObj;
    public Transform GetEnemyAttackObjectPatten { get { return enemyAttackObj; } }

    public bool CardTest = false;

    public Transform GetenemyObjectBox { get { return transform.GetChild(0); } }

    public GameObject playerDeadButton;
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
        enemyAttackObj = transform.GetChild(0);
        enemyManager.Init();
        enemyManager.GetStage = enemyManager.Stages[0];
        enemyManager.GetStage.spawnEnemy();

        //프레임 설정 / 화면비
        RefreshRate rate = new RefreshRate();
        rate.numerator = 120;
        Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow, rate);
        //프레임 제한 최대 60
        Application.targetFrameRate = 60;//59 
    }

    void Start()
    {
       
    }

    private void Update()
    {
        if(CardTest)
        {
            CardTest = false;
            cardManager.ViewCards();
        }
     
    }
    public void CardSelectStep()
    {

    }

    public void NextStageStep()
    {

    }

    public Collider NearbyTrnasform(Collider[] list, Transform center)
    {
        if (list.Length == 0)
            return null;
        int index = 0;
        float min = Vector3.Distance(center.position, list[0].transform.position);
        for (int i = 1; i < list.Length; i++)
        {
            float distnace = Vector3.Distance(center.position, list[i].transform.position);
            if (distnace < min)
            {
                min = distnace;
                index = i;
            }
        }
        return list[index];
    }

    public void PlayerDead()
    {
        playerDeadButton.SetActive(true);
    }

    public void ExitGame()
    {
        //Debug.Log("종료 코드");
        Application.Quit();
    }

    public void RestartGame()
    {
        //다시시작 코드
        Debug.Log("다시시작 코드");
    }

}
