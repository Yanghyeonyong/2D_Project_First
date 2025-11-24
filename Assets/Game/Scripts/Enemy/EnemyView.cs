using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] Image hpBanner;

    public void UpdateEnemyHP(float amount)
    {
        hpBanner.fillAmount = amount;
    }
}
