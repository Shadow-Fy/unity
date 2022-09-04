using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArrow : MonoBehaviour
{
    public GameObject arrowprefeb;
    public bool left;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject arrow = ObjectPool.Instance.GetObject(arrowprefeb);
            if (left)
            {
                arrow.transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
                arrow.transform.up = transform.right;
            }
            else
            {
                arrow.transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
                arrow.transform.up = -transform.right;
            }
        }
    }

}
