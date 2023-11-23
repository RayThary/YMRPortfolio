using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private GameObject objMeteor;
    private SpriteRenderer spr;
    
    private float rangeSpeed = 0.5f;
    private float sX, sY;
    private float Ratio;
    private bool meteorSpawn = false;
    private GameObject shadow;

    void Start()
    {
        spr= GetComponentInChildren<SpriteRenderer>();
        shadow = transform.GetChild(0).gameObject;
    }

    
    void Update()
    {
        if (meteorSpawn)
        {
            return;
        }

        Ratio += Time.deltaTime * rangeSpeed;
        sX = Mathf.Lerp(0.2f, 0.1f, Ratio);
        sY = Mathf.Lerp(0.2f, 0.1f, Ratio);

        shadow.transform.localScale = new Vector3(sX, sY, 0.1f);
        if (sY <= 0.1f)
        {
            spr.color = Color.red;
            GameObject meteor = Instantiate(objMeteor, transform);
            meteor.transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            Invoke("shadowFalse", 1f);
            meteorSpawn = true;
        }
    }

    private void shadowFalse()
    {
        shadow.SetActive(false);
    }
}
