using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Stage nowStage;//���罺��������ã���ִ°�
    public Stage GetStage { get { return nowStage; } set { nowStage = value; } }
    private Transform[] childTrs;
    public Transform EnemyParent;

    [SerializeField]
    private Stage[] stages;
    public Stage[] Stages { get { return stages; } }
    private bool stageChange = true;
    private int stageindex;


    public void Init()
    {
        childTrs = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].Init(EnemyParent);
        }
    }

    //���� ��� ��Ȱ��ȭ�Ǹ� true�� �����ؾ��ϴµ� SwordNav�� ��Ȱ��ȭ �Ǽ� �ϴ��� false�� ������
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
}


[System.Serializable]
public class Stage
{
    private Transform EnemyParent;
    //�ʱ�ȭ �� �Լ��� ó���� �ݵ�� ȣ������� ��
    public void Init(Transform EnemyParent)
    {
        position = new  List<Transform>();
        this.EnemyParent = EnemyParent;
        for (int i = 0; i < map.transform.childCount; i++)
            position.Add(map.transform.GetChild(i).transform);
        enemyList = new List<GameObject> ();
    }

    //�� �� �ʿ�����Ʈ �Ʒ��� transform���� enemy�� ������ position�� ������
    [SerializeField]
    private GameObject map;
    public GameObject Map { get { return map; } }
    [SerializeField]
    private BoxCollider mapBoxCollder;
    public BoxCollider BoxCollider { get { return mapBoxCollder; } }
    [SerializeField]
    private Collider cameraCollder;
    public Collider CameraCollider { get { return cameraCollder; } }    
    //map �ڽ����� �ִ� transform���� ���� ������ �� �ִ� ��ġ��
    private List<Transform> position;
    //���� �� ���������� �������ִ� enemy��
    public List<GameObject> enemyList;
    //�� �ʿ� ������ enemy�� �ּҼ���
    [SerializeField]
    private int min;
    //�� �ʿ� ������ enemy�� �ִ����
    [SerializeField]
    private int max;
    //�� �ʿ� ������ ���ְ� �� Ȯ��
    [SerializeField]
    private EnemyProbability[] enemyProbabilities;

    public List<GameObject> ActiveEnemy()
    {
        if(enemyList == null)
            return null;
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i <  enemyList.Count; i++)
        {
            if (enemyList[i].activeSelf)
                list.Add(enemyList[i]);
        }
        return list;
    }

    public void spawnEnemy()
    {
        //��� �������� ���ϰ�
        int enemyCount = Random.Range(min, max);
        //�̹� ������ ���׹̵��� (�Ƹ� �ٽý����Ҷ� ����Ʈ�� ���� ���������״�)
        //�ʱ�ȭ�ؼ� �����ְ� Clear�� ���ָ� ��ó�� ������ ������ PoolingManager�� ����ؼ� ��������
        enemyList.Clear();
        //������ ���� �� �ִ� ��ġ���� �����ְ�
        List<int> enemyPositionList = new List<int>();
        //enemy�� ���� ��ġ�� �ߺ��� ������
        for (int count = 0; count < enemyCount; count++)
        {
            int currentNumber = Random.Range(0, max);

            while (enemyPositionList.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, max);
            }
            enemyPositionList.Add(currentNumber);
        }

        //���� ���� ������ ���� ����
        GameObject enemy = null;
        //��� enemy�� token(��ȸ)�� �� ��ħ
        int allToken = enemyProbabilities.Select(enemyProbabilities => enemyProbabilities.token).Sum();

        //���;��� ������ ����ŭ �ݺ�
        for (int i = 0; i < enemyCount; i++)
        {
            int type = Random.Range(0, allToken + 1);
            for(int j = 0; j < enemyProbabilities.Length; j++)
            {
                //ù��° ģ���� ��ȸ���� ������ ����ģ������ ��ȸ�� �Ѿ
                if(type > enemyProbabilities[j].token)
                    type -= enemyProbabilities[j].token;
                //���ٸ� ��ģ���� �����°���
                else
                {
                    enemy = PoolingManager.Instance.CreateObject(enemyProbabilities[j].enemyType, EnemyParent);
                    break;
                }
            }
            //������ ���� list�� �߰�
            enemyList.Add(enemy);
            //��ġ�� �ٲ��ְ�

            enemy.transform.position = position[enemyPositionList[i]].position;
        }
    }
}

[System.Serializable]
public class EnemyProbability
{
    [Header("�� ��ü��")]
    public PoolingManager.ePoolingObject enemyType;
    [Header("�� ��ŭ ��ȸ�� ����")]
    public int token;
}