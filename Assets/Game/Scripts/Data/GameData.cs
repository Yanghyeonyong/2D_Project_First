using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    //플레이어가 올린 골드 레벨, 클리어한 층 수 등이 표시
    [Header("영구 능력치 레벨")]
    public float goldHpLevel;
    public float goldMpLevel;
    public float goldDefenceLevel;
    public float goldDamageLevel;
    public float goldAttackRangeLevel;
    public float goldMoveSpeedLevel;
    public float goldJumpForceLevel;
}
