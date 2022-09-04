using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpace : MonoBehaviour
{
    public int count = 0;
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            if (count == 1)/* 朝右边吹的风场 */
            {
                other.GetComponent<Rigidbody2D>().AddForce(new Vector3(10, 0f, 0f) * 10);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().AddForce(new Vector3(-10, 0f, 0f) * 10);
            }
    }
}
