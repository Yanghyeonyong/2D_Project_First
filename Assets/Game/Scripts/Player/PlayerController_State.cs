using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_State : MonoBehaviour
{
    private IPlayerState _currentState;

    //이동 방향
    Vector2 moveDir;
    public Vector2 MoveDir => moveDir;


    //플레이어 애니메이션
    [SerializeField] Animator animator;
    public Animator Anim => animator;

    //물리적 충돌 체크
    Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    //박스콜라이더
    BoxCollider2D myCol;
    public BoxCollider2D MyCol => myCol;


    //점프인지 떨어지는 중인지 체크
    bool isJump = false;
    public bool IsJump => isJump;
    //아래 방향 점프
    [SerializeField] float downJump = 2f;
    public float DownJump => downJump;
    //슈퍼점프까지 걸리는 시간
    [SerializeField] float superJumpDelay = 3f;
    public float SuperJumpDelay => superJumpDelay;

    //가만히 있는 상태에서만 가능
    //웅크리고 있는 상태일 때(공격, 이동 불가능 아랫점, 슈퍼 윗점 가능이며 아랫점의 경우 아랫키 + 점프일 경우, 슈퍼 윗점의 경우 아랫점 일정시간 경과 후)
    bool isCrouch = false;
    public bool IsCrouch => isCrouch;

    //캐릭터의 발 쪽에만 따로 콜라이더 설정
    bool isGrounded = false;
    public bool IsGrounded => isGrounded;
    //바닥에 닿았는지 체크
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckDistance = 0.1f;

    //원래 콜라이더 크기
    Vector2 originalColiderOffset;
    public Vector2 OriginalColiderOffset => originalColiderOffset;
    Vector2 originalColiderSize;
    public Vector2 OriginalColiderSize => originalColiderSize;

    //웅크렸을 경우의 콜라이더 크기
    [SerializeField] Vector2 crouchColiderOffset = new Vector2(0, 0.5f);
    public Vector2 CrouchColiderOffset => crouchColiderOffset;
    [SerializeField] Vector2 crouchColiderSize = new Vector2(2f, 1f);
    public Vector2 CrouchColiderSize => crouchColiderSize;

    public PlayerModel playerModel;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private GameObject playerInformationTab;
    public PlayerModel_Dongeon playerModel_Dongeon;


    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color originColor;
    [SerializeField] Color hitColor;
    [SerializeField] float hitTime;
    WaitForSeconds changeTime;


    [SerializeField] GameObject playerFaceCamera;
    public GameObject PlayerFaceCamera => playerFaceCamera;
    Vector3 originCameraPos;
    public Vector3 OriginCameraPos => originCameraPos;
    [SerializeField] Vector3 crouchCameraPos;
    public Vector3 CrouchCameraPos => crouchCameraPos;

    private void Awake()
    {
        changeTime = new WaitForSeconds(hitTime);
        originCameraPos = playerFaceCamera.transform.localPosition;
    }

    private void Start()
    {
        //저장된 데이터 가져옴
        playerModel = GameManager.Instance.playerModel;
        if (GameManager.Instance.curStage == 2)
        {
            playerModel_Dongeon = GameManager.Instance.playerModel_Dongeon;
        }
        else
        {
            playerModel_Dongeon = new PlayerModel_Dongeon(playerModel);
        }

        myCol = GetComponent<BoxCollider2D>();
        originalColiderOffset = myCol.offset;
        originalColiderSize = myCol.size;

        rb = GetComponent<Rigidbody2D>();

        //checkGround.SetAnimaotr(animator);
        UpdateInfo();
        GameManager.Instance.playerView = playerView;


        originColor = spriteRenderer.color;

        playerView.UpdatePlayerHP(playerModel.CurHp / playerModel.MaxHp);

        SetState(new IdleState(this));
    }

    private void Update()
    {
        isGrounded = Physics2D.Raycast(
        _groundChecker.position,
        Vector2.down,
        _groundCheckDistance,
        _groundLayer);

        //이 또한 jump에 넣을까 고민하였으나, 현재 FixedUpdate에 State Update가 들어있어 가끔 동작하지 않음을 확인
        animator.SetBool("IsGround", IsGrounded);

    }
    private void FixedUpdate()
    {
        _currentState.OnUpdate();
    }


    //상태 변환
    public void SetState(IPlayerState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    //이동
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>();
        //값을 입력받아 벡터2로 변환
        if (ctx.performed && !isCrouch)
        {
            //moveDir = ctx.ReadValue<Vector2>();
            //상태 변환
            SetState(new RunState(this));
        }

        //이동 종료시 걷기 애니메이션 종료
        if (ctx.canceled)
        {
            //moveDir = Vector2.zero;

            SetState(new IdleState(this));
        }
    }

    //점프
    public void OnJump(InputAction.CallbackContext ctx)
    {
        //땅에 닿았을 때만
        if (ctx.performed && isGrounded)
        {
            SetState(new JumpState(this));
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isCrouch)
        {
            SetState(new AttackState(this));
        }
    }

    //플레이어가 레이 범위에 있는지 체크
    public LayerMask layerMask;

    //엎드리기
    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        //Idle 애니메이션 상태에서만 실행하라
        if (ctx.performed && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            isCrouch = true;
            SetState(new CrouchState(this));
        }
        //엎드리기 해제
        if (ctx.canceled)
        {
            SetState(new IdleState(this));
            isCrouch = false;
        }
    }

    //플레이어 정보 확인
    public void UpdateInfo()
    {
        if (GameManager.Instance.curStage != 2)
        {
            playerView.UpdateStatus(playerModel);
        }
        else
        {
            playerView.UpdateStatus(playerModel, playerModel_Dongeon);
        }
        playerView.UpdatePlayerHP(playerModel.CurHp / playerModel.MaxHp);
    }

    public void OnPlayerInfo(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerInformationTab.SetActive(!playerInformationTab.activeSelf);
        }
    }

    [SerializeField] int testExp;
    //데미지를 입었을 경우 발생하는 메서드
    public void TestTakeDamage(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            //OnTakeDamage(31f);
            LevelUp(testExp);
        }
    }


    private Coroutine damgageCoroutine;
    public void OnTakeDamage(float takeDamage)
    {
        if (GameManager.Instance.curStage == 2)
        {
            playerModel_Dongeon.TakeDamage(takeDamage);
        }
        else
        {
            playerModel.TakeDamage(takeDamage);
        }
        if (damgageCoroutine == null)
        {
            damgageCoroutine = StartCoroutine(TakeDamageCharacter());
        }
        else
        {
            StopCoroutine(damgageCoroutine);
            damgageCoroutine = StartCoroutine(TakeDamageCharacter());
        }
        UpdateInfo();
        playerView.UpdatePlayerHP(playerModel.CurHp / playerModel.MaxHp);
    }
    IEnumerator TakeDamageCharacter()
    {
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.color = hitColor;
            yield return changeTime;
            spriteRenderer.color = originColor;
            yield return changeTime;
        }
    }

    public void LevelUp(int exp)
    {
        int levelUpCount = playerModel_Dongeon.LevelUp(exp);
        if (levelUpCount > 0)
        {
            Debug.Log("스탯 올려야징");
            playerView.LevelUpPageOpen(levelUpCount);
        }
    }
}
