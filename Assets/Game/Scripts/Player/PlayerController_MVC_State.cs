using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController_MVC_State : MonoBehaviour
{
    //이동 방향
    Vector2 moveDir;
    //플레이어 애니메이션
    [SerializeField] Animator animator;
    //박스콜라이더
    BoxCollider2D myCol;
    //물리적 충돌 체크
    Rigidbody2D rb;

    //아래 방향 점프
    [SerializeField] float downJump = 2f;
    //슈퍼점프까지 걸리는 시간
    [SerializeField] float superJumpDelay = 3f;

    //땅에 닿았는지 체크를 위해 작성하였으나, 이후 머리가 땅에 닿아서 활성화되어 다른 스크립트로 이관
    //캐릭터의 발 쪽에만 따로 콜라이더 설정
    //bool isGounded = false;

    //가만히 있는 상태에서만 가능
    //웅크리고 있는 상태일 때(공격, 이동 불가능 아랫점, 슈퍼 윗점 가능이며 아랫점의 경우 아랫키 + 점프일 경우, 슈퍼 윗점의 경우 아랫점 일정시간 경과 후)
    bool isCrouch = false;

    //원래 콜라이더 크기
    Vector2 originalColiderOffset;
    Vector2 originalColiderSize;

    //웅크렸을 경우의 콜라이더 크기
    [SerializeField] Vector2 crouchColiderOffset = new Vector2(0, 0.5f);
    [SerializeField] Vector2 crouchColiderSize = new Vector2(2f, 1f);

    //슈퍼점프를 실행하는 코루틴
    Coroutine superJumpCoroutine;

    //땅에 닿았는지 체크하는 스크립트
    [SerializeField] CheckGround checkGround;


    private PlayerData playerData;
    private PlayerModel playerModel;

    private void Awake()
    {
    }

    void Start()
    {
        //저장된 데이터 가져옴
        playerData = GameManager.Instance.playerData;
        playerModel = GameManager.Instance.playerModel;

        myCol = GetComponent<BoxCollider2D>();
        originalColiderOffset = myCol.offset;
        originalColiderSize = myCol.size;

        rb = GetComponent<Rigidbody2D>();

        //checkGround.SetAnimaotr(animator);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2
            (moveDir.x * playerModel.MoveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        //웅크리고 있으면 이동 불가능
        if (!isCrouch)
        {
            //값을 입력받아 벡터2로 변환
            moveDir = ctx.ReadValue<Vector2>();
            if (moveDir.x != 0)
            {
                //이동하는 경우 걷기 애니메이션 실행
                animator.SetBool("IsWalk", true);

                //이동 방향에 따라 캐릭터가 바라보는 방향도 바뀌도록 설정
                if (moveDir.x > 0.1)
                {
                    transform.rotation = Quaternion.identity;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }

            //이동 종료시 걷기 애니메이션 종료
            if (ctx.canceled)
            {
                animator.SetBool("IsWalk", false);
            }
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        //윗방향 점프
        if (ctx.performed && !isCrouch)
        {
            //if (isGounded)
            if (checkGround.OnGround)
            {
                rb.AddForce(Vector2.up * playerModel.JumpForce, ForceMode2D.Impulse);
            }
        }
        //아랫방향 점프
        else if (ctx.performed && isCrouch)
        {
            //if (isGounded)
            if (checkGround.OnGround)
            {
                StopCoroutine(superJumpCoroutine);
                transform.position = new Vector2(transform.position.x, transform.position.y - downJump);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            InitColider();
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isCrouch)
        {
            animator.SetTrigger("IsAttack");
        }
    }

    //엎드리기
    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        //Idle 애니메이션 상태에서만 실행하라
        if (ctx.performed && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //콜라이더 조정
            SetCrouchColider();
            isCrouch = true;
            animator.SetBool("IsCrouch", true);

            superJumpCoroutine = StartCoroutine(SuperJump());
        }

        //엎드리기 해제
        if (ctx.canceled)
        {
            //콜라이더 복구
            InitColider();

            isCrouch = false;
            animator.SetBool("IsCrouch", false);

            //슈퍼점프 아직 못했으면 중단
            if (superJumpCoroutine != null)
            {
                StopCoroutine(superJumpCoroutine);
            }
        }
    }

    //일정 시간 웅크리고 있으면 슈퍼점프
    IEnumerator SuperJump()
    {
        yield return new WaitForSeconds(superJumpDelay);
        animator.SetBool("IsCrouch", false);
        rb.AddForce(Vector2.up * playerModel.JumpForce * 2f, ForceMode2D.Impulse);
    }

    //엎드린 상태에서는 콜라이더 작아지도록
    private void SetCrouchColider()
    {
        myCol.offset = crouchColiderOffset;
        myCol.size = crouchColiderSize;
    }

    //콜라이더 원상 복구
    private void InitColider()
    {
        myCol.offset = originalColiderOffset;
        myCol.size = originalColiderSize;
    }
}
