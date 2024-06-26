using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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

    private List<int> stageList = new List<int>();

    [SerializeField]
    //카드 고를때 화면을 가릴 이미지
    private Image cardSelectWindow;

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
        //enemyManager.GetStage.spawnEnemy();

        //프레임 설정 / 화면비
        RefreshRate rate = new RefreshRate();
        rate.numerator = 120;
        Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow, rate);
        //프레임 제한 최대 60
        Application.targetFrameRate = 60;//59 

        for(int i = 1; i < 5; i++)
        {
            stageList.Add(i);
        }
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
    //보스가 죽으면 실행
    public void CardSelectStep()
    {
        //근데 무기를 장착중인지 확인 해야함
        WeaponDepot w = player.GetComponent<WeaponDepot>();
        //장착하고 있는 무기의 갯수가 0개가 아니다!
        if (w.Launcher.AttackTypes.Count != 0)
        {
            //화면 가려주고
            cardSelectWindow.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
            //카드 보여주고
            CardTest = true;
        }
    }

    //카드를 고르면 실행
    public void CardSelected()
    {
        NextStageStep();
    }

    //CardSelected 작동한 후에 실행
    public void NextStageStep()
    {
        //화면 다시 열어주고
        cardSelectWindow.rectTransform.sizeDelta = new Vector2(0, 0);
        //스테이지 중에 랜덤으로 하나 골라서 씬 로드
        int stage = stageList[ Random.Range(0, stageList.Count)];
        stageList.Remove(stage);
        SceneManager.LoadScene("BossType" + stage);
        //여기서 선택한 씬을 기록
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
        Time.timeScale = 0;
        playerDeadButton.SetActive(true);
    }

    public void ExitGame()
    {
        //Debug.Log("종료 코드");
        Application.Quit();
    }

    //다시시작 코드
    public void RestartGame()
    {
        //NextStageStep 에서 선택한 씬을 다시 로드

        //플레이어의 스탯을 다시 로드
        player.STAT.Init();
    }

}
