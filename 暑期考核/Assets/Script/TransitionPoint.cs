using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{

    public enum TransitionType
    {
        samescene, differentscene
    }

    [Header("Transition Info")]
    public string scenename;
    public TransitionType transitiontype;
    public TransitionDestination.DestinationTag destinationtag;
    private bool cantrans;
    public bool angelboss;
    public bool boss;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cantrans && !angelboss)
        {
            SceneController.Instance.TransitionToDestination(this);
        }

        if (angelboss && cantrans)
        {
            SceneController.Instance.TransitionToDestination(this);
            angelboss = false;
        }
    }



    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !boss)
        {
            cantrans = true;
            SaveManager.Instance.SavePlayerData();
            InventoryManager.Instance.SaveData();
            QuestManager.Instance.SaveQuestManager();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cantrans = false;
        }
    }
}
