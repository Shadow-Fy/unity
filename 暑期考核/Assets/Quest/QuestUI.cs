using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : Singleton<QuestUI>
{
    public GameObject questpanel;
    public ItemToolTips tooltip;
    bool isopen;

    [Header("Quest Name")]
    public RectTransform questlisttransform;
    public QuestNameButton questnamebutton;

    [Header("Text Content")]
    public Text questcontenttext;

    [Header("Requirement")]
    public RectTransform requiretransform;
    public QuestRequirement requirement;

    [Header("Reword Panel")]
    public RectTransform rewardtransform;
    public ItemUI rewardui;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            isopen = !isopen;
            questpanel.SetActive(isopen);
            questcontenttext.text = "";
            SetupQuestList();

            if (!isopen)
                tooltip.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isopen = false;
            questpanel.SetActive(false);
            questcontenttext.text = "";
            SetupQuestList();

            if (!isopen)
                tooltip.gameObject.SetActive(false);
        }
    }

    public void SetupQuestList()
    {
        foreach (Transform item in questlisttransform)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in rewardtransform)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in requiretransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var task in QuestManager.Instance.tasks)
        {
            var newtask = Instantiate(questnamebutton, questlisttransform);
            newtask.SetupNameButton(task.questdata);
            // newtask.questcontenttext = questcontenttext;
        }
    }

    public void SetupRequireList(QuestData_SO questdata)
    {
        questcontenttext.text = questdata.description;
        foreach (Transform item in requiretransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var require in questdata.questrequires)
        {
            var q = Instantiate(requirement, requiretransform);
            if (questdata.isfinished)
                q.SetupRequirement(requirement.name, true);
            else
                q.SetupRequirement(require.name, require.requireamount, require.currentamount);
        }
    }


    public void SetupRewaordItem(ItemData_SO itemdata, int amount)
    {
        var item = Instantiate(rewardui, rewardtransform);
        item.SetUpItemUI(itemdata, amount);
    }
}
