using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController_State : MonoBehaviour
{
    //상태 체크
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

    //플레이어 정보
    public PlayerModel playerModel;
    //플레이어 UI 변환
    [SerializeField] private PlayerView playerView;
    //플레이어 능력치 확인 패널
    [SerializeField] private GameObject playerInformationTab;
    //던전에서의 플레이어 정보 
    public PlayerModel_Dongeon playerModel_Dongeon;


    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color originColor;
    [SerializeField] Color hitColor;
    [SerializeField] float hitTime;
    WaitForSeconds changeTime;

    //플레이어의 얼굴을 촬영하는 카메라(렌더 텍스처용)
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

    //테스트 시 true로 설정
    public bool onTest = false;

    //플레이어 위치 정보
    [SerializeField] Transform pos;
    public Transform Pos => pos;

    //투사체 종류
    [SerializeField] int bulletIndex;
    public int BulletIndex => bulletIndex;

    //투사체 프리팹
    [SerializeField] GameObject skill;
    public GameObject Skill => skill;
    //스킬 사용시 소모 MP
    [SerializeField] float usingSkillMp;
    public float UsingSkillMp => usingSkillMp;
    
    //효과음
    [SerializeField] AudioClip[] effectAudios;
    public AudioClip[] EffectAudios => effectAudios;
    private void Start()
    {
        //저장된 데이터 가져옴
        if (!onTest)
        {
            playerModel = GameManager.Instance.playerModel;
        }
        //타워에선 PlayerModel_Dongeon 정보 사용
        if (GameManager.Instance.curStage == 2)
        {
            playerModel_Dongeon = GameManager.Instance.playerModel_Dongeon;
        }
        else
        {
            //타워 아니면 초기화
            playerModel_Dongeon = new PlayerModel_Dongeon(playerModel);
        }

        //플레이어 초기 콜라이더 저장 
        myCol = GetComponent<BoxCollider2D>();
        originalColiderOffset = myCol.offset;
        originalColiderSize = myCol.size;

        rb = GetComponent<Rigidbody2D>();


        //UpdateInfo();
        //매니저에 플레이어 뷰 전달
        GameManager.Instance.playerView = playerView;

        originColor = spriteRenderer.color;

        playerModel.Init();

        //플레이어 HP와 MP UI에 표시
        playerView.UpdatePlayerHP(playerModel.CurHp / playerModel.MaxHp);
        playerView.UpdatePlayerMP(playerModel.CurMp / playerModel.MaxMp);
        //플레이어 능력치 View에 전달
        UpdateInfo();

        //기본 상태 설정
        SetState(new IdleState(this));

        //무적 상태 해제
        GameManager.Instance.IsInvincible = false;
    }

    private void Update()
    {
        //땅에 닿았는지 확인
        isGrounded = Physics2D.Raycast(
        _groundChecker.position,
        Vector2.down,
        _groundCheckDistance,
        _groundLayer);

        animator.SetBool("IsGround", IsGrounded);

    }
    private void FixedUpdate()
    {
        //각 State에 따른 update 실행
        if (!GameManager.Instance.IsInvincible)
        {
            _currentState.OnUpdate();
        }
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
        //무적상태에서는 실행 불가능
        if (!GameManager.Instance.IsInvincible)
        {
            //값을 입력받아 벡터2로 변환
            moveDir = ctx.ReadValue<Vector2>();
            if (ctx.performed && !isCrouch)
            {
                //상태 변환
                SetState(new RunState(this));
            }

            //이동 종료시 걷기 애니메이션 종료
            if (ctx.canceled)
            {
                SetState(new IdleState(this));
            }

        }
    }

    //점프
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.IsInvincible)
        {
            //땅에 닿았을 때만
            if (ctx.performed && isGrounded)
            {
                SetState(new JumpState(this));
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.IsInvincible)
        {
            //웅크린 상태에서는 불가능
            if (ctx.performed && !isCrouch)
            {
                SetState(new AttackState(this));
            }
        }
    }

    public void OnSkill(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.IsInvincible)
        {
            if (ctx.performed && !isCrouch)
            {
                Debug.Log("스킬 발동 시도");
                SetState(new SkillState(this));
                playerView.UpdatePlayerMP(playerModel.CurMp / playerModel.MaxMp);
            }
        }
    }

    //플레이어가 레이 범위에 있는지 체크
    public LayerMask layerMask;

    //엎드리기
    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.IsInvincible)
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
    }

    //플레이어 정보 확인
    public void UpdateInfo()
    {
        //플레이어 정보창에 모델 기반 능력치 값 작성
        if (GameManager.Instance.curStage != 2)
        {
            playerView.UpdateStatus(playerModel);
        }
        else
        {
            playerView.UpdateStatus(playerModel, playerModel_Dongeon);
        }

    }

    //플레이어 정보창 활성화
    public void OnPlayerInfo(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.IsInvincible)
        {
            if (ctx.started)
            {
                playerInformationTab.SetActive(!playerInformationTab.activeSelf);
            }
            playerView.UpdatePlayerHP(playerModel.CurHp / playerModel.MaxHp);
        }
    }


    //피격시
    private Coroutine damgageCoroutine;
    [SerializeField] float dieAnimationTime = 2f;
    public void OnTakeDamage(float takeDamage)
    {
        bool isDie = false;
        if (!GameManager.Instance.IsInvincible)
        {
            SoundManager.Instance.PlayEffect(effectAudios[6]);
            if (GameManager.Instance.curStage == 2)
            {
                isDie = playerModel_Dongeon.TakeDamage(takeDamage);
            }
            else
            {
                playerModel.TakeDamage(takeDamage);
            }
            //피격 코루틴 실행
            if (damgageCoroutine == null)
            {
                damgageCoroutine = StartCoroutine(TakeDamageCharacter());
            }
            else
            {
                StopCoroutine(damgageCoroutine);
                damgageCoroutine = StartCoroutine(TakeDamageCharacter());
            }
            //정보창 재작성
            UpdateInfo();
            //HP 표시
            playerView.UpdatePlayerHP(playerModel.CurHp / playerModel.MaxHp);

            //사망시 코루틴 실행 및 최고 점수 확인
            if (!isDie)
            {
                //현재 tower 맵 시작 위치가 -6.xxx부터 시작하여 + 6 
                playerModel.SetBestScore(pos.position.y + 6);
                rb.linearVelocity = Vector2.zero;
                SoundManager.Instance.PlayEffect(effectAudios[4]);
                StartCoroutine(playerView.DieAnimation(dieAnimationTime));
            }
        }
    }
    //피격시 표시
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

    //몬스터 처치시 exp와 gold 모델에 전달 및 MP 회복
    public void KillMonster(int exp, int gold)
    {
        playerModel.HealingMp();
        playerView.UpdatePlayerMP(playerModel.CurMp / playerModel.MaxMp);
        playerModel.GetMoney(gold);
        UpdateInfo();
        int levelUpCount = playerModel_Dongeon.LevelUp(exp);
        if (levelUpCount > 0)
        {
            //레벨업시 능력치 증가 버튼 누르기 전까지 무적
            SoundManager.Instance.PlayEffect(effectAudios[5]);
            GameManager.Instance.IsInvincible = true;
            playerView.LevelUpPageOpen(levelUpCount);
        }
    }
}
