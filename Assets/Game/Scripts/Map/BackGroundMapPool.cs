using UnityEngine;

public class BackGroundMapPool : MonoBehaviour
{
    [SerializeField] GameObject[] normalStage;
    [SerializeField] GameObject[] bossStage;
    public static Vector3 nPrevPos = new Vector3(0,-50,0);
   public static Vector3 prevPos = new Vector3(0,-17,0);
    public bool isBossStage = false;
    [SerializeField] static GameObject curNormalStage;
    static GameObject curBossStage;
    float usingCheck = 0;
    public bool startCurNormalStage=false;
    private void Start()
    {
        if (startCurNormalStage)
        {
            curNormalStage = gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if(usingCheck == transform.position.y)
                return;
            usingCheck = transform.position.y;
            if (!isBossStage)
            {
                //backGroundMap.transform.position += new Vector3(0, 60f, 0);
                if (curNormalStage != null)
                {
                    Debug.Log("변경 전전 : " + nPrevPos);
                    nPrevPos = curNormalStage.transform.position;
                    curNormalStage.SetActive(false);
                    Debug.Log("변경 전 : "+ nPrevPos);
                }
                int random = Random.Range(0, normalStage.Length);
                normalStage[random].SetActive(true);
                curNormalStage = normalStage[random];
                curNormalStage.transform.position = nPrevPos + new Vector3(0, 60f, 0);
                Debug.Log("변경 후 : " + curNormalStage.transform.position);
            }
            else
            {
                if (curBossStage != null)
                {
                    prevPos = curBossStage.transform.position;
                    curBossStage.SetActive(false);
                }
                int random=Random.Range(0, bossStage.Length);
                bossStage[random].SetActive(true);

                curBossStage = bossStage[random];
                curBossStage.transform.position = prevPos + new Vector3(0, 60f, 0);
            }

        }
    }
}
