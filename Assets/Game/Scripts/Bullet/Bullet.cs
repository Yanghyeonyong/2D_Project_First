using System.Collections;
using Unity.Hierarchy;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float bulletSpeed;
    [SerializeField] int bulletIndex;

    //데미지와 속도 세팅
    public void SetBullet(float dmg, float spd)
    { 
        damage = dmg;
        bulletSpeed = spd;
    }

    //우측으로 등속 이동
    void Update()
    {
        if (damage != 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);
        }
    }

    //특정 레이어를 가진 객체와 충돌시 이벤트 발생
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && bulletIndex != 6)
        {
            //플레이어에게 데미지
            collision.gameObject.GetComponent<PlayerController_State>().OnTakeDamage(damage);
            //객체 비활성화 및 큐에 추가
            BulletManager.Instance.SetBullet(gameObject, bulletIndex);
        }
        else if (collision.gameObject.layer == 6 && bulletIndex == 6)
        {
            //몬스터에게 데미지
            collision.gameObject.GetComponent<EnemyController>().OnTakeDamage(damage);
            BulletManager.Instance.SetBullet(gameObject, bulletIndex);
        }
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 9)
        {
            //벽이나 땅에 충돌 시
            BulletManager.Instance.SetBullet(gameObject, bulletIndex);
        }
    }

}
