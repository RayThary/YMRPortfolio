using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JinBoss : Unit
{
    public Player player;

    Launcher turret;
    Launcher ringFire;
    Launcher rampage;
    Launcher bounceLauncher;
    public Transform turretTr;
    public Transform turretMz;


    Launcher cannon;
    public Transform cannonTr;
    public Transform cannonMz;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        turret = new Launcher(this, turretTr, turretMz, 0, 0.1f, null);
        turret.BulletPool = PoolingManager.ePoolingObject.JinBossNoamlBullet;

        ringFire = new Launcher(this, turretTr, turretMz, 0, 0, null);
        ringFire.BulletPool = PoolingManager.ePoolingObject.JinBossNoamlBullet;

        rampage = new Launcher(this, turretTr, turretMz, 30, 0, null);
        rampage.BulletPool = PoolingManager.ePoolingObject.JinBossNoamlBullet;

        bounceLauncher = new Launcher(this, turretTr, turretMz, 0, 0, null);
        bounceLauncher.BulletPool = PoolingManager.ePoolingObject.JinBossBounceBullet;

        cannon = new Launcher(this, cannonTr, cannonMz, 0, 0, null);
        cannon.BulletPool = PoolingManager.ePoolingObject.JinBossCannonBullet;

        StartCoroutine(PattonChoice());
        //Patton_1();
        //Patton_2();
        //Patton_3();
    }

    private IEnumerator PattonChoice()
    {
        while(stat.HP > 0)
        {
            FindPatton();
            yield return new WaitForSeconds(5);
        }
    }

    private void FindPatton()
    {
        int[] ints = new int[3];
        if (Vector3.Distance(player.transform.position, transform.position) > 13)
        {
            ints[0] = 10;
            ints[1] = 20;
            ints[2] = 10;
        }
        else if(Vector3.Distance(player.transform.position, transform.position) > 5)
        {
            ints[0] = 20;
            ints[1] = 10;
            ints[2] = 10;
        }
        else
        {
            ints[0] = 10;
            ints[1] = 10;
            ints[2] = 20;
        }
        int select = Probability(ints);
        switch(select)
        {
            case 0:
                Patton_1();
                break;
            case 1:
                Patton_2();
                break;
            case 2:
                Patton_3();
                break;
        }
    }

    private int Probability(int[] pro)
    {
        int max = pro.Sum();
        int select = Random.Range(0, max);
        for(int i = 0; i < pro.Length; i++)
        {
            if (pro[i] < select)
                return i;
        }
        return 0;
    }

    //1 난사 + 링
    public void Patton_1()
    {
        RampagePatton(5);
        RingFirePatton(5, 1.5f, ringFire);
    }
    //캐논 3발 발사 후 돌진 + 터렛 회전
    public void Patton_2()
    {
        StartCoroutine(Patton2Coroutine());
    }
    private IEnumerator Patton2Coroutine()
    {
        CannonBurstPatton(5, 1f, 0.2f);
        yield return new WaitForSeconds(5);
        MovePatton(5);
        TurretRotatePatton(5);
    }
    //튕기는 탄 주위로 발사
    public void Patton_3()
    {
        RingFirePatton(5, 3f, bounceLauncher);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePatton(float timer)
    {
        StartCoroutine(Aim(transform, timer, player.transform, 0.1f));
        StartCoroutine(Move(timer));
    }

    private IEnumerator Move(float timer)
    {
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        while (timer > 0)
        {
            rigidbody.AddForce(transform.forward * 50);
            timer -= Time.deltaTime;
            yield return null;
        }
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;
    }

    //플레이어를 향해 난사를 하는 패턴
    public void RampagePatton(float timer)
    {
        StartCoroutine(Aim(turretTr, timer, player.transform, 0.1f));
        StartCoroutine(Rampage(rampage, turretTr, 0.1f, timer));
    }

    //무슨 총으로, 총의 트랜스폼, 얼마나 빨리 쏠지, 얼마나 탄퍼짐이 심한지, 상대가 누군지, 몇발 쏠지
    private IEnumerator Rampage(Launcher launcher, Transform tf, float firerate, float timer)
    {
        float t = 0; 
        while(true)
        {
            launcher.angle = tf.eulerAngles.y;
            launcher.Fire();
            yield return new WaitForSeconds(firerate);
            t += firerate;
            if (t >= timer)
                break;
        }
    }

    //360도 방향으로 동시에 발사 하는 패턴
    //3번정도 발사
    public void RingFirePatton(float timer, float timeInterval, Launcher launcher )
    {
        StartCoroutine(RingFire(10, timer, timeInterval, launcher));
    }

    //탄과 탄 사이의 거리 (각도), 몇초후에 다음 발사를 할지, 몇번 발사를 할지, 총
    private IEnumerator RingFire(float Interval, float timer, float timeInterval, Launcher launcher)
    {
        float angle = 0;
        while(timer > 0)
        {
            while (true)
            {
                launcher.Fire();
                angle += Interval;
                launcher.angle = angle;
                if (angle > 360)
                {
                    break;
                }
            }
            angle = 0;
            yield return new WaitForSeconds(timeInterval);
            timer -= timeInterval;
        }
    }

    //터렛을 회전시키면서 발사 하는 패턴
    //상대방을 향해 발사하면서 회전도 하면서 뭔가 있는게 좋을거같은데
    public void TurretRotatePatton(float timer)
    {
        StartCoroutine(TurretRotate(timer));
    }

    private IEnumerator TurretRotate(float timer)
    {
        float t = 0;
        while (t < timer)
        {
            t += Time.deltaTime;

            turret.angle += Time.deltaTime * 90;
            turretTr.eulerAngles = new Vector3(0, turret.angle, 0);
            turret.Fire();

            yield return null;
        }
    }

    //상대를 보고 캐논 포 3발 발사
    //1초동안 조준 하면서 머리가 상대를 쫓아가고
    //멈춘 후 0.2초 후 발사
    //3회 반복
    public void CannonBurstPatton(float timer, float aim, float timerInterval)
    {
        StartCoroutine(CannonBurst(timer, aim, timerInterval));
    }
     
    //캐논 패턴 코루틴
    //몇초동안 패턴인지, 발사 사이의 간격
    private IEnumerator CannonBurst(float  timer, float aim, float timerInterval)
    {
        while(timer > 0)
        {
            //조준
            StartCoroutine(Aim(cannonTr, 1, player.transform, 0.1f));
            yield return new WaitForSeconds(aim); //조준 1초 + 발사전의 시간 0.2초
            //발사
            cannon.angle = cannonTr.eulerAngles.y;
            cannon.Fire();
            yield return new WaitForSeconds(timerInterval);
            timer -= timerInterval + aim;
        }
    }

    //회전을 자연스럽게
    private IEnumerator Aim(Transform center, float timer, Transform target, float speed)
    {
        float f = 0;
        while (f < timer)
        {
            f += Time.deltaTime;

            center.eulerAngles += new Vector3(0, Rotate(10, target, center) * speed, 0);
            yield return null;
        }
    }
    //상대와의 각도 리턴
    private float Rotate(float max, Transform target, Transform center)
    {
        if (target == null)
            return 0;

        Vector3 dir = (target.position - center.position).normalized;
        // 내적(dot)을 통해 각도를 구함. (Acos로 나온 각도는 방향을 알 수가 없음)
        float dot = Vector3.Dot(center.forward, dir);
        if (dot < 1.0f)
        {
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            // 외적을 통해 각도의 방향을 판별.
            Vector3 cross = Vector3.Cross(center.forward, dir);
            // 외적 결과 값에 따라 각도 반영
            if (cross.y < 0)
            {
                angle = center.rotation.eulerAngles.z - Mathf.Min(max, angle);
            }
            else
            {
                angle = center.rotation.eulerAngles.z + Mathf.Min(max, angle);
            }
            return angle;
        }
        return 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Unit>().Hit(this, 2);
        }
    }
}
