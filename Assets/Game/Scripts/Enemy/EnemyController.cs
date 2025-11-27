using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //몬스터의 데이터
    public EnemyModel enemyModel;
    [SerializeField] private EnemyView enemyView;

    //투사체 종류
    public GameObject bullet;

    //투사체 소환 위치
    [SerializeField] Transform spawnPos;
    public Transform SpawnPos =>spawnPos;

    //플레이어
    GameObject player;

    //객체의 스프라이트 렌더러
    SpriteRenderer spriteRenderer;
    Color originColor;

    //몬스터 사방 여부 판단
    [SerializeField] bool isDie = false;
    public bool IsDie => isDie;


    //효과음
    [SerializeField] AudioClip[] effectAudios;
    public AudioClip[] EffectAudios => effectAudios;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        EnemyInit();
    }

    //데이터 초기화
    private void EnemyInit()
    {
        enemyModel.Init();
        enemyView.UpdateEnemyHP(enemyModel.CurHp / enemyModel.MaxHp);
        spriteRenderer.color = originColor;
        isDie = false ;
    }

    //플레이어에게 피격 시 HP 감소, HP가 0일 경우 Die 코루틴 실행
    public void OnTakeDamage(float takeDamage)
    {
        SoundManager.Instance.PlayEffect(effectAudios[1]);
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

            StartCoroutine(DieAnimation());
        }
    }

    //애니메이션 지속 시간
    [SerializeField] float dieAnimationTime = 1f;
    //사망 시 점차 투명해지는 코루틴
    IEnumerator DieAnimation()
    {
        isDie = true;
        float timer = 0f;
        while (timer <= dieAnimationTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, 
            spriteRenderer.color.b, Mathf.Max(0,spriteRenderer.color.a - 1/dieAnimationTime/10));
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        gameObject.SetActive(false);
    }

    //객체 능력치 강화
    public void EnemyUpgrade(float upgradeRate)
    {
        enemyModel.EnemyUpgrade(upgradeRate);
    }

    //플레이어 충돌 시 플레이어에게 데미지
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<PlayerController_State>().OnTakeDamage(enemyModel.Damage);
        }
    }
}
