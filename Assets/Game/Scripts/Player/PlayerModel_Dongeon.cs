using System;
using UnityEngine;
[System.Serializable]
public class PlayerModel_Dongeon
{
    //플레이어 기본 스탯
    PlayerModel playerModel;
    //추가 플레이어 스탯
    [SerializeField] private float defence;
    public float Defence => defence;
    [SerializeField] private float damage;
    public float Damage => damage;
    [SerializeField] private float attackRange;
    public float AttackRange => attackRange;

    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;
    [SerializeField] private float jumpForce;
    public float JumpForce => jumpForce;



    [SerializeField] private int curExp;
    public int CurExp => curExp;
    [SerializeField] private int maxExp;
    public int MaxExp => maxExp;


    public PlayerModel_Dongeon(PlayerModel playerModel)
    {
        this.playerModel = playerModel;
        defence = 0;
        damage = 0;
        attackRange = 0;
        moveSpeed = 0;
        jumpForce = 0;
        curExp = 0;
        maxExp = 50;
    }

    public bool TakeDamage(float takeDamage)
    {
        //방어력으로 인한 피격 데미지 감소(1차)
        takeDamage = Mathf.Max(0, takeDamage - defence);
        return playerModel.TakeDamage(takeDamage);
    }

    public void ChangeStatus(int statType, float statPoint)
    {
        switch (statType)
        {
            case 0:
                defence += statPoint;
                break;
            case 1:
                damage += statPoint;
                break;
            case 2:
                attackRange += statPoint;
                break;
            case 3:
                moveSpeed += statPoint;
                break;
            case 4:
                jumpForce += statPoint;
                break;
        }
    }

    public float ReturnTotalStatus(int statType)
    {
        switch (statType)
        {
            case 0:
                return defence+playerModel.Defence;
            case 1:
                return damage+playerModel.Damage;
            case 2:
                return attackRange+playerModel.AttackRange;
            case 3:
                return moveSpeed+playerModel.MoveSpeed;
            case 4:
                return jumpForce+playerModel.JumpForce;
        }
        return 0;
    }

    public int LevelUp(int exp)
    {
        int levelUpCount = 0;
        curExp += exp;
        if (curExp >= maxExp)
        {
            while (curExp >= maxExp)
            {
                Debug.Log("레벨업!");
                curExp -= maxExp;
                levelUpCount++;
            }
        }
        return levelUpCount;
    }
}
