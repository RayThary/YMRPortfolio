using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PoolingManager;

public class JyBoss : Unit
{
    protected Animator animator;

    public Launcher[] launcher;
    public Transform[] muzzles;

    public Launcher circleLauncher;
    public Transform circleMuzzle;

    public int state = 0;

    private Coroutine stateCoroutine = null;
    public Transform objectParent;

    public bool b = false;

    public Tower tower;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        launcher = new Launcher[muzzles.Length];
        for (int i = 0; i < muzzles.Length; i++)
        {
            launcher[i] = new Launcher(this, muzzles[i], muzzles[i], 0, 1, objectParent);
        }
        circleLauncher = new Launcher(this, circleMuzzle, circleMuzzle, 0, 0.1f, objectParent);
        //Operation();
    }

    // Update is called once per frame
    void Update()
    {
        if(b)
        {
            b = false;
            state++;
            if (state > 6)
                state = 1;
            SetState();
        }
    }

    
    //자동 발사
    private IEnumerator OperationCoroutine()
    {
        for (int i = 0; i < launcher.Length; i++)
        {
            //launcher[i].FireRate = 1f;
            launcher[i].FireRate = 0f;
        }
        while (true)
        {
            for (int i = 0; i < launcher.Length; i++)
            {
                launcher[i].angle = muzzles[i].eulerAngles.y;
                launcher[i].Fire();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator LocationFire()
    {
        //circleLauncher.FireRate = 0.1f;
        circleLauncher.FireRate = 0f;
        while (true)
        {
            circleLauncher.angle += Time.deltaTime * 90;
            circleLauncher.Fire();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CircleFire()
    {
        circleLauncher.FireRate = 0f;
        while(true)
        {
            for(int i = 0; i < 36; i++)
            {
                circleLauncher.angle = i * 10;
                circleLauncher.Fire();
            }
            
            yield return new WaitForSeconds(1f);
        }
    }

    private void BigBullet()
    {
        Bullet b = PoolingManager.Instance.CreateObject(ePoolingObject.JyBossBigBullet, objectParent).GetComponent<Bullet>();
        b.transform.position = circleMuzzle.position;
        b.unit = this;
        b.transform.localEulerAngles = new Vector3(0, circleMuzzle.eulerAngles.y, 0);
        b.Straight();
    }

    private IEnumerator Scattering()
    {
        //circleLauncher.FireRate = 0.1f;
        circleLauncher.FireRate = 0;
        circleLauncher.Mistake = 30;
        float angle;
        while(true)
        {
            angle = a.WorldAngleCalculate(GameManager.instance.GetPlayerTransform.position, transform.position);

            circleLauncher.angle = angle;
            circleLauncher.Fire();

            yield return new WaitForSeconds(0.1f);
        }
    }

    List<Tower> towers = new List<Tower>();
    private void TowerSpawn()
    {
        towers.Add(Instantiate(tower));
        towers[towers.Count - 1].rate = 0.2f;
    }

    private IEnumerator Arc(Transform transform, float height)
    {
        float cos;
        float sin;
        float timer = 0;
        while(true)
        {
            timer += Time.deltaTime;
            cos = Mathf.Cos(timer);
            sin = Mathf.Sin(timer);
            transform.position += new Vector3(cos, sin, 0);

            yield return null;
        }
    }

    public void SetState()
    {
        if (stateCoroutine != null)
            StopCoroutine(stateCoroutine);
        switch (state)
        {
            case 1:
                //animator.SetTrigger("Attack");
                stateCoroutine = StartCoroutine(OperationCoroutine());
                break;
            case 2:
                //animator.SetTrigger("Attack");
                stateCoroutine = StartCoroutine(LocationFire());
                break;
            case 3:
                //animator.SetTrigger("Attack");
                BigBullet();
                break;
            case 4:
                //animator.SetTrigger("Attack");
                stateCoroutine = StartCoroutine(CircleFire());
                break;
            case 5:
                stateCoroutine = StartCoroutine(Scattering());
                break;
            case 6:
                TowerSpawn();
                break;
        }
    }
}
