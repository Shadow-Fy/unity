using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    public Image bloodimage;
    public int maxHP;
    [HideInInspector]public int curHP;
    public GameObject bloodeffectprefeb;

    public void GetDamage(int damage)
    {
        curHP -= damage;
        float prefillamount = bloodimage.fillAmount;
        float curfillamount = (float)curHP / (float)maxHP;
        bloodimage.fillAmount = curfillamount;

        GameObject bloodeffect= ObjectPool.Instance.GetObject(bloodeffectprefeb);
        bloodeffect.transform.SetParent(transform.parent);
        bloodeffect.transform.localPosition = Vector3.zero;
        bloodeffect.transform.localScale = Vector3.one;
        // GameObject bloodeffect = Instantiate(bloodeffectprefeb, transform.parent);
        bloodeffect.transform.SetAsFirstSibling();
        bloodeffect.GetComponent<BloodEffect>().InitBloodEffect(curfillamount, prefillamount);
    }

}
