using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemdata;
    public bool pick = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pick)
        {
            //TODO:将物品添加到背包
            pick = true;
            InventoryManager.Instance.inventorydata.ADDItem(itemdata, itemdata.itemamount);
            InventoryManager.Instance.inventoryui.RefresUI();
            //装备武器;

            QuestManager.Instance.UpdateQuestProgress(itemdata.itemname, itemdata.itemamount);
            Destroy(gameObject);
        }
    }
}
