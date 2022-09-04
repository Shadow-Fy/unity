using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuffManager : Singleton<BuffManager>
{
    public bool haseyebuff;

    public bool hasfirebuff;
    public GameObject firebuffprefeb;
    // public GameObject firebuff;

    public bool haspoisonbuff;
    public GameObject poisonbuffprefeb;
    // public GameObject poisonbuff;


    public Scene currentscene;
    public Transform player;
    public GameObject fire, health, blue, poison, eye;
    public bool befire, behealth, beblue, bepoison, beeye;

    private float firetime = 1;
    public float firetextcount = 0;
    public float firecount = 0;
    public float maxfirecount = 0;
    private int firedamage = 2;


    private float poisontime = 1;
    public float poisontextcount = 0;
    public float poisoncount = 0;
    public float maxpoisoncount = 0;
    private int poisondamage = 1;

    private float healthtime = 3;

    private float bluetime = 2;


    private float eyetime = 1f;
    public float eyetextcount = 0;
    public float eyecount = 0;
    public float maxeyecount = 0;




    private void Update()
    {
        currentscene = SceneManager.GetActiveScene();
        if (currentscene.name == "大厅" || currentscene.name == "村庄")
        {
            behealth = true;
            beblue = true;
        }
        else
        {
            health.SetActive(false);
            blue.SetActive(false);
            behealth = false;
            beblue = false;
        }

        GetFireBuff();
        GetPisonBuff();
        GetHealthBuff();
        GetBlueBuff();
        GetEyeBuff();
    }

    public void GetPlayer(Transform obj)
    {
        player = obj;
    }

    public void OpenFireBuff()
    {
        hasfirebuff = true;
        befire = true;
        if (maxfirecount != 0)
        {
            maxfirecount += 3;
            firedamage += 2;
        }
        else
        {
            maxfirecount = 7;
        }
    }

    public void OpenPisonBuff()
    {
        haspoisonbuff = true;
        bepoison = true;
        if (maxpoisoncount != 0)
        {
            maxpoisoncount += 5;
            poisondamage += 3;
        }
        else
        {
            maxpoisoncount = 7;
        }
    }

    public void OpenEyeBuff()
    {
        haseyebuff = true;
        beeye = true;
        if (maxeyecount != 0)
        {
            maxeyecount += 1;
        }
        else
        {
            maxeyecount = 3;
        }
    }




    public void GetFireBuff()
    {
        if (befire)
        {
            firetime -= Time.deltaTime;
            fire.SetActive(true);
            fire.transform.GetChild(0).GetComponent<Text>().text = firetextcount.ToString();
            if (hasfirebuff)
            {
                firetextcount += 1;
                GameObject firebuff = ObjectPool.Instance.GetObject(firebuffprefeb);
                firebuff.transform.position = player.position;
                hasfirebuff = false;
            }
            if (firetime <= 0)
            {
                GameManager.Instance.playerstats.BUffHurt(firedamage);
                firecount += 1;
                firetime = 1;
                hasfirebuff = false;
            }

            if (firecount == maxfirecount)
            {
                befire = false;
                firetime = 1;
                fire.SetActive(false);
                firedamage = 2;
                firecount = 0;
                maxfirecount = 0;
                firetextcount = 0;
            }
        }
    }


    public void GetPisonBuff()
    {
        if (bepoison)
        {
            poisontime -= Time.deltaTime;
            poison.SetActive(true);
            poison.transform.GetChild(0).GetComponent<Text>().text = poisontextcount.ToString();
            if (haspoisonbuff)
            {
                poisontextcount += 1;
                GameObject poisonbuff = ObjectPool.Instance.GetObject(poisonbuffprefeb);
                poisonbuff.transform.position = player.position;
                haspoisonbuff = false;
            }
            if (poisontime <= 0)
            {
                GameManager.Instance.playerstats.BUffHurt(poisondamage);
                poisoncount += 1;
                poisontime = 1;
            }

            if (poisoncount == maxpoisoncount)
            {
                bepoison = false;
                poisontime = 1;
                poisondamage = 1;
                poison.SetActive(false);
                poisoncount = 0;
                maxpoisoncount = 0;
                poisontextcount = 0;
            }
        }
    }

    public void GetEyeBuff()
    {
        if (beeye)
        {
            VolumeSetting.Instance.chromaticAberration.intensity.value = 1f;
            eyetime -= Time.deltaTime;
            eye.SetActive(true);
            eye.transform.GetChild(0).GetComponent<Text>().text = eyetextcount.ToString();
            if (haseyebuff)
            {
                eyetextcount += 1;
                haseyebuff = false;
            }
            if (eyetime <= 0)
            {
                eyecount += 1;
                eyetime = 1;
            }

            if (eyecount == maxeyecount)
            {
                beeye = false;
                eyetime = 1;
                eye.SetActive(false);
                eyecount = 0;
                maxeyecount = 0;
                eyetextcount = 0;
                VolumeSetting.Instance.chromaticAberration.intensity.value = 0;
            }
        }
    }


    public void GetHealthBuff()
    {
        if (behealth)
        {
            healthtime -= Time.deltaTime;
            health.SetActive(true);

            if (healthtime <= 0)
            {
                GameManager.Instance.playerstats.BUffHealthUp(1);
                healthtime = 3f;
            }
        }
    }

    public void GetBlueBuff()
    {
        if (beblue)
        {
            bluetime -= Time.deltaTime;
            blue.SetActive(true);

            if (bluetime <= 0)
            {
                GameManager.Instance.playerstats.BUffHealthUp(1);
                bluetime = 2f;
            }
        }
    }
}
