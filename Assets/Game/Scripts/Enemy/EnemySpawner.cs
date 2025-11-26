using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //MapPool 체크시 몬스터 스폰 이전 몬스터들은 모두 Set Active False
    //그냥 해당 객체가 OnEnable일 경우 스폰하면 되겠는데, Disable일 경우 모두 끄고
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        Debug.Log("test");
    }
}
