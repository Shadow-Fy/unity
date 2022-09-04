using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFire : MonoBehaviour
{
    public GameObject stoneprefeb;
    public float cdtime;
    public float origintime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cdtime -= Time.deltaTime;
        if (cdtime <= 0)
        {
            Attack();
        }
    }


    public void Attack()
    {
        GameObject stone = ObjectPool.Instance.GetObject(stoneprefeb);
        stone.transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        cdtime = origintime;
    }

}
