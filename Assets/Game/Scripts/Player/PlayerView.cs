using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] statusTexts;
    [SerializeField] Image hpBanner;
    [SerializeField] Image mpBanner;
    [SerializeField] GameObject levelUpPage;

    //HP, MP 표시
    public void UpdatePlayerHP(float amount)
    {
        hpBanner.fillAmount = amount;
    }
    public void UpdatePlayerMP(float amount)
    {
        mpBanner.fillAmount = amount;
    }

    //모델 기반 스탯 작성
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
    //탑에서의 능력치 작성
    public void UpdateStatus(PlayerModel playerModel, PlayerModel_Dongeon playerModel_Dongeon)
    {
        statusTexts[0].text = $"HP: {playerModel.CurHp} / {playerModel.MaxHp}";
        statusTexts[1].text = $"MP: {playerModel.CurMp} / {playerModel.MaxMp}";
        statusTexts[2].text = $"Def: {playerModel.Defence} + {playerModel_Dongeon.Defence}";
        statusTexts[3].text = $"Att: {playerModel.Damage} + {playerModel_Dongeon.Damage}";
        statusTexts[4].text = $"AttackRange: {playerModel.AttackRange} + {playerModel_Dongeon.AttackRange}";
        statusTexts[5].text = $"Speed: {playerModel.MoveSpeed} + {playerModel_Dongeon.MoveSpeed}";
        statusTexts[6].text = $"Jump: {playerModel.JumpForce} + {playerModel_Dongeon.JumpForce}";
        statusTexts[7].text = $"{playerModel.Gold}$";
    }

    public void LevelUpPageOpen(int levelUpCount)
    {
        StartCoroutine(LevelUp(levelUpCount));
    }

    IEnumerator LevelUp(int levelUpCount)
    {
        //능력치 선택 전까지 코루틴 계속 진행, 무적 상태 지속
        while (levelUpCount > 0)
        {
            Debug.Log(levelUpCount+"번째");
            yield return new WaitUntil(() => !levelUpPage.activeSelf);
            levelUpPage.SetActive(true);
            levelUpCount--;
        }
        yield return new WaitUntil(() => !levelUpPage.activeSelf);
        GameManager.Instance.IsInvincible = false;
    }



    [SerializeField] GameObject[] dieObject;
    [SerializeField] Image fadeImage;
    [SerializeField] TextMeshProUGUI fadeText;

    //사망시 재생하는 페이드아웃
    public IEnumerator DieAnimation(float dieAnimationTime)
    {
        foreach (GameObject obj in dieObject)
        {
            obj.SetActive(true);
        }

        GameManager.Instance.IsInvincible = true;
        float timer = 0f;
        while (timer <= dieAnimationTime)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,
            fadeImage.color.b, Mathf.Min(255, fadeImage.color.a + 1 / dieAnimationTime / 10));

            fadeText.color = new Color(fadeText.color.r, fadeText.color.g,
fadeText.color.b, Mathf.Min(255, fadeText.color.a + 1 / dieAnimationTime / 10));
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        //StartCoroutine(GameManager.Instance.MoveScene(0, 1));
        GameManager.Instance.StartCoroutine(GameManager.Instance.MoveScene(0, 1));
    }

}
