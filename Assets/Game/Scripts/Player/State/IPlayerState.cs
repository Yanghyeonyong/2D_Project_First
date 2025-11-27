using UnityEngine;

public interface IPlayerState
{
    //상태 진입 처리
    void OnEnter();
    //상태 유지 중 처리
    void OnUpdate();
    //상태 종료 처리
    void OnExit();
}
