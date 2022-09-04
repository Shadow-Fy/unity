using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcticFlower : MonoBehaviour
{
    private float cdtime = 2;
    private void OnEnable()
    {
        cdtime = 2;
    }
    void Update()
    {
        cdtime -= Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 1));
        if (cdtime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.tag == "Enemy")
        {
            other.GetComponent<IDamageable>().GetHit(15);


            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
