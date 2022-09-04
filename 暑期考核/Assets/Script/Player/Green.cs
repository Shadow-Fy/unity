using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : MonoBehaviour
{

    private float lifetime = 4;
    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }

    public void Move()//飞行
    {
        lifetime -= Time.deltaTime;

        rb.velocity =  transform.right * 40;/* 飞行速度 */
        if (lifetime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
            lifetime = 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")  || other.CompareTag("Enemy"))
        {
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            // other.GetComponent<IDamageable>().GetHit(GameManager.Instance.playerstats.weaponDamage);
            other.GetComponent<IDamageable>().GetHit(20);
        }

    }
}
