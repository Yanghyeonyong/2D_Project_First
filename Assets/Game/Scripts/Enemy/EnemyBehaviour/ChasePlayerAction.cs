using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

//using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChasePlayer", story: "[Self] Chase Player with [NavMeshAgent]", category: "Action", id: "a901c68b2f4466338195a81d1b984d79")]
public partial class ChasePlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<UnityEngine.AI.NavMeshAgent> NavMeshAgent;
    GameObject player;
    protected override Status OnStart()
    {
        if (player == null)
        {
            player = GameObject.FindFirstObjectByType<PlayerController_State>().gameObject;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        //플레이어를 바라보도록 각도 설정
        if ((player.transform.position.x > Self.Value.transform.position.x))
        {
            Self.Value.transform.rotation = Quaternion.identity;
        }
        else
        {
            Self.Value.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        //NavMeshAgent를 통한 실시간 플레이어 추적
        NavMeshAgent.Value.SetDestination(player.transform.position+ Vector3.up);

        return Status.Success;
    }
}

