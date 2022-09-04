using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Text energytext;
    public static int currentenergy;
    public static int maxenergy;
    private Image energybar;
    // Start is called before the first frame update
    void Start()
    {
        energybar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        maxenergy = GameManager.Instance.playerstats.maxEnergy;
        currentenergy = GameManager.Instance.playerstats.currentEnergy;
        energybar.fillAmount = (float)currentenergy / (float)maxenergy;
        energytext.text = currentenergy.ToString() + "/" + maxenergy.ToString();
    }
}
