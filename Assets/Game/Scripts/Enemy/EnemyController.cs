using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyView enemyView;
    private EnemyModel enemyModel;
    private void OnEnable()
    {
        enemyModel = new EnemyModel(1000);
        EnemyInit();
    }
    private void EnemyInit()
    {
        enemyModel.Init();
    }

    public void OnTakeDamage(float takeDamage)
    {
        enemyModel.TakeDamage(takeDamage);
        enemyView.UpdateEnemyHP(enemyModel.CurHp/enemyModel.MaxHp);
    }

    public LayerMask layerMask;
    float attackPosY = 1.3f;
    float attackBoxSize = 6f;
    float attackRange = 5f;

    //플레이어가 레이 범위에 있는지 체크
    private bool CheckPlayer()
    {
        Debug.Log("찾는 중");
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            new Vector2(1, attackBoxSize),
            0, transform.right,
            attackRange, layerMask);


        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public GameObject bullet;
    private void Update()
    {
        if (CheckPlayer())
        {
            GameObject mybullet= Instantiate(bullet, transform);
            mybullet.transform.SetParent(null);
        }
    }
}
