using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Quest Data")]
public class QuestData_SO : ScriptableObject
{
    [System.Serializable]
    public class QuestRequire
    {
        public string name;
        public int requireamount;
        public int currentamount;
    }
    public string questname;
    [TextArea]
    public string description;
    public bool isstarted;
    public bool iscomplete;
    public bool isfinished;

    public List<QuestRequire> questrequires = new List<QuestRequire>();
    public List<InventoryItem> rewards = new List<InventoryItem>();

    public void CheckQuestProgress()
    {
        var finishrequires = questrequires.Where(r => r.requireamount <= r.currentamount);
        iscomplete = finishrequires.Count() == questrequires.Count;

        if (iscomplete)
        {
            Debug.Log("任务完成");
        }
    }

    public void GiveRewards()
    {
        foreach (var reward in rewards)
        {
            if (reward.amount < 0)
            {
                int requirecount = Mathf.Abs(reward.amount);

                if (InventoryManager.Instance.QuestItemInBag(reward.itemdata) != null)
                {
                    if (InventoryManager.Instance.QuestItemInBag(reward.itemdata).amount <= requirecount)
                    {
                        requirecount -= InventoryManager.Instance.QuestItemInBag(reward.itemdata).amount;
                        InventoryManager.Instance.QuestItemInBag(reward.itemdata).amount = 0;

                        if (InventoryManager.Instance.QuestItemInAction(reward.itemdata) != null)
                        {
                            InventoryManager.Instance.QuestItemInAction(reward.itemdata).amount -= requirecount;
                        }
                    }
                    else
                    {
                        InventoryManager.Instance.QuestItemInBag(reward.itemdata).amount -= requirecount;
                    }
                }
                else
                {
                    InventoryManager.Instance.QuestItemInAction(reward.itemdata).amount -= requirecount;
                }
            }
            else
            {
                InventoryManager.Instance.inventorydata.ADDItem(reward.itemdata, reward.amount);
            }

            InventoryManager.Instance.inventoryui.RefresUI();
            InventoryManager.Instance.actionui.RefresUI();
        }
    }


    public List<string> Requiretargetname()
    {
        List<string> targetnamelist = new List<string>();

        foreach (var require in questrequires)
        {
            targetnamelist.Add(require.name);
        }
        return targetnamelist;
    }

}
