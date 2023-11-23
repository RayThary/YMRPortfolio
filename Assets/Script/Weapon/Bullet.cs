using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Unit unit;
    public float damage;
    public float timer;
    public float speed; 

    private Coroutine straight = null;
    private Coroutine t = null;

    public void Straight()
    {
        if(straight != null)
        {
            StopCoroutine(straight);
        }
        straight = StartCoroutine(StraightC());
        TimerStart();
    }

    public void TimerStart()
    {
        if (t != null)
        {
            StopCoroutine(t);
        }
        t = StartCoroutine(OffTimer());
    }

    private IEnumerator StraightC()
    {
        while(true)
        {
            transform.Translate(transform.forward * speed *  Time.deltaTime, Space.World);

            yield return null;
        }
    }

    private IEnumerator OffTimer()
    {
        yield return new WaitForSeconds(timer);
        t = null;
        if(straight != null)
        {
            StopCoroutine(straight);
        }
        gameObject.SetActive(false);
    }

    public virtual void Judgment(Collider other)
    {
        if (other.GetComponent<Unit>() != null && unit != null)
        {
            other.GetComponent<Unit>().Hit(unit, damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "test")
        {
            return;
        }
        if (straight != null)
        {
            StopCoroutine(straight);
        }

        Judgment(other);

        PollingManager.Instance.RemovePoolingObject(gameObject);
    }
}
