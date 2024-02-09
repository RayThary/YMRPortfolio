using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroundPatten : MonoBehaviour
{
    [SerializeField] private GameObject nowMap;
    private List<GameObject> maps = new List<GameObject>();

    [SerializeField] private float HrizontalSetUpGroundTime = 1;
    [SerializeField] private bool Hrizontal;

    [SerializeField] private float ViticalSetUpGroundTime = 1;
    [SerializeField] private bool Vitical;


    [SerializeField] private float UpSetUpGroundTime = 1;
    [SerializeField] private bool Up;
    [SerializeField] private float RightSetUpGroundTime = 1;
    [SerializeField] private bool Right;


    [SerializeField] private float CloseWallSetUpGroundTime = 1;



    private List<GameObject> mapUnder = new List<GameObject>();
    private List<GameObject> mapUnderTrs = new List<GameObject>();


    public enum PattenName
    {
        HrizontalAndVerticalPatten,
        WavePatten,
        OpenWallGroundPatten
    }

    [SerializeField] private PattenName pattenName;

    [SerializeField] private bool pattenStart = false;


    private bool mapChange = true;


    private Transform playerTrs;

    void Start()
    {
        playerTrs = GameManager.instance.GetPlayerTransform;

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
                if (Hrizontal)
                {
                    horizontalPatten();
                }
                if (Vitical)
                {
                    verticalPatten();
                }
                    
                pattenStart = false;
            }

            if (pattenName == PattenName.WavePatten)
            {
                if (Right)
                {
                    StartCoroutine(wavePattenRightOrLeft());
                }
                if (Up)
                {
                    StartCoroutine(wavePattenUpOrDown());
                }
                pattenStart = false;
            }

            if (pattenName == PattenName.OpenWallGroundPatten)
            {
                closeWallGroundPatten(3);//반지름의크기 ex) 4 = 8*8 로만들어진벽
                pattenStart = false;
            }
        }
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
            obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundPushObj, GameManager.instance.GetEnemyAttackObjectPatten);
            obj.transform.position = mapUnderTrs[i].transform.position;
            DangerZone danger = obj.GetComponentInChildren<DangerZone>();
            danger.SetTime(ViticalSetUpGroundTime);
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
            
            obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundPushObj, GameManager.instance.GetEnemyAttackObjectPatten);
            obj.transform.position = mapUnderTrs[i].transform.position;
            DangerZone danger = obj.GetComponentInChildren<DangerZone>();
            danger.SetTime(HrizontalSetUpGroundTime);
        }

    }

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
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundPushObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
                DangerZone danger = obj.GetComponentInChildren<DangerZone>();
                danger.SetTime(RightSetUpGroundTime);
            }
            yield return new WaitForSeconds(1.8f);
        }

        for (int i = 25; i > 0; i--)
        {
            GameObject obj;
            List<GameObject> objTrs = new List<GameObject>();
            objTrs.Clear();

            objTrs = mapUnder.FindAll((x) => x.name.Contains($"{{{i},") == true);
            for (int j = 0; j < objTrs.Count; j++)
            {
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundPushObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
                DangerZone danger = obj.GetComponentInChildren<DangerZone>();
                danger.SetTime(RightSetUpGroundTime);//오른쪽에서 시작했다 왼쪽으로갈때 같은시간을쓰는중 나중에 분리해줄필요있을지도?
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
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundPushObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
                DangerZone danger = obj.GetComponentInChildren<DangerZone>();
                danger.SetTime(UpSetUpGroundTime);
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
                obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundPushObj, GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = objTrs[j].transform.position;
                DangerZone danger = obj.GetComponentInChildren<DangerZone>();
                danger.SetTime(UpSetUpGroundTime);
            }
            yield return new WaitForSeconds(1.8f);
        }
    }

    private void closeWallGroundPatten(int _value)
    {
        mapUnderTrs.Clear();


        Vector3 playerPos = playerTrs.position;
        int x = Mathf.RoundToInt(playerPos.x);
        int z = Mathf.RoundToInt(playerPos.z);


        int rightX = Mathf.Clamp(x + _value, 1, 28);
        int leftX = Mathf.Clamp(x - _value, 1, 28);


        int upZ = Mathf.Clamp(z + _value, 1, 28);
        int downZ = Mathf.Clamp(z - _value, 1, 28);

        Vector3 rightVec = mapUnder.Find((x) => x.name.Contains($"{{{rightX},{upZ}}}") == true).transform.position;
        Vector3 leftVec = mapUnder.Find((x) => x.name.Contains($"{{{leftX},{downZ}}}") == true).transform.position;
        Vector3 upVec = mapUnder.Find((x) => x.name.Contains($"{{{leftX},{upZ}}}") == true).transform.position;
        Vector3 downVec = mapUnder.Find((x) => x.name.Contains($"{{{rightX},{downZ}}}") == true).transform.position;

        List<Vector3> spawnTrs = new List<Vector3>();

        for (int i = 0; i < _value * 2; i++)
        {


            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        if (spawnTrs.Exists((x) => x == rightVec) == false)
                        {
                            spawnTrs.Add(rightVec);
                        }
                        break;
                    case 1:
                        if (spawnTrs.Exists((x) => x == leftVec) == false)
                        {
                            spawnTrs.Add(leftVec);
                        }
                        break;
                    case 2:
                        if (spawnTrs.Exists((x) => x == upVec) == false)
                        {
                            spawnTrs.Add(upVec);
                        }
                        break;
                    case 3:
                        if (spawnTrs.Exists((x) => x == downVec) == false)
                        {
                            spawnTrs.Add(downVec);
                        }
                        break;
                }
            }

            if (rightVec.z > 1)
            {
                rightVec.z -= 1;
            }

            if (leftVec.z < 28)
            {
                leftVec.z += 1;
            }

            if (upVec.x < 28)
            {
                upVec.x += 1;
            }

            if (downVec.x > 1)
            {
                downVec.x -= 1;
            }
        }

        if (x < _value)
        {
            spawnTrs = spawnTrs.FindAll((x) => x.x > rightX == false);
        }


        if (z < _value)
        {
            spawnTrs = spawnTrs.FindAll((z) => z.z > upZ == false);
        }


        if ( x > 28 - _value)
        {
            spawnTrs = spawnTrs.FindAll((x) => x.x < leftX == false);
        }

        if (z > 28 - _value)
        {
            spawnTrs = spawnTrs.FindAll((x) => x.z < downZ == false);
        }


        for (int i = 0; i < spawnTrs.Count; i++)
        {
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.UpGroundPushObj, GameManager.instance.GetEnemyAttackObjectPatten);
            obj.transform.position = spawnTrs[i];
            DangerZone danger = obj.GetComponentInChildren<DangerZone>();
            danger.SetTime(CloseWallSetUpGroundTime);
        }

    }

}
