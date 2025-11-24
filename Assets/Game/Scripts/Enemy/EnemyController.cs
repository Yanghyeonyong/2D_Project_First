using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyView enemyView;
    private EnemyModel enemyModel;
    private void OnEnable()
    {
        enemyModel = new EnemyModel(1000);
        EnemyInit();
    }
    private void EnemyInit()
    {
        enemyModel.Init();
    }

    public void OnTakeDamage(float takeDamage)
    {
        enemyModel.TakeDamage(takeDamage);
        enemyView.UpdateEnemyHP(enemyModel.CurHp/enemyModel.MaxHp);
    }
}
