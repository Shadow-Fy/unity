using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestRequirement : MonoBehaviour
{
    private Text requirename;
    private Text progressnumber;

    void Awake()
    {
        requirename = GetComponent<Text>();
        progressnumber = transform.GetChild(0).GetComponent<Text>();
    }

    public void SetupRequirement(string name, int amount, int currentamount)
    {
        requirename.text = name;
        progressnumber.text = currentamount.ToString() + "/" + amount.ToString();
    }


    public void SetupRequirement(string name, bool isFinished)
    {
        if(isFinished)
        {
            requirename.text = name;
            progressnumber.text = "完成";
            requirename.color = Color.gray;
        }
    }
}
