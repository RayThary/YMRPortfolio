using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public float moveSpeed = 5.0f; // 이동 속도 조절용 변수
    public float moveAnimatorSpeed;
    float horizontalInput = 0;
    float verticalInput = 0;
    Vector3 moveVelocity;

    public Weapon weapon;
    public Transform r_weapon;
    public Transform objectParent;

    Ray ray;
    RaycastHit hit;

    //대쉬의 쿨타임을 알려줄 이미지
    public Image spaceImage;
    //대쉬의 쿨타임
    public float spaceCooltime = 3;
    //대쉬가 움직일 거리
    public float spaceDis;
    //대쉬의 이동시간과 무적시간
    public float travel = 0.1f;
    //시간을 잴 변수
    private float spaceTimer;
    //플레이어가 움직일 수 있는지
    private bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    //맞고 나서 무적시간
    public float hit_invincibility;
    //무적시간을 재는 코루틴
    public Coroutine invincibility = null;
    private SpriteAlphaControl spriteAlpha;
    private Animator animator;
    //당기는 함수를 저장할 변수
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
        //움직임을 계산
        MoveCalculate();
        //움직임에 당기는 힘을 계산
        if (pull != null)
            pull();
        //움직임
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
        // 수평 및 수직 입력을 가져옵니다.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // 입력에 따라 이동 벡터를 만듭니다.
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f);

        // 이동 벡터의 길이를 1로 정규화하고 속도를 곱합니다.
        moveDirection.Normalize();
        moveVelocity = moveDirection * moveSpeed;
    }

    //당기는 방향
    Vector3 target;
    //당기는 시간
    float timer;
    //당기는 힘
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
        // 이동을 적용합니다.
        if (canMove)
        {
            animator.SetFloat("MoveSpeed", moveAnimatorSpeed);
            animator.SetFloat("RunState", 0.5f);
            //transform.Translate(moveVelocity * Time.deltaTime);

            rigid.velocity = new Vector3(moveVelocity.x, 0, moveVelocity.y);
        }
    }

    //플레이어는 맞을때 잠시 무적시간이 필요함
    public override void Hit(Unit unit, float figure)
    {
        if (god)
            return;
        base.Hit(unit, figure);
        //플레이어는 체력이 0이되면 게임이 끝난거임
        if (stat.HP <= 0)
        {
            Time.timeScale = 0;
            //게임 다시시작
            return;
        }
        else
        {
            //플레이어 무적시간
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

    //플레이어가 어디를 보는지에 따라 좌우 반전
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

    //스페이스를 누르면 대쉬
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

    //대쉬 이동
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
