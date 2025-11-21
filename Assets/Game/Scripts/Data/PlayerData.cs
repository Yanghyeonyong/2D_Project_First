using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("플레이어 스탯")]
    public float hp;
    public float mp;
    public float defence;
    public float damage;
    public float attackRange;
    public float moveSpeed;
    public float jumpForce;


    [Header("소지 물품")]
    //현재는 골드만 설정하고 이후 아이템 추가 예정
    public int gold;
}
