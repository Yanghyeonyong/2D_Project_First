using UnityEngine;

public class Shopping : MonoBehaviour
{
    [SerializeField] float[] upgradeRate;
    public void HpUpgrade()
    {
        GameManager.Instance.playerModel.ChangeStatus(0, upgradeRate[0]);
    }
    public void MpUpgrade()
    {
        GameManager.Instance.playerModel.ChangeStatus(1, upgradeRate[1]);
    }
    public void DefenceUpgrade()
    {
        GameManager.Instance.playerModel.ChangeStatus(2, upgradeRate[2]);
    }
    public void DamageUpgrade()
    {
        GameManager.Instance.playerModel.ChangeStatus(3, upgradeRate[3]);
    }
    public void AttackRangeUpgrade()
    {
        GameManager.Instance.playerModel.ChangeStatus(4, upgradeRate[4]);
    }
    public void MoveSpeedUpgrade()
    {
        GameManager.Instance.playerModel.ChangeStatus(5, upgradeRate[5]);
    }
    public void JumpForceUpgrade()
    {
        GameManager.Instance.playerModel.ChangeStatus(6, upgradeRate[6]);
    }
}
