using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Animator anim;
    public CharacterData_SO templatedata;
    public CharacterData_SO characterdata;/* awake里面实例化 */
    public AttackData_SO templateattackdata;
    public AttackData_SO attackdata;
    public ArmorData_SO templatearmordata;
    public ArmorData_SO armordata;
    public WingData_SO templatewingdata;
    public WingData_SO wingdata;
    public SkillData_SO templateskilldata_1;

    public SkillData_SO skilldata_1;
    public SkillData_SO templateskilldata_2;
    public SkillData_SO skilldata_2;
    public SkillData_SO templateskilldata_3;
    public SkillData_SO skilldata_3;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (templatedata != null)
        {
            characterdata = Instantiate(templatedata);
        }

        if (templateattackdata != null)
        {
            attackdata = Instantiate(templateattackdata);
        }

        if (templatearmordata != null)
        {
            armordata = Instantiate(templatearmordata);
        }

        if (templatewingdata != null)
        {
            wingdata = Instantiate(templatewingdata);
        }
        
        if (templateskilldata_1 != null)
        {
            skilldata_1 = Instantiate(templateskilldata_1);
        }

        if (templateskilldata_2 != null)
        {
            skilldata_2 = Instantiate(templateskilldata_2);
        }

        if (templateskilldata_3 != null)
        {
            skilldata_3 = Instantiate(templateskilldata_3);
        }


    }
    public int maxHealth
    {
        get
        {
            if (characterdata != null)
                return characterdata.maxhealth + armordata.armorhealth;
            else return 0;
        }
        set
        {
            characterdata.maxhealth = value;
        }
    }

    public int currentHealth
    {
        get
        {
            if (characterdata != null)
                return characterdata.currenthealth;
            else return 0;
        }
        set
        {
            characterdata.currenthealth = value;
        }
    }

    public int maxEnergy
    {
        get
        {
            if (characterdata != null)
                return characterdata.maxenergy;
            else return 0;
        }
        set
        {
            characterdata.maxenergy = value;
        }
    }

    public int currentEnergy
    {
        get
        {
            if (characterdata != null)
                return characterdata.currentenergy;
            else return 0;
        }
        set
        {
            characterdata.currentenergy = value;
        }
    }


    public int weaponDamage
    {
        get
        {
            if (attackdata != null)
                return attackdata.weapondamage;
            else return 0;
        }
        set
        {
            attackdata.weapondamage = value;
        }
    }

    public bool canFly
    {
        get
        {
            if (wingdata != null)
                return wingdata.canfly;
            else return false;
        }
        set
        {
            wingdata.canfly = value;
        }
    }

    public int WeaponCount
    {
        get
        {
            if (attackdata != null)
                return attackdata.weaponcount;
            else return 0;
        }
        set
        {
            attackdata.weaponcount = value;
        }
    }

    public int armorHealth
    {
        get
        {
            if (armordata != null)
                return armordata.armorhealth;
            else return 0;
        }
        set
        {
            armordata.armorhealth = value;
        }
    }

    public int skillCount1
    {
        get
        {
            if (characterdata != null)
                return skilldata_1.skillcount;
            else return 0;
        }
        set
        {
            skilldata_1.skillcount = value;
        }
    }

    public int skillCount2
    {
        get
        {
            if (characterdata != null)
                return skilldata_2.skillcount;
            else return 0;
        }
        set
        {
            skilldata_2.skillcount = value;
        }
    }

    public int skillCount3
    {
        get
        {
            if (characterdata != null)
                return skilldata_3.skillcount;
            else return 0;
        }
        set
        {
            skilldata_3.skillcount = value;
        }
    }

    public void GetHurt(int amount)
    {
        currentHealth -= amount;
    }

    public void BUffHurt(int amount)
    {
        currentHealth -= amount;
        anim.SetTrigger("hit");
        if (GameManager.Instance.playerstats.currentHealth < 1)
        {
            GameManager.Instance.playerstats.currentHealth = 0;
        }
    }

    public void BUffHealthUp(int amount)
    {
        currentHealth += amount;
        if (GameManager.Instance.playerstats.currentHealth > GameManager.Instance.playerstats.maxHealth - 1)
        {
            GameManager.Instance.playerstats.currentHealth = GameManager.Instance.playerstats.maxHealth;
        }
    }

    public void BUffBlueUp(int amount)
    {

        currentEnergy += amount;
        if (GameManager.Instance.playerstats.currentHealth > 99)
        {
            GameManager.Instance.playerstats.currentHealth = 100;
        }
    }


    public void ApplyHealth(int amount)
    {
        if (currentHealth + amount <= maxHealth)
            currentHealth += amount;
        else
            currentHealth = maxHealth;

    }

    public void ApplyArmor(int amount)
    {
        armorHealth = amount;
    }

    public void ApplyDamage(int amount)
    {
        weaponDamage = amount;
    }

    public void ApplyWing(bool fly)
    {
        canFly = fly;
    }

    public void ApplyWeaponCount(int count)
    {
        WeaponCount = count;
    }

    public void ApplySkill1(int count)
    {
        skillCount1 = count;
    }
    public void ApplySkill2(int count)
    {
        skillCount2 = count;
    }
    public void ApplySkill3(int count)
    {
        skillCount3 = count;
    }
}
