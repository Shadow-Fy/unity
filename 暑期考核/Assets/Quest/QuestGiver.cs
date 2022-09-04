using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class QuestGiver : MonoBehaviour
{
    DialogueController controller;
    QuestData_SO currentquest;

    public DialogueData_SO startdialogue;
    public DialogueData_SO progressdialogue;
    public DialogueData_SO completedialogue;
    public DialogueData_SO finisheddialogue;

    public bool isStarted
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentquest))
            {
                return QuestManager.Instance.GetTask(currentquest).isStarted;
            }
            else return false;
        }
    }

    public bool isComplete
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentquest))
            {
                return QuestManager.Instance.GetTask(currentquest).isComplete;
            }
            else return false;
        }
    }

    public bool isFinished
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentquest))
            {
                return QuestManager.Instance.GetTask(currentquest).isFinished;
            }
            else return false;
        }
    }



    void Awake()
    {
        controller = GetComponent<DialogueController>();
    }

    void Start()
    {
        controller.currentdata = startdialogue;
        currentquest = controller.currentdata.GetQuest();
    }

    void Update()
    {
        if (isStarted)
        {
            if (isComplete)
            {
                controller.currentdata = completedialogue;
            }
            else
            {
                controller.currentdata = progressdialogue;
            }
        }
        
        if(isFinished)
        {
            controller.currentdata = finisheddialogue;
        }
    }
}
