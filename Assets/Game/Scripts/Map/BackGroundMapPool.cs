using UnityEngine;

public class BackGroundMapPool : MonoBehaviour
{
    [SerializeField] GameObject backGroundMap;
    [SerializeField] GameObject[] bossStage;
    Vector3 prevPos = new Vector3(0,-17,0);
    public bool isBossStage = false;
    GameObject curBossStage;

    float usingCheck = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if(usingCheck == transform.position.y)
                return;
            usingCheck = transform.position.y;
            if (!isBossStage)
            {
                backGroundMap.transform.position += new Vector3(0, 60f, 0);
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
