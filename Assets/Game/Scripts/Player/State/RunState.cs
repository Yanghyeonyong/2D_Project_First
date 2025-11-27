using UnityEngine;


public class RunState : IPlayerState
{
    private PlayerController_State _player;
    private Animator _anim;
    Rigidbody2D rb;
    Vector2 moveDir;
    float moveSpeed = 0;

    public RunState(PlayerController_State player)
    {
        _player = player;
        moveDir = player.MoveDir;
        _anim = player.Anim;
        rb = player.Rb;
        //moveSpeed = player.playerModel.MoveSpeed;
        moveSpeed = player.playerModel_Dongeon.ReturnTotalStatus(3);
    }

    //이동 시 애니메이션 적용
    public void OnEnter()
    {
        //이동 방향 가져오고 애니메이션 실행
        moveDir = _player.MoveDir;
        _anim.SetBool("IsWalk", true);
    }

    public void OnExit()
    {
    //종료 시 운동량 0, 애니메이션 종료
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        _anim.SetBool("IsWalk", false);
    }

    public void OnUpdate()
    {

        //이동 방향에 따라 캐릭터가 바라보는 방향도 바뀌도록 설정
        if (moveDir.x > 0.1)
        {
            _player.transform.rotation = Quaternion.identity;
        }
        else
        {
            _player.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //입력하는 경우 이동
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);

        //입력값 0이면 상태 변환
        if (moveDir.x == 0)
        {
            _player.SetState(new IdleState(_player));
        }
    }
}
