using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D coll;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetTrigger("collapse");
        }
    }

    void DisableCollider()
    {
        coll.enabled = false;
    }

    void DestroyTrapPlatform()
    {
        Destroy(gameObject);
    }
}
