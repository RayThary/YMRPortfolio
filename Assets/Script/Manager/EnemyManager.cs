using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Stage nowStage;//현재스테이지를찾아주는것
    private Transform[] childTrs;
    public Transform EnemyParent;

    [SerializeField]
    private Stage[] stages;
    private bool stageChange = true;
    private int stageindex;
    public CardManager cardManager;

    void Start()
    {
        childTrs = transform.GetComponentsInChildren<Transform>();
        for(int i = 0; i <stages.Length; i++)
        {
            stages[i].Init();
        }
        stageindex = 0;
        nowStage = stages[0];
        //spawnEnemy();
        nowStage.spawnEnemy(EnemyParent);
        cardManager.ViewCards();
    }



    //적이 모두 비활성화되면 true를 리턴해야하는데 SwordNav가 비활성화 되서 일단은 false만 리턴함
    public bool EnemyClear()
    {
        for(int i = 0; i < nowStage.enemyList.Count; i++)
        {
            if (nowStage.enemyList[i].activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        if(EnemyClear())
        {
            Debug.Log("다음 스테이지로");
        //    //카드 고르고 다음 라운드로
        //    cardManager.ViewCards();
        //    //스테이지 바꾸고 spawnEnemy
        //    stageindex++;
        //    nowStage = stages[stageindex];
        //    spawnEnemy();
        }
    }
}


[System.Serializable]
public class Stage
{
    //초기화 이 함수를 처음에 반드시 호출해줘야 함
    public void Init()
    {
        position = new  List<Transform>();
        for (int i = 0; i < map.transform.childCount; i++)
            position.Add(map.transform.GetChild(i).transform);
        enemyList = new List<GameObject> ();
    }

    //맵 이 맵오브젝트 아래에 transform으로 enemy가 스폰될 position이 결정됨
    [SerializeField]
    private GameObject map;
    //map 자식으로 있는 transform으로 받은 스폰될 수 있는 위치들
    private List<Transform> position;
    //지금 이 스테이지에 스폰되있는 enemy들
    public List<GameObject> enemyList;
    //이 맵에 스폰될 enemy의 최소숫자
    [SerializeField]
    private int min;
    //이 맵에 스폰될 enemy의 최대숫자
    [SerializeField]
    private int max;
    //이 맵에 스폰될 유닛과 그 확률
    [SerializeField]
    private EnemyProbability[] enemyProbabilities;
    //토큰 == 확률 

    public void spawnEnemy(Transform parent)
    {
        //몇마리 스폰될지 정하고
        int enemyCount = Random.Range(min, max);
        //이미 스폰된 에네미들은 (아마 다시시작할때 리스트에 적이 남아있을테니)
        //초기화해서 없애주고 Clear만 해주면 어처피 유닛이 죽을때 PoolingManager를 사용해서 문제없음
        enemyList.Clear();

        //적들이 나올 수 있는 위치들을 정해주고
        List<int> enemyPositionList = new List<int>();
        //enemy가 나올 위치를 중복이 없도록
        for (int count = 0; count < enemyCount; count++)
        {
            int currentNumber = Random.Range(0, max);

            while (enemyPositionList.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, max);
            }
            enemyPositionList.Add(currentNumber);
        }

        //무슨 적이 나올지 담을 변수
        GameObject enemy = null;
        //모든 enemy의 token(기회)를 다 합침
        int allToken = enemyProbabilities.Select(enemyProbabilities => enemyProbabilities.token).Sum();

        //나와야할 적수의 수만큼 반복
        for (int i = 0; i < enemyCount; i++)
        {
            int type = Random.Range(0, allToken + 1);
            for(int j = 0; j < enemyProbabilities.Length; j++)
            {
                //첫번째 친구의 기회보다 높으면 다음친구에게 기회가 넘어감
                if(type > enemyProbabilities[j].token)
                    type -= enemyProbabilities[j].token;
                //낮다면 그친구가 나오는거임
                else
                {
                    enemy = PollingManager.Instance.CreateObject(enemyProbabilities[j].enemyType, parent);
                    break;
                }
            }
            //생성된 적을 list에 추가
            enemyList.Add(enemy);
            //위치도 바꿔주고
            Debug.Log(position[enemyPositionList[i]].position);
            enemy.transform.position = position[enemyPositionList[i]].position;
        }
    }
}

[System.Serializable]
public class EnemyProbability
{
    [Header("이 객체가")]
    public PollingManager.ePoolingObject enemyType;
    [Header("이 만큼 기회가 있음")]
    public int token;
}