using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_2 : MonoBehaviour
{
    // private Aim aim;


    void Start()
    {
        // aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Aim>();
    }
    void Update()
    {
        BackMove();
    }

    void BackMove()
    {
        // transform.position += 1.7f * aim.speed * -transform.up;
        transform.position += 0.1f * -transform.right;
        if (transform.position.x < -60f)
        {
            transform.position = new Vector2(131.1f, -0.3f);
        }
    }
}
