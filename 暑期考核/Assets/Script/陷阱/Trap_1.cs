using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_1 : MonoBehaviour
{
    public Transform A, B;
    private LineRenderer line;

    private BoxCollider2D coll;
    public float cdtime;
    public float origintime;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        line = GetComponent<LineRenderer>();



        line.SetPosition(0, A.position);
        line.SetPosition(1, B.position);
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        coll.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        cdtime -= Time.deltaTime;
        if (cdtime <= 0)
        {
            Warning();

        }
    }

    public void Warning()
    {
        line.enabled = true;
        Invoke("Attack", 0.2f);
    }

    public void Attack()
    {
        coll.enabled = true;

        line.startWidth = 0.8f;
        line.endWidth = 0.8f;
        Invoke("Recover", 0.1f);
    }

    public void Recover()
    {
        coll.enabled = false;
        line.enabled = false;
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        cdtime = origintime;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BuffManager.Instance.OpenEyeBuff();
            other.GetComponent<IDamageable>().GetHit(20);
        }
    }
}
