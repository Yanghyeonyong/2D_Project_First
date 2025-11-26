using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{

    public EnemyModel enemyModel;
    [SerializeField] private EnemyView enemyView;

    public LayerMask layerMask;
    public float attackPosY = 1.3f;
    public float attackBoxSize = 6f;
    public float attackRange = 5f;

    public GameObject bullet;

    [SerializeField] Transform spawnPos;
    public Transform SpawnPos =>spawnPos;


    GameObject player;

    private void OnEnable()
    {
        EnemyInit();
    }
    private void EnemyInit()
    {
        enemyModel.Init();
        enemyView.UpdateEnemyHP(enemyModel.CurHp / enemyModel.MaxHp);
    }

    public void OnTakeDamage(float takeDamage)
    {
        if (enemyModel.TakeDamage(takeDamage))
        {
            enemyView.UpdateEnemyHP(enemyModel.CurHp / enemyModel.MaxHp);
        }
        else
        {
            enemyView.UpdateEnemyHP(enemyModel.CurHp / enemyModel.MaxHp);
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController_State>().gameObject;
            }
            player.GetComponent<PlayerController_State>().KillMonster(enemyModel.Exp, enemyModel.Gold);

            gameObject.SetActive(false);
        }
    }

}
