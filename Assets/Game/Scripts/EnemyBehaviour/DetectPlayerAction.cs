using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEditor.Experimental.GraphView;
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
        if (player == null)
            return Status.Failure;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (EnemyController.Value.IsDie || GameManager.Instance.IsInvincible)
            return Status.Failure;
        //float distance = Vector2.Distance(Self.Value.transform.position, Player.Value.transform.position);
        float distance = Vector2.Distance(Self.Value.transform.position, player.transform.position);

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
        //return distance <= EnemyController.Value.enemyModel.DetectRange ? Status.Success : Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

