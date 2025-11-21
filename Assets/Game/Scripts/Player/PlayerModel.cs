using System;
using UnityEngine;

public class PlayerModel
{
    //플레이어 스탯
    [SerializeField] private float maxHp = 100;
    public float MaxHp => maxHp;

    [SerializeField] private float curHp;
    public float CurHp => curHp;

    [SerializeField] private float maxMp = 50;
    public float MaxMp => maxMp;
    [SerializeField] private float curMp;
    public float CurMp => curMp;

    private float defence;
    public float Defence => defence;
    private float damage;
    public float Damage => damage;
    private float attackRange;
    public float AttackRange => attackRange;

    private float moveSpeed;
    public float MoveSpeed => moveSpeed;
    private float jumpForce;
    public float JumpForce => jumpForce;


    private int gold = 0;
    public int Gold=>gold;

    public event Action<float> OnHealthChange;
    public event Action<int> OnPlayerJump;

    public PlayerModel(float maxHp, float maxMp, float defence, float damage, float attackRange, float moveSpeed, float jumpForce, int gold)
    {
        this.maxHp = maxHp;
        curHp=maxHp;
        this.maxMp = maxMp;
        curMp=maxMp;
        this.defence = defence;
        this.damage = damage;
        this.attackRange = attackRange;
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.gold = gold;   
    }

    public void Init()
    {
        curHp = maxHp;
        curMp = maxMp;
    }

    public void TakeDamage(float amount)
    {
        curHp -= amount;
        if (curHp <= 0)
        {
            curHp = 0;
        }
        OnHealthChange?.Invoke(curHp);
    }

    public bool BuyStatus(int usingGold)
    {
        if (usingGold > gold)
            return false;
        else
        {
            gold -= usingGold;
            return true;
        }
    }

    public void ChangeStatus(int statType, float statPoint)
    {
        switch (statType)
        {
            case 0:
                maxHp += statPoint;
                break;
            case 1:
                maxMp += statPoint;
                break;
            case 2:
                defence += statPoint;
                break;
            case 3:
                damage += statPoint;
                break;
            case 4:
                attackRange += statPoint;
                break;
            case 5:
                moveSpeed += statPoint;
                break;
            case 6:
                jumpForce += statPoint;
                break;
        }
    }
}
