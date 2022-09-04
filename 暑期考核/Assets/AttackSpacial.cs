using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpacial : MonoBehaviour
{
    public void ReBack()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
