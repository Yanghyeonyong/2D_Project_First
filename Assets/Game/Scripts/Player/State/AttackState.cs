using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class AttackState : IPlayerState
{
    private PlayerController_State _player;
    private Animator _anim;

    LayerMask layerMask;
    float attackPosY = 1.3f;
    float attackBoxSize = 2.6f;

    public AttackState(PlayerController_State player)
    {
        _player = player;
        _anim = player.Anim;
        layerMask=player.layerMask;
    }

    //이동 시 애니메이션 적용
    public void OnEnter()
    {
        //애니메이션 실행
        _anim.SetTrigger("IsAttack");
        SoundManager.Instance.PlayEffect(_player.EffectAudios[0]);
        //공격 범위에 몬스터 있는지 확인
        RaycastHit2D[] hit = CheckEnemy();
        //있다면 모든 몬스터에게 데미지
        foreach (RaycastHit2D enemy in hit)
        {
            enemy.collider.gameObject.GetComponent<EnemyController>().OnTakeDamage(_player.playerModel_Dongeon.ReturnTotalStatus(1));
        }
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }

    //몬스터가 박스캐스트 범위에 있는지 체크
    private RaycastHit2D[] CheckEnemy()
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(
            _player.transform.position + new Vector3(0, attackPosY, 0),
            new Vector2(1, attackBoxSize),
            0, _player.transform.right,
            _player.playerModel_Dongeon.ReturnTotalStatus(2), layerMask);
        return hit;
    }
}
