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
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        bullets = new List<Queue<GameObject>>();
        for (int i = 0; i < 10; i++)
        {
            bullets.Add(new Queue<GameObject>());
        }

    }

    public GameObject GetBullet(int index)
    {
        if (bullets[index].Count == 0) 
            return null;
        Debug.Log("불릿 가져옴");
        return bullets[index].Dequeue();
    }
    public void SetBullet(GameObject bullet, int index)
    {
        Debug.Log("불릿 추가");
        bullets[index].Enqueue(bullet);
        bullet.SetActive(false);
    }
}
