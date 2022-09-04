using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    string scenename = "level";

    public string sceneName
    {
        get
        {
            return PlayerPrefs.GetString(scenename);
        }
    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {

    }



    public void SavePlayerData()
    {
        Save(GameManager.Instance.playerstats.characterdata, GameManager.Instance.playerstats.characterdata.name);
        Save(GameManager.Instance.playerstats.attackdata, GameManager.Instance.playerstats.attackdata.name);
        Save(GameManager.Instance.playerstats.armordata, GameManager.Instance.playerstats.armordata.name);
        Save(GameManager.Instance.playerstats.wingdata, GameManager.Instance.playerstats.wingdata.name);
        Save(GameManager.Instance.playerstats.skilldata_1, GameManager.Instance.playerstats.skilldata_1.name);
        Save(GameManager.Instance.playerstats.skilldata_2, GameManager.Instance.playerstats.skilldata_2.name);
        Save(GameManager.Instance.playerstats.skilldata_3, GameManager.Instance.playerstats.skilldata_3.name);

    }

    public void LoadPlayerData()
    {
        Load(GameManager.Instance.playerstats.characterdata, GameManager.Instance.playerstats.characterdata.name);
        Load(GameManager.Instance.playerstats.attackdata, GameManager.Instance.playerstats.attackdata.name);
        Load(GameManager.Instance.playerstats.armordata, GameManager.Instance.playerstats.armordata.name);
        Load(GameManager.Instance.playerstats.wingdata, GameManager.Instance.playerstats.wingdata.name);
        Load(GameManager.Instance.playerstats.skilldata_1, GameManager.Instance.playerstats.skilldata_1.name);
        Load(GameManager.Instance.playerstats.skilldata_2, GameManager.Instance.playerstats.skilldata_2.name);
        Load(GameManager.Instance.playerstats.skilldata_3, GameManager.Instance.playerstats.skilldata_3.name);
    }


    public void Save(Object data, string key)
    {
        var jsondata = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsondata);
        PlayerPrefs.SetString(scenename, SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }

    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }
}
