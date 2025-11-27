using UnityEngine;

public class IdleState : IPlayerState
{

    private PlayerController_State _player;
    private Animator _anim;
    Rigidbody2D rb;

    public IdleState(PlayerController_State player)
    {
        _player = player;
        _anim=player.Anim;
        rb = player.Rb;
    }

    //기본 상태면 운동량 0, 점프에서 연속으로 이동으로 전환하는 경우도 작성
    public void OnEnter()
    {
        if (_player.MoveDir.x == 0)
        {
            //입력 멈추면 운동량도 0
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            //있다면 상태 전환
            _player.SetState(new RunState(_player));
        }
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
    }
}
