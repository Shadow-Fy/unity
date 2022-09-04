using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{

    public Transform A, B;
    private Vector3 targetpoint;
    public Animator anim;
    private Rigidbody2D rb;
    private float speed = 2;
    public bool canmove;
    void Start()
    {
        targetpoint = A.transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canmove = true;
    }

    void Update()
    {
        A = gameObject.transform.parent.GetChild(1).transform;
        B = gameObject.transform.parent.GetChild(2).transform;
        MoveToTarget();
    }

    public void MoveToTarget()
    {


        if (Vector2.Distance(transform.position, targetpoint) >= 0.1f && canmove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetpoint, speed * Time.deltaTime);
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        FilpDirection();
    }

    public void FilpDirection()/* 改变方向 */
    {


        if (transform.position.x > targetpoint.x)/* 朝左 */
        {
            // direction = 1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else/* 朝右 */
        {
            // direction = -1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Vector2.Distance(transform.position, targetpoint) < 0.1f)
        {
            Invoke("SwitchPoint", 1.2f);
            anim.SetBool("run", false);
        }
    }


    public void SwitchPoint()
    {
        if (Mathf.Abs(A.transform.position.x - transform.position.x) > Mathf.Abs(B.transform.position.x - transform.position.x))
        {
            targetpoint = A.position;
        }
        else
        {
            targetpoint = B.position;
        }

    }

}
