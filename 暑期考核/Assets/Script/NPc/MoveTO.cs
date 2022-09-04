using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTO : MonoBehaviour
{
    public Transform target;
    public bool canmove;
    private NPCMove npc;
    // Start is called before the first frame update
    void Start()
    {
        npc = transform.GetChild(0).GetComponent<NPCMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove)
        {
            Debug.Log(1);
            Invoke("MoveTransform", 15f);

        }
    }

    public void MoveTransform()
    {

        transform.position = target.position;
        npc.SwitchPoint();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canmove = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canmove = false;
    }
}
