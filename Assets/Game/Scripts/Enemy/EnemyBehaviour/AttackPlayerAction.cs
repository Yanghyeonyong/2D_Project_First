using System;
using Unity.Behavior;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackPlayer", story: "[Self] Attack Player With [EnemyController] and [BulletIndex]", category: "Action", id: "d52963fc5477954b50a3c18922882081")]
public partial class AttackPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<EnemyController> EnemyController;
    [SerializeReference] public BlackboardVariable<int> BulletIndex;
    GameObject player;

    protected override Status OnStart()
    {
        //플레이어 찾기
        if (player == null)
        {
            player = GameObject.FindFirstObjectByType<PlayerController_State>().gameObject;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        SpawnBullet();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }


    //플레이어가 공격 범위 내에 진입시 true 반환
    private bool CheckPlayer()
    {
        float distance = Vector2.Distance(Self.Value.transform.position, player.transform.position);
        if (distance <= EnemyController.Value.enemyModel.AttackRange)
        {
            return true;
        }
        else
            return false;
    }

    //일정 시간 마다 투사체 소환
    float curTime = 0;
    public void SpawnBullet()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            if (CheckPlayer())
            {
                //몬스터에서 플레이어를 바라보는 각도 확인
                // Vector.up을 더한 이유는 플레이어의 위치가 플레이어 기준 발에 있어서 너무 아래로 날리는 것 방지
                Vector3 dir = player.transform.position - EnemyController.Value.SpawnPos.transform.position + Vector3.up;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                Quaternion rot = Quaternion.Euler(0f, 0f, angle);

                //오브젝트 풀링을 통해 투사체 객체 가져오기 시도
                GameObject mybullet = BulletManager.Instance.GetBullet(BulletIndex.Value);

                //가져왔으면 앞서 구한 위치 및 각도로 이동 및 활성화
                if (mybullet != null)
                {
                    mybullet.transform.rotation = rot;
                    mybullet.transform.position = EnemyController.Value.SpawnPos.transform.position;
                    mybullet.SetActive(true);
                }
                //없으면 새롭게 생성
                else
                {
                    mybullet = UnityEngine.Object.Instantiate(EnemyController.Value.bullet, EnemyController.Value.SpawnPos.transform.position, rot);
                }
                //효과음 재생
                SoundManager.Instance.PlayEffect(EnemyController.Value.EffectAudios[0]);
                //투사체 속도, 공격력 세팅
                mybullet.GetComponent<Bullet>().SetBullet(EnemyController.Value.enemyModel.Damage, EnemyController.Value.enemyModel.BulletSpeed);
                //공격 간격 다시 설정
                curTime = EnemyController.Value.enemyModel.AttackSpeed;
            }
        }
    }
}

