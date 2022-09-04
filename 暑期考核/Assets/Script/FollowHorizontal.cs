using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHorizontal : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    private Vector3 stratposition;
    public float speed;
    Vector3 travel => target.position - stratposition;
    void Start()
    {
        offset = target.position - transform.position;
        stratposition = transform.position;

    }

    void Update()
    {
        // transform.position = new Vector3(target.position.x + offset.x, transform.position.y, 0);
        transform.position = stratposition + new Vector3(travel.x * speed, travel.y * speed, 0);
    }
}
