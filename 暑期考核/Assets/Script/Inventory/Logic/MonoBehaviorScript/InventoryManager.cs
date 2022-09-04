using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : Singleton<InventoryManager>
{
    public class DragData
    {
        public SlotHolder originalholder;
        public RectTransform originalParent;
    }

    [Header("Inventory Data")]
    public InventoryData_SO inventorytemplate;
    public InventoryData_SO inventorydata;
    public InventoryData_SO actiontemplate;
    public InventoryData_SO actiondata;
    public InventoryData_SO equipmenttemplate;
    public InventoryData_SO equipmentdata;

    [Header("ContainerS")]
    public ContainerUI inventoryui;
    public ContainerUI actionui;
    public ContainerUI equipmentui;

    [Header("Drag Canvas")]
    public Canvas dragcanvas;
    public DragData currentdrag;

    [Header("UI Panel")]
    public GameObject bagpanel;
    public GameObject statspanel;

    [Header("Stats Text")]
    public Text healthtext;
    public Text attacktext;

    bool isopen = false;

    [Header("Tooltip")]
    public ItemToolTips tooltips;


    protected override void Awake()
    {
        base.Awake();
        if (inventorytemplate != null)
            inventorydata = Instantiate(inventorytemplate);
        if (actiontemplate != null)
            actiondata = Instantiate(actiontemplate);
        if (equipmenttemplate != null)
            equipmentdata = Instantiate(equipmenttemplate);
    }

    // private void OnEnable()
    // {
    //     LoadData();
    //     inventoryui.RefresUI();
    //     actionui.RefresUI();
    //     equipmentui.RefresUI();
    // }

    void Start()
    {
        LoadData();
        inventoryui.RefresUI();
        actionui.RefresUI();
        equipmentui.RefresUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isopen = !isopen;
            bagpanel.SetActive(isopen);
            statspanel.SetActive(isopen);
            if (!isopen)
                tooltips.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isopen = false;
            bagpanel.SetActive(false);
            statspanel.SetActive(false);
            if (!isopen)
                tooltips.gameObject.SetActive(false);
        }



        UpdateStatsText(GameManager.Instance.playerstats.maxHealth, GameManager.Instance.playerstats.weaponDamage);
    }

    public void SaveData()
    {
        SaveManager.Instance.Save(inventorydata, inventorydata.name);
        SaveManager.Instance.Save(actiondata, actiondata.name);
        SaveManager.Instance.Save(equipmentdata, equipmentdata.name);

    }

    public void LoadData()
    {
        SaveManager.Instance.Load(inventorydata, inventorydata.name);
        SaveManager.Instance.Load(actiondata, actiondata.name);
        SaveManager.Instance.Load(equipmentdata, equipmentdata.name);
    }

    public void UpdateStatsText(int health, int attacknum)
    {
        healthtext.text = health.ToString();
        attacktext.text = attacknum.ToString();
    }

    #region  检查拖拽物品是否在每一个 Slot 范围内
    public bool CheckInInventoryUI(Vector3 position)
    {
        for (int i = 0; i < inventoryui.slotholders.Length; i++)
        {
            RectTransform t = inventoryui.slotholders[i].transform as RectTransform;//强制转换(as RectTransform)

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckInActionUI(Vector3 position)
    {
        for (int i = 0; i < actionui.slotholders.Length; i++)
        {
            RectTransform t = actionui.slotholders[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckInEquipmentUI(Vector3 position)
    {
        for (int i = 0; i < equipmentui.slotholders.Length; i++)
        {
            RectTransform t = equipmentui.slotholders[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    public void CheckQuestItemBag(string questitemname)
    {
        foreach (var item in inventorydata.items)
        {
            if (item.itemdata != null)
            {
                if (item.itemdata.itemname == questitemname)
                    QuestManager.Instance.UpdateQuestProgress(item.itemdata.itemname, item.amount);
            }
        }

        foreach (var item in actiondata.items)
        {
            if (item.itemdata != null)
            {
                if (item.itemdata.itemname == questitemname)
                    QuestManager.Instance.UpdateQuestProgress(item.itemdata.itemname, item.amount);
            }
        }
    }

    public InventoryItem QuestItemInBag(ItemData_SO questitem)
    {
        return inventorydata.items.Find(i => i.itemdata == questitem);
    }

    public InventoryItem QuestItemInAction(ItemData_SO questitem)
    {
        return actiondata.items.Find(i => i.itemdata == questitem);
    }
}