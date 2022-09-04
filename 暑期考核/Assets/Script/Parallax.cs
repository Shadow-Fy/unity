using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moverateX, moverateY;
    private float startpointx, startpointy;
    void Start()
    {
        startpointx = transform.position.x;
        startpointy = transform.position.y;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        transform.position = new Vector2(startpointx + Cam.position.x * moverateX, startpointy + Cam.position.y * moverateY);

    }
}
