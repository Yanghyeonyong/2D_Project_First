using UnityEngine;

public class JumpState : IPlayerState
{

    private PlayerController_State _player;
    private Animator _anim;
    Rigidbody2D rb;
    float jumpForce;

    Vector2 moveDir;
    float moveSpeed = 0;

    bool jumpFinish = false;

    public JumpState(PlayerController_State player)
    {
        _player = player;
        _anim = player.Anim;
        rb = player.Rb;
        jumpForce = player.playerModel_Dongeon.ReturnTotalStatus(4);

        moveDir = player.MoveDir;
        _anim = player.Anim;
        rb = player.Rb;
        moveSpeed = player.playerModel_Dongeon.ReturnTotalStatus(3);
    }


    public void OnEnter()
    {
        //시작하자마자 점프 멈추는 것 방지
        jumpFinish = false;
        //점프중에도 이동
        moveDir = _player.MoveDir;

        //애니메이션 실행
        _anim.SetTrigger("IsJump");

        //위로 점프
        rb.AddForce(Vector2.up * _player.playerModel_Dongeon.ReturnTotalStatus(4), ForceMode2D.Impulse);
        SoundManager.Instance.PlayEffect(_player.EffectAudios[2]);
    }

    public void OnExit()
    {
        _anim.SetBool("IsWalk", false);
    }


    public void OnUpdate()
    {
        if (!_player.IsGrounded && !jumpFinish)
        {
            jumpFinish = true;
        }


        //이동 방향에 따라 캐릭터가 바라보는 방향도 바뀌도록 설정
        if (moveDir.x > 0.1)
        {
            _player.transform.rotation = Quaternion.identity;
        }
        else
        {
            _player.transform.eulerAngles = new Vector3(0, 180, 0);
        }


        //플레이어 능력치만큼 점프
        rb.linearVelocity = new Vector2(moveDir.x * _player.playerModel_Dongeon.ReturnTotalStatus(3), rb.linearVelocity.y);

        //플레이어가 땅에 닿았으면 idle로 전환
        if (_player.IsGrounded &&jumpFinish)
        {
            _anim.SetBool("IsGround", _player.IsGrounded);
            _player.SetState(new IdleState(_player));
        }
    }

}
