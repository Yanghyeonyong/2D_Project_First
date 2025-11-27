using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
    private static BulletManager instance;
    public static BulletManager Instance => instance;

    //0~8번까지 bullet의 종류별로 저장, bullet 종류는 bullet script에서 확인
    [SerializeField] List<Queue<GameObject>> bullets;

    private void Awake()
    {
        //싱글톤이긴 한데 DontDestroy는 아님. 그저 전역 객체 접근용
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        //리스트 초기화
        bullets = new List<Queue<GameObject>>();
        for (int i = 0; i < 10; i++)
        {
            bullets.Add(new Queue<GameObject>());
        }

    }

    //투사체 반환
    public GameObject GetBullet(int index)
    {
        if (bullets[index].Count == 0) 
            return null;
        return bullets[index].Dequeue();
    }

    //투사체 큐에 삽입
    public void SetBullet(GameObject bullet, int index)
    {
        bullets[index].Enqueue(bullet);
        bullet.SetActive(false);
    }
}
