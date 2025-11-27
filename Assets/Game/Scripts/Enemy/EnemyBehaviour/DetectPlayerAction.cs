using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DetectPlayer", story: "[Self] Detect Player with [EnemyController] And [NavMeshAgent]", category: "Action", id: "7aeeb37e0453511fe936cf2e3c4954b6")]
public partial class DetectPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<EnemyController> EnemyController;
    [SerializeReference] public BlackboardVariable<UnityEngine.AI.NavMeshAgent> NavMeshAgent;
    GameObject player;

    protected override Status OnStart()
    {
        if (player == null)
        {
            player = GameObject.FindFirstObjectByType<PlayerController_State>()?.gameObject;
        }
        //플레이어 못찾으면 이후 sequence들은 진행하지 말아라
        if (player == null)
            return Status.Failure;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        //몬스터가 죽었거나 플레이어가 무적상태일 경우 이후 sequence들은 진행하지 말아라
        if (EnemyController.Value.IsDie || GameManager.Instance.IsInvincible)
            return Status.Failure;
        
        //몬스터와 플레이어 거리 
        float distance = Vector2.Distance(Self.Value.transform.position, player.transform.position);

        //거리가 탐지 거리 이상일 경우 NavMeshAgent의 추적을 정지하라
        if (distance <= EnemyController.Value.enemyModel.DetectRange)
        {
            NavMeshAgent.Value.isStopped = false;
            return Status.Success;
        }
        else
        {
            NavMeshAgent.Value.isStopped = true;
            return Status.Failure;
        }
        
    }

    protected override void OnEnd()
    {
    }
}

