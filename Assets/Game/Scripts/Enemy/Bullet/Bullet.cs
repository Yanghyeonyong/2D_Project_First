using System.Collections;
using Unity.Hierarchy;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float bulletSpeed;

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
        if (collision.gameObject.layer==8)
        {
            Debug.Log("´ê¾Ò´Ù.");
            collision.gameObject.GetComponent<PlayerController_State>().OnTakeDamage(damage);
        }
    }

}
