using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] statusTexts;


    public void UpdateStatus(PlayerModel playerModel)
    {
        statusTexts[0].text = $"HP: {playerModel.CurHp} / {playerModel.MaxHp}";
        statusTexts[1].text = $"MP: {playerModel.CurMp} / {playerModel.MaxMp}";
        statusTexts[2].text = $"Def: {playerModel.Defence}";
        statusTexts[3].text = $"Att: {playerModel.Damage}";
        statusTexts[4].text = $"AttackRange: {playerModel.AttackRange}";
        statusTexts[5].text = $"Speed: {playerModel.MoveSpeed}";
        statusTexts[6].text = $"Jump: {playerModel.JumpForce}";
        statusTexts[7].text = $"{playerModel.Gold}$";

    }
    //public void UpdateHpUI(float curHp, float maxHp)
    //{
    //    statusTexts[0].text = $"HP: {curHp} / {maxHp}";
    //}
    //public void UpdateMpUI(float curMp, float maxMp)
    //{
    //    statusTexts[1].text = $"MP: {curMp} / {maxMp}";
    //}
    //public void UpdateDefenceUI(float defence)
    //{
    //    statusTexts[2].text = $"defence: {defence}";
    //}
    //public void UpdateDamageUI(float damage)
    //{
    //    statusTexts[3].text = $"Damage: {damage}";
    //}
    //public void UpdateAttackRangeUI(float attackRange)
    //{
    //    statusTexts[4].text = $"AttackRange: {attackRange}";
    //}
    //public void UpdateMoveSpeedUI(float speed)
    //{
    //    statusTexts[5].text = $"Speed: {speed}";
    //}
    //public void UpdateJumpForceUI(float jumpForce)
    //{
    //    statusTexts[6].text = $"jump: {jumpForce}";
    //}
}
