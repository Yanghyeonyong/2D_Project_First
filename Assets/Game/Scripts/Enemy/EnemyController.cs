using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyView enemyView;
    public EnemyModel enemyModel;
    private void OnEnable()
    {
        enemyModel = new EnemyModel(1000, 100);
        EnemyInit();
    }
    private void EnemyInit()
    {
        enemyModel.Init();
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
            gameObject.SetActive(false);
        }
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
            Debug.Log("찾았다");
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
            if (spawnBullet == null)
            {
                spawnBullet=StartCoroutine(SpawnBullet());
            }
        }
        else
        {
            if (spawnBullet != null)
            {
                StopCoroutine(spawnBullet);
            }
                spawnBullet = null;
        }
    }

    Coroutine spawnBullet;
    IEnumerator SpawnBullet()
    {
        while (true)
        {

            GameObject mybullet = Instantiate(bullet, transform.position, transform.rotation);
            //mybullet.transform.SetParent(null);
            yield return new WaitForSeconds(1f);
        }
    }
}
