using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public float moveSpeed = 5.0f; // 이동 속도 조절용 변수
    float horizontalInput = 0;
    float verticalInput = 0;
    Vector3 moveVelocity;

    public Weapon weapon;
    public Transform r_weapon;
    public Transform objectParent;

    Ray ray;
    RaycastHit hit;

    public Image spaceImage;
    public float spaceCooltime = 3;
    public float spaceDis;
    public float travel = 0.1f;
    private float spaceTimer;
    private bool canMove = true;


    protected new void Start()
    {
        base.Start();
        weapon = new TestGun(this, r_weapon, r_weapon, 0, 1, objectParent);
    }


    private void Update()
    {
        // 수평 및 수직 입력을 가져옵니다.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // 입력에 따라 이동 벡터를 만듭니다.
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f);

        // 이동 벡터의 길이를 1로 정규화하고 속도를 곱합니다.
        moveDirection.Normalize();
        moveVelocity = moveDirection * moveSpeed;

        // 이동을 적용합니다.
        if(canMove)
            transform.Translate(moveVelocity * Time.deltaTime);


        


        if(Input.GetKeyDown(KeyCode.Space))
        {
            Space();
        }

        if(spaceTimer >= 0)
        {
            spaceTimer -= Time.deltaTime;
            spaceImage.fillAmount = spaceTimer / spaceCooltime;
        }

        weapon.Direction_Calculation_Screen(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
    }

    public void Space()
    {
        if(moveVelocity != Vector3.zero && spaceTimer < 0)
        {
            StartCoroutine(SpaceCoroutine(travel, moveVelocity));
            spaceTimer = spaceCooltime;
        }
    }

    private IEnumerator SpaceCoroutine(float t, Vector3 velocity)
    {
        float timer = 0;
        canMove = false;
        GetComponent<Collider>().enabled = false;
        while(true)
        {
            transform.Translate(velocity * spaceDis * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer > t)
                break;
            yield return null;
        }
        canMove = true;
        GetComponent<Collider>().enabled = true;
    }
}
