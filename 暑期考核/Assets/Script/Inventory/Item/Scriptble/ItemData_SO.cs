using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType { uesable, weapen, armor, wing, skill }
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData_SO : ScriptableObject
{
    public ItemType itemtype;
    public string itemname;
    public Sprite itemicon;
    public int itemamount;

    [Header("Useble Data")]
    public UseableItemData_SO useabledata;

    [TextArea]
    public string description = "";
    public bool stackble;

    [Header("翅膀")]
    public WingData_SO wingdata;


    [Header("武器")]
    // public GameObject weaponprefeb;
    public AttackData_SO attackdata;

    [Header("技能")]
    public SkillData_SO skilldata;

    [Header("盔甲")]
    public ArmorData_SO armordata;

}
