using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNameButton : MonoBehaviour
{
    public Text questnametext;
    public QuestData_SO currentdata;
    // public Text questcontenttext;


    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UpdateQuestContent);
    }

    void UpdateQuestContent()
    {
        // questcontenttext.text = currentdata.description;
        QuestUI.Instance.SetupRequireList(currentdata);

        foreach (Transform item in QuestUI.Instance.rewardtransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in currentdata.rewards)
        {
            QuestUI.Instance.SetupRewaordItem(item.itemdata, item.amount);
        }

    }

    public void SetupNameButton(QuestData_SO questdata)
    {
        currentdata = questdata;

        if (questdata.iscomplete)
            questnametext.text = questdata.questname + "(完成)";
        else
            questnametext.text = questdata.questname;
    }
}
