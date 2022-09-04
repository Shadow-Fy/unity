using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO currentdata;
    bool cantalk = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentdata != null)
        {
            cantalk = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cantalk = false;
            DialogueUI.Instance.dialoguepanel.SetActive(false);
        }
    }

    void Update()
    {
        if (cantalk && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }
    }

    void OpenDialogue()
    {
        DialogueUI.Instance.UpdateDialogueData(currentdata);
        DialogueUI.Instance.UpdateMainDialogue(currentdata.dialoguepieces[0]);
    }
}
