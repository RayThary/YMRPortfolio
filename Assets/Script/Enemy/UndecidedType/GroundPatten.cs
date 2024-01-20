using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroundPatten : MonoBehaviour
{
    [SerializeField] private GameObject nowMap;
    private List<GameObject> maps = new List<GameObject>();
    
    private List<GameObject> mapUnder = new List<GameObject>();
    private List<GameObject> mapUnderTrs = new List<GameObject>();


    public enum PattenName
    {
        HrizontalAndVerticalPatten,
        WavePatten,

    }

    [SerializeField] private PattenName pattenName;

    [SerializeField] private bool pattenStart = false;


    private bool mapChange = true;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        nowMapCheck();
        groundPatten();
    }

    private void nowMapCheck()
    {
        if (mapChange)
        {
            //맵이바뀌면 겟스테이지로 현재스테이지를 체크해줘서 리턴을해줘야함 *만들예정
            //nowMpa = maps[GameManager.instance.nowStage()]
            int childCount = nowMap.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                maps.Add(nowMap.transform.GetChild(i).gameObject);
                if (i == childCount - 1)
                {
                    mapUnder = maps.FindAll((x) => x.name.Contains("Tile") == true && x.transform.childCount == 0);
                    mapChange = false;
                }
            }
        }
    }

    private void groundPatten()
    {
        if (pattenStart)
        {
            if (pattenName == PattenName.HrizontalAndVerticalPatten)
            {
                StartCoroutine(hrizontalAndverticalPatten());
                pattenStart = false;
            }
            
            if( pattenName == PattenName.WavePatten)
            {
                StartCoroutine(wavePattenRightOrLeft());
                StartCoroutine(wavePattenUpOrDown());
                pattenStart = false;
            }
        }


    }


    IEnumerator hrizontalAndverticalPatten()
    {
        verticalPatten();
        yield return new WaitForSeconds(5);
        horizontalPatten();
    }
    private void verticalPatten()
    {
        mapUnderTrs.Clear();
        int add = 3;
        int limitLine = 28;
        int findNum = 1;
        string findText = string.Empty;
        while (true)
        {
            findText = $"{{{findNum},";
            mapUnderTrs.AddRange(mapUnder.FindAll((x) => x.name.Contains(findText) == true));
            findNum += add;
            if (findNum > limitLine)
            {
                break;
            }
        }

        for (int i = 0; i < mapUnderTrs.Count; i++)
        {
            GameObject obj;
            obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
            obj.transform.position = mapUnderTrs[i].transform.position;
        }

    }

    private void horizontalPatten()
    {
        mapUnderTrs.Clear();
        int add = 3;
        int limitLine = 28;
        int findNum = 1;
        string findText = string.Empty;
        while (true)
        {
            findText = $",{findNum}}}";
            mapUnderTrs.AddRange(mapUnder.FindAll((x) => x.name.Contains(findText) == true));
            findNum += add;
            if (findNum > limitLine)
            {
                break;
            }
        }

        for (int i = 0; i < mapUnderTrs.Count; i++)
        {
            GameObject obj;
            obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
            obj.transform.position = mapUnderTrs[i].transform.position;
        }

    }

    //private void wavePatten()
    //{
    //    mapUnderTrs.Clear();
    //    for (int i = 1; i < 29; i++)
    //    {
    //        GameObject obj;
    //        List<GameObject> objTrs = new List<GameObject>();
    //        objTrs.Clear();

    //        objTrs = mapUnder.FindAll((x) => x.name.Contains($"{{{i},") == true);
    //        for(int j = 0; j < objTrs.Count;j++)
    //        {
    //            obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
    //            obj.transform.position = objTrs[j].transform.position;
    //        }
            
    //    }
    //}

    IEnumerator wavePattenRightOrLeft()
    {
        mapUnderTrs.Clear();
        for (int i = 1; i < 25; i++)
        {
            GameObject obj;
            List<GameObject> objTrs = new List<GameObject>();
            objTrs.Clear();

            objTrs = mapUnder.FindAll((x) => x.name.Contains($"{{{i},") == true);
            for (int j = 0; j < objTrs.Count; j++)
            {
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
            }
            yield return new WaitForSeconds(1.8f);
        }
        
        for(int i = 25; i > 0; i--)
        {
            GameObject obj;
            List<GameObject> objTrs = new List<GameObject>();
            objTrs.Clear();

            objTrs = mapUnder.FindAll((x) => x.name.Contains($"{{{i},") == true);
            for (int j = 0; j < objTrs.Count; j++)
            {
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
            }
            yield return new WaitForSeconds(1.8f);
        }

    }

    IEnumerator wavePattenUpOrDown()
    {
        mapUnderTrs.Clear();
        for (int i = 1; i < 25; i++)
        {
            GameObject obj;
            List<GameObject> objTrs = new List<GameObject>();
            objTrs.Clear();

            objTrs = mapUnder.FindAll((x) => x.name.Contains($",{i}}}") == true);
            for (int j = 0; j < objTrs.Count; j++)
            {
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
            }
            yield return new WaitForSeconds(1.8f);
        }

        for (int i = 25; i > 0; i--)
        {
            GameObject obj;
            List<GameObject> objTrs = new List<GameObject>();
            objTrs.Clear();

            objTrs = mapUnder.FindAll((x) => x.name.Contains($",{i}}}") == true);
            for (int j = 0; j < objTrs.Count; j++)
            {
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
            }
            yield return new WaitForSeconds(1.8f);
        }
    }
}
