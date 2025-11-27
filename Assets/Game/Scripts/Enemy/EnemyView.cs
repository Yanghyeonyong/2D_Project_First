using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] Image hpBanner;

    //HP 감소 UI에 표시
    public void UpdateEnemyHP(float amount)
    {
        hpBanner.fillAmount = amount;
    }
}
