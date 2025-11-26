using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    public Transform player;//플레이어 추적하게
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(player.position);
    }

    private void Update()
    {
        if (agent != null)
        {
            if (player != null)
            {
                //특정 시간마다 호출하면 되긴 하는데 진짜엄청나게 정교한 이동 필요하면 업데이트에 넣어라
                agent.SetDestination(player.position);
            }
        }
    }
}
