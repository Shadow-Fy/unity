using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float lifetime;
    public float origintime;
    // Start is called before the first frame update
    void OnEnable()
    {
        lifetime = origintime;
    }


    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(10);
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.CompareTag("PlayerAttack"))
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
