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
    public int gold;
}
