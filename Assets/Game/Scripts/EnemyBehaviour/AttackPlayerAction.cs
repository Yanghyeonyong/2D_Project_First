using System;
using Unity.Behavior;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackPlayer", story: "[Self] Attack Player With [EnemyController]", category: "Action", id: "d52963fc5477954b50a3c18922882081")]
public partial class AttackPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<EnemyController> EnemyController;
    GameObject player;

    protected override Status OnStart()
    {
        if (player == null)
        {
            player = GameObject.FindFirstObjectByType<PlayerController_State>().gameObject;
            Debug.Log("플레이어 찾기 attack");
        }
        return Status.Running;
    }
    //Coroutine spawnBullet;
    protected override Status OnUpdate()
    {
        SpawnBullet();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }



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


    float curTime = 0;
    public void SpawnBullet()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            if (CheckPlayer())
            {
                Vector3 dir = player.transform.position - EnemyController.Value.SpawnPos.transform.position + Vector3.up;
                //Quaternion rot = Quaternion.LookRotation(dir);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                // 3. Z축 회전만 적용된 쿼터니언 생성
                // Z축을 중심으로 angle만큼 회전 (X, Y는 0)
                Quaternion rot = Quaternion.Euler(0f, 0f, angle);
                GameObject mybullet = UnityEngine.Object.Instantiate(EnemyController.Value.bullet, EnemyController.Value.SpawnPos.transform.position, rot);
                mybullet.GetComponent<Bullet>().SetBullet(EnemyController.Value.enemyModel.Damage, EnemyController.Value.enemyModel.BulletSpeed);
                curTime = EnemyController.Value.enemyModel.AttackSpeed;
            }
        }
        //curTime += Time.deltaTime;
        //if (curTime >= EnemyController.Value.enemyModel.AttackSpeed)
        //{
        //    if (CheckPlayer())
        //    {
        //        Vector3 dir = player.transform.position - EnemyController.Value.SpawnPos.transform.position + Vector3.up;
        //        //Quaternion rot = Quaternion.LookRotation(dir);
        //        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //        // 3. Z축 회전만 적용된 쿼터니언 생성
        //        // Z축을 중심으로 angle만큼 회전 (X, Y는 0)
        //        Quaternion rot = Quaternion.Euler(0f, 0f, angle);
        //        GameObject mybullet = UnityEngine.Object.Instantiate(EnemyController.Value.bullet, EnemyController.Value.SpawnPos.transform.position, rot);
        //        mybullet.GetComponent<Bullet>().SetBullet(EnemyController.Value.enemyModel.Damage, EnemyController.Value.enemyModel.BulletSpeed);
        //        curTime = 0;
        //    }
        //}
    }
}

