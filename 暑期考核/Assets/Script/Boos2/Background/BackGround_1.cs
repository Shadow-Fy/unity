using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_1 : MonoBehaviour
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
        transform.position += 0.2f * -transform.right;
        if (transform.position.x < -40f)
        {
            transform.position = new Vector2(95.4f, 0.2f);
        }
    }
}
