using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSword : MonoBehaviour
{
    private float cdtime = 2;
    private Rigidbody2D rb;
    private Vector2 direction;
    // Start is called before the first frame update
    private void OnEnable()
    {
        // line.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        cdtime = 2;
    }


    // Update is called once per frame
    void Update()
    {
        cdtime -= Time.deltaTime;
        if (cdtime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }

        direction = new Vector2(rb.velocity.x, rb.velocity.y).normalized;
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Enemy")
        {
            other.GetComponent<IDamageable>().GetHit(10);
            ObjectPool.Instance.PushObject(gameObject);
            GameManager.Instance.playerstats.currentHealth +=10;
        }
    }
}
