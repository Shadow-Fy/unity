using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCHeck : MonoBehaviour
{
    public NPCMove npcmove;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            npcmove.canmove = false;
            npcmove.anim.SetBool("run", false);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        npcmove.canmove = false;
        npcmove.anim.SetBool("run", false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            npcmove.canmove = true;
            npcmove.anim.SetBool("run", true);
        }
    }
}
