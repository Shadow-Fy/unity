using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFollow : MonoBehaviour
{
    public bool ispoison, isfire;
    // Start is called before the first frame update


    // Update is called once per frame
    private void Update()
    {

        if(ispoison)
        transform.transform.position = new Vector2(BuffManager.Instance.player.position.x + 0.1f, BuffManager.Instance.player.position.y + 0.45f);
        if(isfire)
        transform.transform.position = new Vector2(BuffManager.Instance.player.position.x - 0.15f, BuffManager.Instance.player.position.y + 0.45f);
        DestroyThis();
    }


    public void DestroyThis()
    {
        if (ispoison)
        {
            if (BuffManager.Instance.poisoncount == BuffManager.Instance.maxpoisoncount)
            {
                ObjectPool.Instance.PushObject(gameObject);
            }
        }

        if (isfire)
        {
            if (BuffManager.Instance.firecount == BuffManager.Instance.maxfirecount)
            {
                ObjectPool.Instance.PushObject(gameObject);
            }
        }
    }
}
