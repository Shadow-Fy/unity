using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEnemy : MonoBehaviour
{
    public GameObject batprefeb;
    public GameObject manprefeb;
    public GameObject redprefeb;
    private bool cancall = false;

    public Transform A, B, C, D, E, F;

    private void Update()
    {
        if (cancall)
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject red1 = ObjectPool.Instance.GetObject(redprefeb);
                red1.transform.position = A.position;
                GameObject red2 = ObjectPool.Instance.GetObject(redprefeb);
                red2.transform.position = B.position;
                GameObject bat1 = ObjectPool.Instance.GetObject(batprefeb);
                bat1.transform.position = C.position;
                GameObject bat2 = ObjectPool.Instance.GetObject(batprefeb);
                bat2.transform.position = D.position;
                GameObject man1 = ObjectPool.Instance.GetObject(manprefeb);
                man1.transform.position = E.position;
                GameObject man2 = ObjectPool.Instance.GetObject(manprefeb);
                man2.transform.position = F.position;
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cancall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            cancall = false;
        }
    }
}
