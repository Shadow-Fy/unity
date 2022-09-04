using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text healthtext;
    public static int currenthealth;
    public static int maxhealth;
    private Image healthbar;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        maxhealth = GameManager.Instance.playerstats.maxHealth;
        currenthealth = GameManager.Instance.playerstats.currentHealth;
        healthbar.fillAmount = (float)currenthealth / (float)maxhealth;
        healthtext.text = currenthealth.ToString() + "/" + maxhealth.ToString();
    }
}
