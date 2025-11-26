using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{

    public EnemyModel enemyModel;
    [SerializeField] private EnemyView enemyView;

    public GameObject bullet;

    [SerializeField] Transform spawnPos;
    public Transform SpawnPos =>spawnPos;

    GameObject player;

    SpriteRenderer spriteRenderer;
    Color originColor;

    [SerializeField] bool isDie = false;
    public bool IsDie => isDie;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        EnemyInit();
    }
    private void EnemyInit()
    {
        enemyModel.Init();
        enemyView.UpdateEnemyHP(enemyModel.CurHp / enemyModel.MaxHp);
        spriteRenderer.color = originColor;
        isDie = false ;
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

            //gameObject.SetActive(false);
            StartCoroutine(DieAnimation());
        }
    }
    [SerializeField] float dieAnimationTime = 1f;
    IEnumerator DieAnimation()
    {
        isDie = true;
        float timer = 0f;
        while (timer <= dieAnimationTime)
        {
            Debug.Log($"°¨¼Ú°ª : {spriteRenderer.color.a - 255f / dieAnimationTime / 10}");
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, 
                spriteRenderer.color.b, Mathf.Max(0,spriteRenderer.color.a - 1/dieAnimationTime/10));
            Debug.Log($"a°ª : {spriteRenderer.color.a}");
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        gameObject.SetActive(false);
    }
}
