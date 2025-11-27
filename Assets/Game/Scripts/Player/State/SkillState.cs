using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class SkillState : IPlayerState
{
    private PlayerController_State _player;
    private Animator _anim;

    public SkillState(PlayerController_State player)
    {
        _player = player;
        _anim = player.Anim;
    }

    //이동 시 애니메이션 적용
    public void OnEnter()
    {
        //애니메이션 실행
        _anim.SetTrigger("IsAttack");
        //마나가 있다면 스킬 발동
        if (_player.playerModel.CurMp > _player.UsingSkillMp)
        {
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
        //마우스의 방향으로 투사체 발사
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        Vector3 playerPosition = _player.Pos.position + new Vector3(0, 1.5f,0);

        Vector3 dir = mousePosition - playerPosition;
           
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0f, 0f, angle);

        //오브젝트 풀링을 통해 투사체 재사용 시도
        GameObject mybullet = BulletManager.Instance.GetBullet(_player.BulletIndex);
        if (mybullet != null)
        {
            mybullet.transform.rotation = rot;
            mybullet.transform.position = playerPosition;
            mybullet.SetActive(true);
        }
        //실패시 생성해서 사용
        else
        {
            mybullet = UnityEngine.Object.Instantiate(_player.Skill, playerPosition, rot);
        }
        SoundManager.Instance.PlayEffect(_player.EffectAudios[3]);
        //투사체 능력치 설정
        mybullet.GetComponent<Bullet>().SetBullet(_player.playerModel_Dongeon.ReturnTotalStatus(1)*1.5f, _player.playerModel_Dongeon.ReturnTotalStatus(3));
    }
}
