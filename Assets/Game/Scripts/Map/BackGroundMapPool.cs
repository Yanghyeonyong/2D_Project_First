using UnityEngine;

public class BackGroundMapPool : MonoBehaviour
{
    //일반 맵
    [SerializeField] GameObject[] normalStage;
    //중간보스 맵
    [SerializeField] GameObject[] bossStage;
    //일반 맵 이전 위치
    public static Vector3 nPrevPos = new Vector3(0,-50,0);
    //보스 맵 이전 위치
    public static Vector3 prevPos = new Vector3(0,-17,0);
    //보스맵을 소환한거라면 true
    public bool isBossStage = false;
    //현재의 일반 맵(일반 맵은 처음부터 하나 소환된 상태여야 하기에 SerializeField 사용)
    [SerializeField] static GameObject curNormalStage;
    //현재의 보스 맵
    static GameObject curBossStage;
    //이미 한 번 플레이어와 닿았는지 체크 
    float usingCheck = 0;
    //시작시의 일반 맵이면 체크
    public bool startCurNormalStage=false;

    //몬스터들 정보
    [SerializeField] EnemyController[] enemies;
    //해당 구간 넘어갈 때마다 몬스터 성장 비율
    [SerializeField] float upgradeRate;
    //처음부터 성장하면 안되므로
    bool isFirst = true;

    private void Start()
    {
        //처음 시작 시의 일반 맵 설정
        if (startCurNormalStage)
        {
            curNormalStage = gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어 충돌시
        if (collision.gameObject.layer == 8)
        {
            //이미 한 번 충돌했으면 return
            if(usingCheck == transform.position.y)
                return;
            //미사용시 사용했다고 체크
            usingCheck = transform.position.y;
            if (!isBossStage)
            {
                //몬스터들 업그레이드
                foreach (var enemy in enemies)
                {
                    enemy.EnemyUpgrade(upgradeRate);
                }
                //일반 맵이 존재(활성화 상태)일 경우 이전 위치 정보에 해당 맵의 위치 저장 및 비활성화 
                if (curNormalStage != null)
                {
                    nPrevPos = curNormalStage.transform.position;
                    curNormalStage.SetActive(false);
                }
                //맵들 중 랜덤하게 생성
                int random = Random.Range(0, normalStage.Length);
                //현 일반 맵은 앞서 생성한 랜덤 인덱스 기반 맵
                curNormalStage = normalStage[random];
                //맵 위치 이동 및 활성화
                curNormalStage.transform.position = nPrevPos + new Vector3(0, 60f, 0);
                normalStage[random].SetActive(true);

            }
            else
            {
                //처음부터 보스몹 스탯 오르는 것 방지
                if (isFirst)
                {
                    isFirst = false;
                }
                //처음이 아니라면 스탯 올림
                else
                {
                    foreach (var enemy in enemies)
                    {
                        enemy.EnemyUpgrade(upgradeRate);
                    }
                }
                //보스 맵이 존재(활성화 상태)일 경우 이전 위치 정보에 해당 맵의 위치 저장 및 비활성화
                if (curBossStage != null)
                {
                    prevPos = curBossStage.transform.position;
                    curBossStage.SetActive(false);
                }
                //보스맵중 랜덤하게 생성
                int random=Random.Range(0, bossStage.Length);
                curBossStage = bossStage[random];
                //위치 이동 및 활성화
                curBossStage.transform.position = prevPos + new Vector3(0, 60f, 0);
                bossStage[random].SetActive(true);



            }

        }
    }
}
