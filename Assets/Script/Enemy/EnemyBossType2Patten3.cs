using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType2Patten3 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int bulletSpawnCount = 10;
    private int bulletCount = 0;
    private Launcher[] launcher;
    private Coroutine operationCoroutine = null;
    [SerializeField] private Transform bulletParent;

    [SerializeField] private Transform[] muzzles;
    private Transform sprTrs;
    [SerializeField] private Unit boss;
    public float rate;
    void Start()
    {
        SpriteRenderer spr = GetComponentInChildren<SpriteRenderer>();
        sprTrs = spr.GetComponent<Transform>();

        launcher = new Launcher[muzzles.Length];
        bulletParent = FindObjectOfType<GameManager>().transform;
        for (int i = 0; i < muzzles.Length; i++)
        {
            launcher[i] = new Launcher(boss, sprTrs, muzzles[i], 0, rate, bulletParent);
        }

        Operation();
    }

    // Update is called once per frame
    void Update()
    {
        bigBulletMove();
    }
    private void bigBulletMove()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
        sprTrs.transform.Rotate(new Vector3(0, 0, -1) * rotateSpeed * Time.deltaTime);
    }

    public void Operation()
    {
        operationCoroutine = StartCoroutine(OperationCoroutine());
    }
    //작동 중지
    public void OperationStop()
    {
        if (operationCoroutine != null)
            StopCoroutine(operationCoroutine);
    }
    //자동 발사
    private IEnumerator OperationCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < launcher.Length; i++)
            {
                launcher[i].angle = muzzles[i].eulerAngles.y;
                launcher[i].Fire();
            }
            yield return null;
        }
    }
}
