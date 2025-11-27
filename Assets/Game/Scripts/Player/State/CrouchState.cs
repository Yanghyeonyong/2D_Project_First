using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class CrouchState : MonoBehaviour, IPlayerState 
{
    PlayerController_State _player;
    Animator _anim;
    Rigidbody2D rb;
    BoxCollider2D myCol;

    //원래 콜라이더 크기
    Vector2 originalColiderOffset;
    Vector2 originalColiderSize;

    //웅크렸을 경우의 콜라이더 크기
    Vector2 crouchColiderOffset;
    Vector2 crouchColiderSize;

    //웅크렸을 경우 카메라 이동
    GameObject playerFaceCamera;
    Vector3 originCameraPos;
    Vector3 crouchCameraPos;

    //슈퍼점프 대기시간
    float superJumpDelay;
    Coroutine superJumpCoroutine;

    //슈퍼점프 중일 경우
    bool isSuperJump;

    //이동 방향
    Vector2 moveDir;
    float moveSpeed = 0;

    //땅에 닿았을 경우
    bool jumpFinish = false;

    //시간 체크용
    float currentTime = 0f;
    bool crouchFinish = false;
    public CrouchState(PlayerController_State player)
    {
        _player = player;
        _anim = player.Anim;
        rb=player.Rb;
        myCol = player.MyCol;

        originalColiderOffset = player.OriginalColiderOffset;
        originalColiderSize = player.OriginalColiderSize;

        crouchColiderOffset = player.CrouchColiderOffset;
        crouchColiderSize = player.CrouchColiderSize;

        playerFaceCamera = player.PlayerFaceCamera;
        originCameraPos = player.OriginCameraPos;
        crouchCameraPos = player.CrouchCameraPos;

        superJumpDelay = player.SuperJumpDelay;

        moveDir = player.MoveDir;

    }

    //이동 시 애니메이션 적용
    public void OnEnter()
    {
        currentTime = 0;
        crouchFinish = false;

        jumpFinish = false;
        isSuperJump = false;
        //콜라이더 조정
        SetCrouchColider();

        //애니메이션 실행
        _anim.SetBool("IsCrouch", true);
        //웅크린 상태에서의 얼굴 촬영을 위해 카메라 이동
        playerFaceCamera.transform.localPosition = crouchCameraPos;
    }


    public void OnExit()
    {
        crouchFinish=true;
        //애니메이션 탈출
        _anim.SetBool("IsCrouch", false);
        InitColider();
        //카메라 원래 위치로 복귀
        playerFaceCamera.transform.localPosition = originCameraPos;
        if (superJumpCoroutine != null)
        {
            //슈퍼 점프 코루친 취소
            StopCoroutine(superJumpCoroutine);
        }
    }

    public void OnUpdate()
    {
        //일정 시간 웅크리고 있을 경우 최종 점프 능력치의 1.5배 높이로 점프
        if (!isSuperJump && !crouchFinish)
        {
            currentTime += Time.fixedDeltaTime;
            Debug.Log(currentTime);
            if (currentTime >= superJumpDelay)
            {
                _anim.SetBool("IsCrouch", false);
                //애니메이션 실행
                _anim.SetTrigger("IsJump");
                rb.AddForce(Vector2.up * _player.playerModel_Dongeon.ReturnTotalStatus(4) * 1.5f, ForceMode2D.Impulse);
                SoundManager.Instance.PlayEffect(_player.EffectAudios[1]);
                isSuperJump = true;

                InitColider();
            }
        }

        //슈퍼 점프중 공중에서 이동
        if (isSuperJump)
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

            rb.linearVelocity = new Vector2(moveDir.x * _player.playerModel_Dongeon.ReturnTotalStatus(3), rb.linearVelocity.y);

            if (_player.IsGrounded && jumpFinish)
            {
                _anim.SetBool("IsGround", _player.IsGrounded);
                _player.SetState(new IdleState(_player));
            }
        }
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
