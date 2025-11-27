using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    //해당 객체가 활성화 시 배열에 담긴 몬스터들 모두 활성화
    private void OnEnable()
    {
        foreach (var enemy in Enemies)
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);
            }
        }
    }
}
