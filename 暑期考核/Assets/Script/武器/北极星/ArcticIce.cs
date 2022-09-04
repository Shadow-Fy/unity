using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcticIce : MonoBehaviour
{
    private float cdtime = 3;
    public GameObject flower_1prefeb;
    public GameObject flower_2prefeb;
    public GameObject flower_3prefeb;
    private Rigidbody2D rb;
    private Vector2 direction;
    float flowertime = 0.1f;
    int count;

    // Start is called before the first frame update
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        cdtime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(rb.velocity.x, rb.velocity.y).normalized;
        transform.up = direction;
        cdtime -= Time.deltaTime;
        if (cdtime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }

        flowertime -= Time.deltaTime;
        if (flowertime <= 0)
        {
            count = Random.Range(1, 4);
            switch (count)
            {
                case 1:
                    Flower_1();
                    break;
                case 2:
                    Flower_2();
                    break;
                case 3:
                    Flower_3();
                    break;

            }
            flowertime = 0.1f;
        }
    }

    void Flower_1()
    {
        GameObject flower_1 = ObjectPool.Instance.GetObject(flower_1prefeb);
        flower_1.transform.position = transform.position;
    }
    void Flower_2()
    {
        GameObject flower_2 = ObjectPool.Instance.GetObject(flower_2prefeb);
        flower_2.transform.position = transform.position;
    }
    void Flower_3()
    {
        GameObject flower_3 = ObjectPool.Instance.GetObject(flower_3prefeb);
        flower_3.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.tag == "Enemy")
        {
            other.GetComponent<IDamageable>().GetHit(30);


            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
