using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class SkillState : IPlayerState
{
    private PlayerController_State _player;
    private Animator _anim;

    LayerMask layerMask;
    float attackPosY = 1.3f;
    float attackBoxSize = 2.6f;

    public SkillState(PlayerController_State player)
    {
        _player = player;
        _anim = player.Anim;
        layerMask=player.layerMask;
    }

    //이동 시 애니메이션 적용
    public void OnEnter()
    {
        _anim.SetTrigger("IsAttack");
        if (_player.playerModel.CurMp > _player.UsingSkillMp)
        {
            Debug.Log("스킬 발동 시도 2");
            _player.playerModel.UsingSkill(_player.UsingSkillMp);
            FireBall();
        }
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }

    private void FireBall()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log("스킬 발동 시도 3");
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        Vector3 playerPosition = _player.Pos.position + Vector3.up;

        Vector3 dir = mousePosition - playerPosition;
           
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0f, 0f, angle);

        GameObject mybullet = BulletManager.Instance.GetBullet(_player.BulletIndex);

        if (mybullet != null)
        {
            //Debug.Log("가져와서 사용 "+mybullet.name);
            //mybullet.name = "objpool";
            mybullet.transform.rotation = rot;
            mybullet.transform.position = playerPosition;
            mybullet.SetActive(true);
        }
        else
        {
            Debug.Log("없어서 사용");
            mybullet = UnityEngine.Object.Instantiate(_player.Skill, playerPosition, rot);
        }
        mybullet.GetComponent<Bullet>().SetBullet(_player.playerModel_Dongeon.ReturnTotalStatus(2)*1.5f, _player.playerModel_Dongeon.ReturnTotalStatus(3));

    }


    ////플레이어가 레이 범위에 있는지 체크
    //private RaycastHit2D[] CheckEnemy()
    //{
    //    RaycastHit2D[] hit = Physics2D.BoxCastAll(
    //        _player.transform.position + new Vector3(0, attackPosY, 0),
    //        new Vector2(1, attackBoxSize),
    //        0, _player.transform.right,
    //        _player.playerModel_Dongeon.ReturnTotalStatus(2), layerMask);
    //    return hit;
    //}
}
