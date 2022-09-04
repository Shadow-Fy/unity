using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPointBase : MonoBehaviour
{
    private float destroytime = 0.3f;
    void OnEnable()
    {
        Invoke("DestroyFloat", destroytime);
    }




    void DestroyFloat()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
