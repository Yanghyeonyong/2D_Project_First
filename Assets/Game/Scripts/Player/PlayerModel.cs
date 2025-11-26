using System;
using UnityEngine;
using UnityEngine.Rendering;
[System.Serializable]
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


    [SerializeField] private int gold = 0;
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

    public void TakeDamage(float takeDamage)
    {
        //방어력으로 인한 피격 데미지 감소
        curHp -= Mathf.Max(0,takeDamage-defence);
        if (curHp <= 0)
        {
            curHp = 0;
        }
        OnHealthChange?.Invoke(curHp);
    }

    public void UsingSkill(float usingMp)
    {
        curMp -= usingMp;
    }

    public void HealingMp()
    {
        curMp = Mathf.Min(MaxMp, curMp + MaxMp / 5);
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

    public void GetMoney(int getGold)
    {
        gold += getGold;
    }
}
