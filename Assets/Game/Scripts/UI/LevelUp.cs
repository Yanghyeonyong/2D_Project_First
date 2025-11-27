using UnityEngine;

public class LevelUp : MonoBehaviour
{
    //능력치 선택 카드
    [SerializeField] GameObject[] levelUpStatus;
    //능력치 선택 카드 위치
    [SerializeField] Transform[] levelUpPos;
    //각 능력치별 레벨업시 스탯 상승량
    [SerializeField] float[] upgradeRate;
    //능력치 선택 패널 
    [SerializeField] GameObject leveUpPage;
    private void OnEnable()
    {
        //모든 능력치 카드 중 levelUpPos.Length개를 반환 및 이 중 하나 선택 가능
        int returnCount = 0;
        foreach (GameObject levelUp in levelUpStatus)
        {
            levelUp.SetActive(false);
        }
        while (returnCount < levelUpPos.Length)
        {
            int random = Random.Range(0, levelUpStatus.Length);
            if (!levelUpStatus[random].activeSelf)
            {
                levelUpStatus[random].SetActive(true);
                levelUpStatus[random].transform.position = levelUpPos[returnCount].position;
                returnCount++;
            }
        }
    }

    //각 능력치 업그레이드
    public void DefenceUpgrade()
    {
        GameManager.Instance.playerModel_Dongeon.ChangeStatus(0, upgradeRate[0]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel,GameManager.Instance.playerModel_Dongeon);
        leveUpPage.SetActive(false);
    }
    public void DamageUpgrade()
    {
        GameManager.Instance.playerModel_Dongeon.ChangeStatus(1, upgradeRate[1]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel, GameManager.Instance.playerModel_Dongeon);
        leveUpPage.SetActive(false);
    }

    public void AttackRangeUpgrade()
    {
        GameManager.Instance.playerModel_Dongeon.ChangeStatus(2, upgradeRate[2]);
    GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel, GameManager.Instance.playerModel_Dongeon);
        leveUpPage.SetActive(false);
    }
    public void MoveSpeedUpgrade()
    {

        GameManager.Instance.playerModel_Dongeon.ChangeStatus(3, upgradeRate[3]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel, GameManager.Instance.playerModel_Dongeon);
        leveUpPage.SetActive(false);
    }
    public void JumpForceUpgrade()
    {
        GameManager.Instance.playerModel_Dongeon.ChangeStatus(4, upgradeRate[4]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel, GameManager.Instance.playerModel_Dongeon);
        leveUpPage.SetActive(false);

    }
}
