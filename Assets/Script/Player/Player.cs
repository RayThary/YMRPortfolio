using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public float moveSpeed = 5.0f; // �̵� �ӵ� ������ ����
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
        // ���� �� ���� �Է��� �����ɴϴ�.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // �Է¿� ���� �̵� ���͸� ����ϴ�.
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f);

        // �̵� ������ ���̸� 1�� ����ȭ�ϰ� �ӵ��� ���մϴ�.
        moveDirection.Normalize();
        moveVelocity = moveDirection * moveSpeed;

        // �̵��� �����մϴ�.
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
