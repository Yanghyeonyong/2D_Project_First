using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] GameObject[] levelUpStatus;
    [SerializeField] Transform[] levelUpPos;
    [SerializeField] float[] upgradeRate;
    [SerializeField] GameObject leveUpPage;
    private void OnEnable()
    {
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
