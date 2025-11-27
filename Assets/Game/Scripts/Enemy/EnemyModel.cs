using JetBrains.Annotations;
using System;
using UnityEngine;
[System.Serializable]
public class EnemyModel
{
    //몬스터 스탯
    [SerializeField] private float maxHp = 100;
    public float MaxHp => maxHp;
    [SerializeField] private float curHp;
    public float CurHp => curHp;

    [SerializeField] private float damage;
    public float Damage => damage;

    [SerializeField] private float defence;
    public float Defence => defence;

    //움직임 속도
    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    //적 발견 거리
    [SerializeField] private float detectRange;
    public float DetectRange => detectRange;

    //적 공격 시도 거리
    [SerializeField] private float attackRange;
    public float AttackRange => attackRange;

    //투사체 속도
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed => bulletSpeed;

    //공격 간격
    [SerializeField] private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    //경험치
    [SerializeField] private int exp;
    public int Exp => exp;

    //드랍 골드
    [SerializeField] private int gold;
    public int Gold => gold;


    public event Action<float> OnHealthChange;

    public EnemyModel(float maxHp, float curHp, float damage, float defence, float moveSpeed, float detectRange, float attackRange, float bulletSpeed, float attackSpeed, int exp, int gold)
    {
        this.maxHp = maxHp;
        curHp = maxHp;
        this.damage = damage;
        this.defence = defence;
        this.moveSpeed = moveSpeed;
        this.detectRange = detectRange;
        this.attackRange = attackRange;
        this.bulletSpeed = bulletSpeed;
        this.attackSpeed = attackSpeed;
        this.exp = exp;
        this.gold = gold;
    }

    public EnemyModel(float maxHp, int exp)
    {
        this.maxHp = maxHp;
        curHp = maxHp;
        this.exp = exp;
    }



    public bool TakeDamage(float takeDamage)
    {
        //방어력으로 인한 피격 데미지 감소
        curHp -= Mathf.Max(0, takeDamage-defence);
        if (curHp <= 0)
        {
            curHp = 0;
            return false;
        }
        return true;
    }

    public void Init()
    {
        curHp = maxHp;
    }

    public void EnemyUpgrade(float upgradeRate)
    {
        maxHp += maxHp * upgradeRate;
        curHp += maxHp;
        damage += damage * upgradeRate;
        defence += defence * upgradeRate;
        exp += Mathf.Max(1, (int) (exp * upgradeRate));
        gold += (int)(gold * upgradeRate);
    }
}
