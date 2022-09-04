using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private BoxCollider2D box;
    private Animator anim;
    public float cdtime;
    public float origintime;
    void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cdtime -= Time.deltaTime;
        if(cdtime <= 0)
        {
            Attack();
        }
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
        cdtime = origintime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(5);
        }
    }
}
