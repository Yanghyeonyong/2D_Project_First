using System.Collections;
using Unity.Hierarchy;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float bulletSpeed;
    [SerializeField] int bulletIndex;

    public void SetBullet(float dmg, float spd)
    { 
        damage = dmg;
        bulletSpeed = spd;
    }

    // Update is called once per frame
    void Update()
    {
        if (damage != 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);
        }
    }

    Coroutine playerAttakck;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && bulletIndex != 6)
        {
            collision.gameObject.GetComponent<PlayerController_State>().OnTakeDamage(damage);
            BulletManager.Instance.SetBullet(gameObject, bulletIndex);
        }
        else if (collision.gameObject.layer == 6 && bulletIndex == 6)
        {
            collision.gameObject.GetComponent<EnemyController>().OnTakeDamage(damage);
            BulletManager.Instance.SetBullet(gameObject, bulletIndex);
        }
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 9)
        {
            BulletManager.Instance.SetBullet(gameObject, bulletIndex);
        }
    }

}
