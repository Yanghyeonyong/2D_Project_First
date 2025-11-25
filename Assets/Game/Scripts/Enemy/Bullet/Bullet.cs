using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime);
    }

    Coroutine playerAttakck;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==8)
        {
            Debug.Log("´ê¾Ò´Ù.");
            collision.gameObject.GetComponent<PlayerController_State>().OnTakeDamage(13f);
        }
    }

}
