using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
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
