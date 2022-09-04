using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Data")]
public class InventoryData_SO : ScriptableObject
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void ADDItem(ItemData_SO newitemdata, int amount)
    {
        bool found = false;

        if(newitemdata.stackble)
        {
            foreach(var item in items)
            {
                if(item.itemdata == newitemdata)
                {
                    item.amount += amount;
                    found = true;
                    break;
                }
            }
        }

        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].itemdata == null && !found)
            {
                items[i].itemdata = newitemdata;
                items[i].amount = amount;
                break;
            }
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemData_SO itemdata;
    
    public int amount;
}
