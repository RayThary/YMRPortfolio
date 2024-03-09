using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingPatten : MonoBehaviour
{
    public enum BombingType
    {
        Horizontal,
        Vertical,
        LeftDiagonal, //���������� �����ʾƷ��� �׾����´밢��
        RightDiagonal,//������������ ���ʾƷ��� �׾����´밢��
    }
    [SerializeField] private BombingType bType;
    [SerializeField] private float bombingTime = 1;
    private float bombingTimer = 0.0f;
    [SerializeField] private float bombingIntervalTimer = 0.2f;
    [SerializeField] private bool bombingStartCheck = false;//field�������� ��������

    private Transform centerTrs;

    private bool startLeft = false;

    private Vector3 startPosVec;
    private Vector3 centerPosVec;
    private Vector3 endPosVec;

    private Vector3 targetPos;//��ǥ����
    private List<Vector3> targetVec = new List<Vector3>();

    LineRenderer line;

    private bool bombingStart = false;

    private DangerZone_LineRenderer lineDangerZone;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        lineDangerZone = GetComponent<DangerZone_LineRenderer>();
        lineDangerZone.SetTime(bombingTime);

        lineDangerZone.enabled = false;

        centerTrs = GameManager.instance.GetPlayerTransform;
    }


    void Update()
    {
        centerPostion();
        bombingSpawn();
    }

    private void centerPostion()
    {
        if (bombingStartCheck)
        {
            centerPosVec = centerTrs.position;
            int x = Mathf.RoundToInt(centerPosVec.x);
            int z = Mathf.RoundToInt(centerPosVec.z);

            centerPosVec = new Vector3(x, 0.1f, z);
            if (bType == BombingType.Horizontal)
            {
                startPosVec = new Vector3(centerPosVec.x - 4.5f, centerPosVec.y, centerPosVec.z);
                endPosVec = new Vector3(centerPosVec.x + 4.5f, centerPosVec.y, centerPosVec.z);
                line.SetPosition(0, startPosVec);
                line.SetPosition(1, endPosVec);
                if (startLeft)
                {
                    targetPos = startPosVec;
                    targetPos.x -= 0.5f;
                }
                else
                {
                    targetPos = endPosVec;
                    targetPos.x += 0.5f;
                }
            }
            else if (bType == BombingType.Vertical)
            {
                startPosVec = new Vector3(centerPosVec.x, centerPosVec.y, centerPosVec.z + 4.5f);
                endPosVec = new Vector3(centerPosVec.x, centerPosVec.y, centerPosVec.z - 4.5f);
                line.SetPosition(0, startPosVec);
                line.SetPosition(1, endPosVec);

                if (startLeft)
                {
                    //������ �Ʒ���
                    targetPos = startPosVec;
                    targetPos.z -= 0.5f;
                }
                else
                {
                    //�Ʒ����� ����
                    targetPos = endPosVec;
                    targetPos.z += 0.5f;
                }
            }
            else if (bType == BombingType.LeftDiagonal)
            {
                startPosVec = new Vector3(centerPosVec.x - 4.5f, centerPosVec.y, centerPosVec.z + 4.5f);
                endPosVec = new Vector3(centerPosVec.x + 4.5f, centerPosVec.y, centerPosVec.z - 4.5f);
                line.SetPosition(0, startPosVec);
                line.SetPosition(1, endPosVec);
                if (startLeft)
                {
                    targetPos = startPosVec;
                    targetPos.x += 0.5f;
                    targetPos.z -= 0.5f;
                }
                else
                {
                    targetPos = endPosVec;
                    targetPos.x -= 0.5f;
                    targetPos.z += 0.5f;
                }
            }
            else if (bType == BombingType.RightDiagonal)
            {
                startPosVec = new Vector3(centerPosVec.x + 4.5f, centerPosVec.y, centerPosVec.z + 4.5f);
                endPosVec = new Vector3(centerPosVec.x - 4.5f, centerPosVec.y, centerPosVec.z - 4.5f);
                line.SetPosition(0, startPosVec);
                line.SetPosition(1, endPosVec);

                if (startLeft)
                {
                    targetPos = endPosVec;
                    targetPos.x += 0.5f;
                    targetPos.z += 0.5f;
                }
                else
                {
                    targetPos = startPosVec;
                    targetPos.x -= 0.5f;
                    targetPos.z -= 0.5f;
                }
            }

            lineDangerZone.enabled = true;
            bombingStartCheck = false;
            bombingStart = true;
        }

    }

    private void bombingSpawn()
    {

        if (bombingStart && line.enabled == false)
        {
            targetVec.Clear();

            

            StartCoroutine(bombing());
            bombingStart = false;
        }
    }

    //private void 
    IEnumerator bombing()
    {
        if (bType == BombingType.Horizontal)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject obj = null;
                targetVec.Add(targetPos);
                if (startLeft)
                {
                    targetPos += Vector3.right;
                }
                else
                {
                    targetPos += Vector3.left;
                }
                obj = PoolingManager.Instance.CreateObject("BlueBombing", GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = targetPos;
                yield return new WaitForSeconds(bombingIntervalTimer);
            }
        }
        else if (bType == BombingType.Vertical)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject obj = null;
                targetVec.Add(targetPos);
                if (startLeft)
                {
                    targetPos += Vector3.back;
                }
                else
                {
                    targetPos += Vector3.forward;
                }
                obj = PoolingManager.Instance.CreateObject("BlueBombing", GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = targetVec[i];
                yield return new WaitForSeconds(bombingIntervalTimer);
            }
        }
        else if (bType == BombingType.LeftDiagonal)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject obj = null;
                targetVec.Add(targetPos);
                if (startLeft)
                {
                    targetPos += new Vector3(+1, 0, -1);
                }
                else
                {
                    targetPos += new Vector3(-1, 0, +1);
                }
                obj = PoolingManager.Instance.CreateObject("BlueBombing", GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = targetVec[i];
                yield return new WaitForSeconds(bombingIntervalTimer);
            }
        }
        else if (bType == BombingType.RightDiagonal)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject obj = null;
                targetVec.Add(targetPos);
                if (startLeft)
                {
                    targetPos += new Vector3(+1, 0, +1);
                }
                else
                {
                    targetPos += new Vector3(-1, 0, -1);
                }
                obj = PoolingManager.Instance.CreateObject("BlueBombing", GameManager.instance.GetEnemyAttackObjectPatten);
                obj.transform.position = targetVec[i];
                yield return new WaitForSeconds(bombingIntervalTimer);
            }
        }

    }



}
