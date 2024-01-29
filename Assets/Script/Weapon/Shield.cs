using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //�ε��� ������ �����ð� �Ŀ� �ٽ� ������� �ֵ���
    private List<Unit> units = new List<Unit>();
    //��ü ������ ����
    int all;
    //���� �ε���
    int index;
    //�÷��̾�κ����� �Ÿ�
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
