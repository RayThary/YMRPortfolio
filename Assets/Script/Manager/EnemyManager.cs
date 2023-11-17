using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Stage nowStage;//현재스테이지를찾아주는것
    private Transform[] childTrs;
    public Transform EnemyParent;
    private List<int> enemyList = new List<int>();
    private bool stageChange = true;

    

    public Stage[] stages;

    void Start()
    {
        childTrs = transform.GetComponentsInChildren<Transform>();
        nowStage = stages[0];
        findEnemyPos();
        spawnEnemy();
        FindMap();
        findEnemyPos();
    }

    private void FindMap()
    {
        Transform maps = GameObject.Find("Maps").transform;
        stages = new Stage[maps.childCount];
        for (int i = 0; i < stages.Length; i++)
        {
            if (stages.Length <= i)
                break;
            stages[i] = new Stage(5, 10);
            stages[i].map = maps.GetChild(i).gameObject;
        }
    }

    private void findEnemyPos()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            GameObject tf = GameObject.Find("Stage" + i.ToString());
            if (tf != null)
            {
                for (int j = 0; j < tf.transform.childCount; j++)
                {
                    stages[i].position.Add(tf.transform.GetChild(j));
                }
            }
        }
    }

    private void spawnEnemy()
    {
        if (stageChange == true)
        {
            int enemyCount = Random.Range(nowStage.min, nowStage.max);


            
            for (int count = 0; count < enemyCount; count++)
            {
                int currentNumber = Random.Range(0, nowStage.max);

                while (enemyList.Contains(currentNumber))
                {
                    currentNumber = Random.Range(0, nowStage.max);
                }
                enemyList.Add(currentNumber);

            }

            GameObject enemy = null;
            for (int i = 0; i < enemyCount; i++)
            {
                int type = Random.Range(0, 9);
                if (type <7)
                {
                    enemy = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.EnemySword, EnemyParent);
                }
                else if (type > 6)
                {
                    enemy = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.EnemyBow, EnemyParent);
                }

                enemy.transform.position = nowStage.position[enemyList[i]].position;
            }

            stageChange = false;
        }

    }


    void Update()
    {

    }
}


[System.Serializable]
public class Stage
{
    public Stage(int min, int max)
    {
        this.min = min;
        this.max = max;
        position = new List<Transform>();
    }

    public GameObject map;
    public List<Transform> position;
    public int min;
    public int max;
}