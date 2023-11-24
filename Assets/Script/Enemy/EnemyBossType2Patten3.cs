using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType2Patten3 : MonoBehaviour
{
    [SerializeField] private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
    }
}
