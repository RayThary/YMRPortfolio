using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public float moveSpeed = 5.0f; // �̵� �ӵ� ������ ����
    public float moveAnimatorSpeed;
    float horizontalInput = 0;
    float verticalInput = 0;
    Vector3 moveVelocity;

    public Weapon weapon;
    public Transform r_weapon;
    public Transform objectParent;

    Ray ray;
    RaycastHit hit;

    //�뽬�� ��Ÿ���� �˷��� �̹���
    public Image spaceImage;
    //�뽬�� ��Ÿ��
    public float spaceCooltime = 3;
    //�뽬�� ������ �Ÿ�
    public float spaceDis;
    //�뽬�� �̵��ð��� �����ð�
    public float travel = 0.1f;
    //�ð��� �� ����
    private float spaceTimer;
    //�÷��̾ ������ �� �ִ���
    private bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    //�°� ���� �����ð�
    public float hit_invincibility;
    //�����ð��� ��� �ڷ�ƾ
    public Coroutine invincibility = null;
    private SpriteAlphaControl spriteAlpha;
    private Animator animator;
    //���� �Լ��� ������ ����
    private Action pull = null;

    private Rigidbody rigid;

    protected new void Start()
    {
        base.Start();
        weapon = new TestGun(this, r_weapon, r_weapon, 0, 1, objectParent);
        spriteAlpha = GetComponent<SpriteAlphaControl>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        //�������� ���
        MoveCalculate();
        //�����ӿ� ���� ���� ���
        if (pull != null)
            pull();
        //������
        Move();

        Vector3 look = Camera.main.WorldToScreenPoint(transform.position);
        UnitLook(Input.mousePosition - look);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Space();
        }

        if (spaceTimer >= 0)
        {
            spaceTimer -= Time.deltaTime;
            spaceImage.fillAmount = spaceTimer / spaceCooltime;
        }

        weapon.Direction_Calculation_Screen(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            weapon.Fire();
        }
    }

    private void MoveCalculate()
    {
        // ���� �� ���� �Է��� �����ɴϴ�.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // �Է¿� ���� �̵� ���͸� ����ϴ�.
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f);

        // �̵� ������ ���̸� 1�� ����ȭ�ϰ� �ӵ��� ���մϴ�.
        moveDirection.Normalize();
        moveVelocity = moveDirection * moveSpeed;
    }

    //���� ����
    Vector3 target;
    //���� �ð�
    float timer;
    //���� ��
    float power;
    public void Pull(Vector3 target, float timer, float power)
    {
        this.target = target;
        this.timer = timer;
        this.power = power;
        pull += PullAction;
    }
    private void PullAction()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            pull -= PullAction;
        }
        Vector3 dir = (target - transform.position).normalized;
        moveVelocity += new Vector3(dir.x, dir.z, 0) * power;
    }

    private void Move()
    {
        // �̵��� �����մϴ�.
        if (canMove)
        {
            animator.SetFloat("MoveSpeed", moveAnimatorSpeed);
            animator.SetFloat("RunState", 0.5f);
            //transform.Translate(moveVelocity * Time.deltaTime);

            rigid.velocity = new Vector3(moveVelocity.x, 0, moveVelocity.y);
        }
    }

    //�÷��̾�� ������ ��� �����ð��� �ʿ���
    public override void Hit(Unit unit, float figure)
    {
        if (god)
            return;
        base.Hit(unit, figure);
        //�÷��̾�� ü���� 0�̵Ǹ� ������ ��������
        if (stat.HP <= 0)
        {
            Time.timeScale = 0;
            //���� �ٽý���
            return;
        }
        else
        {
            //�÷��̾� �����ð�
            if (invincibility != null && godTimer < hit_invincibility)
            {
                StopCoroutine(invincibility);
                invincibility = StartCoroutine(Invincibility(hit_invincibility));
            }
            else if (invincibility == null)
            {
                invincibility = StartCoroutine(Invincibility(hit_invincibility));
            }
            spriteAlpha.isHit = true;
        }
    }

    //�÷��̾ ��� �������� ���� �¿� ����
    private void UnitLook(Vector3 dir)
    {
        if (dir.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            r_weapon.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            r_weapon.localScale = new Vector3(-1, 1, 1);
        }
    }

    //�����̽��� ������ �뽬
    public void Space()
    {
        if (moveVelocity != Vector3.zero && spaceTimer < 0)
        {
            StartCoroutine(SpaceCoroutine(travel, moveVelocity));
            if (invincibility != null && godTimer < travel)
            {
                StopCoroutine(invincibility);
                invincibility = StartCoroutine(Invincibility(travel));
            }
            else if (invincibility == null)
            {
                invincibility = StartCoroutine(Invincibility(travel));
            }
            spaceTimer = spaceCooltime;
        }
    }

    //�뽬 �̵�
    private IEnumerator SpaceCoroutine(float t, Vector3 velocity)
    {
        float timer = 0;
        canMove = false;
        while (true)
        {
            transform.Translate(velocity * spaceDis * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer > t)
                break;
            yield return null;
        }
        canMove = true;
    }

    private IEnumerator UpGG(Vector3 velocity)
    {
        float timer = 0;
        velocity = new Vector3(velocity.x, velocity.z, 0);
        canMove = false;
        while (true)
        {
            transform.Translate(velocity * 3 * Time.deltaTime);

            timer += Time.deltaTime;

            if (timer > 0.2f)
                break;
            yield return null;
        }
        canMove = true;
    }

    private IEnumerator Invincibility(float t)
    {
        god = true;
        godTimer = t;
        while (godTimer > 0)
        {
            godTimer -= Time.deltaTime;
            yield return null;
        }
        god = false;
        invincibility = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("UpGroundPush"))
        {
            if (!god)
            {
                Vector3 dir = other.GetComponent<UpGround>().playerHitDirection();
                StartCoroutine(UpGG(dir));
            }
        }
    }
}
