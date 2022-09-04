using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public GameObject line;
    private float cdtime = 3;
    private Rigidbody2D rb;
    private Vector2 direction;
    // Start is called before the first frame update
    private void OnEnable()
    {
        // line.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        cdtime = 3;
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
            other.GetComponent<IDamageable>().GetHit(30);
            ObjectPool.Instance.PushObject(gameObject);
            // line.SetActive(false);
        }
    }
}
