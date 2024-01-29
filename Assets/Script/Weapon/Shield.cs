using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //부딪힌 유닛은 일정시간 후에 다시 대미지를 주도록
    private List<Unit> units = new List<Unit>();
    //전체 방패의 갯수
    int all;
    //나의 인덱스
    int index;
    //플레이어로부터의 거리
    float distance;
    float timer = 0;
    float speed;
    Transform tf;

    public void Init(int all, int index, float distance, float speed, Transform transform)
    {
        this.all = all;
        this.index = index;
        this.distance = distance;
        this.speed = speed;
        tf = transform;
    }

    private void Update()
    {
        this.transform.position = tf.position + new Vector3(Mathf.Cos(((360 / all) * index + timer) * Mathf.Deg2Rad) * distance, 0.1f, Mathf.Sin(((360 / all) * index + timer) * Mathf.Deg2Rad) * distance);
        timer += Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Unit>() != null)
        {
            if(!units.Contains(other.GetComponent<Unit>()))
            {
                units.Add(other.GetComponent<Unit>());
                other.GetComponent<Unit>().Hit(GameManager.instance.GetPlayer, 3);
                StartCoroutine(TimerCoroutine(other.GetComponent<Unit>()));
            }
        }
    }

    private IEnumerator TimerCoroutine(Unit unit)
    {
        yield return new WaitForSeconds(1);
        units.Remove(unit);
    }
}
