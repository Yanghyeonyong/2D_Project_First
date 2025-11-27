using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Self] Patrol [WayPoint] with [EnemyController]", category: "Action", id: "3858f9a9051e2f18bf15b229921b1450")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> WayPoint;
    [SerializeReference] public BlackboardVariable<EnemyController> EnemyController;
    private int _currentWayIndex;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        //몬스터가 죽었거나 플레이어가 무적 상태면 멈춰라
        if (EnemyController.Value.IsDie|| GameManager.Instance.IsInvincible)
            return Status.Failure;

        if (WayPoint.Value.Count == 0)
        {
            return Status.Failure;
        }

        //웨이포인트를 타겟으로 지정
        Vector3 target = WayPoint.Value[_currentWayIndex].transform.position;
        //타겟을 바라보도록 지정
        if ((target.x > Self.Value.transform.position.x))
        {
            Self.Value.transform.rotation = Quaternion.identity;
        }
        else
        {
            Self.Value.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //타겟을 향해 이동
        Self.Value.transform.position = Vector3.MoveTowards(Self.Value.transform.position, target, EnemyController.Value.enemyModel.MoveSpeed * Time.deltaTime);

        //타겟과 일정 거리 가까워지면 타겟 변경
        if (Vector3.Distance(Self.Value.transform.position, target) < 0.1f)
        {
            _currentWayIndex = (_currentWayIndex + 1) % WayPoint.Value.Count;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

