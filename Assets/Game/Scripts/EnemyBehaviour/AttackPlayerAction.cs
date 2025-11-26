using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackPlayer", story: "[Self] Attack [Player] With [EnemyController]", category: "Action", id: "d52963fc5477954b50a3c18922882081")]
public partial class AttackPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<EnemyController> EnemyController;

    protected override Status OnStart()
    {
        return Status.Running;
    }
    //Coroutine spawnBullet;
    protected override Status OnUpdate()
    {
        //if (CheckPlayer())
        //{
        //    if (spawnBullet == null)
        //    {
        //        spawnBullet = StartCoroutine(SpawnBullet());
        //    }
        //}
        //else
        //{
        //    if (spawnBullet != null)
        //    {
        //        StopCoroutine(spawnBullet);
        //    }
        //    spawnBullet = null;
        //}
        SpawnBullet();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }



    private bool CheckPlayer()
    {
        Debug.Log("찾는 중");
        RaycastHit2D hit = Physics2D.BoxCast(
            Self.Value.transform.position,
            new Vector2(1, EnemyController.Value.attackBoxSize),
            0, Self.Value.transform.right,
            EnemyController.Value.attackRange, EnemyController.Value.layerMask);


        if (hit.collider != null)
        {
            Debug.Log("찾았다");
            return true;
        }
        else
        {
            return false;
        }
    }

    //IEnumerator SpawnBullet()
    //{
    //    while (true)
    //    {

    //        GameObject mybullet = Instantiate(bullet, transform.position, transform.rotation);
    //        //mybullet.transform.SetParent(null);
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    float curTime = 0;
    public void SpawnBullet()
    {
        curTime += Time.deltaTime;
        if (curTime >= EnemyController.Value.attackDelay)
        {
            if (CheckPlayer())
            {
                GameObject mybullet = UnityEngine.Object.Instantiate(EnemyController.Value.bullet, EnemyController.Value.transform.position, EnemyController.Value.transform.rotation);
                curTime = 0;
            }
        }
    }
}

