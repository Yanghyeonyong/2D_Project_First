using UnityEngine;

public class Shopping : MonoBehaviour
{
    //능력시 상승량
    [SerializeField] float[] upgradeRate;
    //능력치 상승에 필요 골드
    [SerializeField] int[] upgradeGold;
    //상점 페이지
    [SerializeField] GameObject shopPage;
    bool shopOpen = false;

    //상점 열기 닫기
    public void OnOffShop()
    {
        shopOpen = UIManager.Instance.OnOffUI(shopPage, shopOpen);
    }

    //각 능력치 골드를 소비하여 업그레이드
    public void HpUpgrade()
    {
        if (!GameManager.Instance.playerModel.BuyStatus(upgradeGold[0]))
        {
            Debug.Log("소지금이 부족합니다");
            return;
        }
        GameManager.Instance.playerModel.ChangeStatus(0, upgradeRate[0]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel);
    }
    public void MpUpgrade()
    {
        if (!GameManager.Instance.playerModel.BuyStatus(upgradeGold[1]))
            return;
        GameManager.Instance.playerModel.ChangeStatus(1, upgradeRate[1]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel);
    }
    public void DefenceUpgrade()
    {
        if (!GameManager.Instance.playerModel.BuyStatus(upgradeGold[2]))
            return;
        GameManager.Instance.playerModel.ChangeStatus(2, upgradeRate[2]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel);
    }
    public void DamageUpgrade()
    {
        if (!GameManager.Instance.playerModel.BuyStatus(upgradeGold[3]))
            return;
        GameManager.Instance.playerModel.ChangeStatus(3, upgradeRate[3]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel);
    }
    public void AttackRangeUpgrade()
    {
        if (!GameManager.Instance.playerModel.BuyStatus(upgradeGold[4]))
            return;
        GameManager.Instance.playerModel.ChangeStatus(4, upgradeRate[4]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel);
    }
    public void MoveSpeedUpgrade()
    {
        if (!GameManager.Instance.playerModel.BuyStatus(upgradeGold[5]))
            return;
        GameManager.Instance.playerModel.ChangeStatus(5, upgradeRate[5]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel);
    }
    public void JumpForceUpgrade()
    {
        if (!GameManager.Instance.playerModel.BuyStatus(upgradeGold[6]))
            return;
        GameManager.Instance.playerModel.ChangeStatus(6, upgradeRate[6]);
        GameManager.Instance.playerView.UpdateStatus(GameManager.Instance.playerModel);
    }
}
