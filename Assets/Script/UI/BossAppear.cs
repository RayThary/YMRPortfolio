using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAppear : MonoBehaviour
{
    private RectTransform rectTrs;
    
    [SerializeField] private float speed = 20;

    private bool stopCheck;

    void Start()
    {
        rectTrs = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {

        moveBackGround();


        // x 180 y -45 ¸ØÃâÁöÁ¡
    }

    private void moveBackGround()
    {
        if (stopCheck)
        {
            rectTrs.position += rectTrs.right * speed;
            
        }

        if (rectTrs.localPosition.x >=80 )
        {
            stopCheck = false;
        }
    }

    

}
