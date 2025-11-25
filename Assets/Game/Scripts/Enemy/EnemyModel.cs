using JetBrains.Annotations;
using System;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    //몬스터 스탯
    [SerializeField] private float maxHp = 100;
    public float MaxHp => maxHp;
    [SerializeField] private float curHp;
    public float CurHp => curHp;
    [SerializeField] private int exp;
    public int Exp => exp;

    public event Action<float> OnHealthChange;
    public EnemyModel(float maxHp)
    {
        this.maxHp = maxHp;
        curHp = maxHp;
    }

    public void TakeDamage(float takeDamage)
    {
        //방어력으로 인한 피격 데미지 감소
        curHp -= Mathf.Max(0, takeDamage);
        if (curHp <= 0)
        {
            curHp = 0;
        }
        //OnHealthChange?.Invoke(curHp);
    }

    public void Init()
    {
        curHp = maxHp;
    }

    public void Die()
    {

    }
}
