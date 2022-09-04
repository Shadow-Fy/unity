using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public SceneFader scenefaderprefab;

    public GameObject playerprefeb;
    GameObject player;

    public bool canupdatebag;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void TransitionToDestination(TransitionPoint transitionpoint)
    {

        switch (transitionpoint.transitiontype)
        {
            case TransitionPoint.TransitionType.samescene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionpoint.destinationtag));
                break;
            case TransitionPoint.TransitionType.differentscene:
                StartCoroutine(Transition(transitionpoint.scenename, transitionpoint.destinationtag));
                break;
        }
    }

    IEnumerator Transition(string scenename, TransitionDestination.DestinationTag destinationtag)
    {
        SaveManager.Instance.SavePlayerData();
        InventoryManager.Instance.SaveData();
        QuestManager.Instance.SaveQuestManager();


        if (SceneManager.GetActiveScene().name != scenename)
        {
            SceneFader fade = Instantiate(scenefaderprefab);
            yield return StartCoroutine(fade.FadeOut(1f));
            yield return SceneManager.LoadSceneAsync(scenename);
            ObjectPool.Instance.objectPool.Clear();
            yield return Instantiate(playerprefeb, GetDestination(destinationtag).transform.position, GetDestination(destinationtag).transform.rotation);
            SaveManager.Instance.LoadPlayerData();/* 读取数据 */
            yield return StartCoroutine(fade.FadeIn(1f));
            ObjectPool.Instance.objectPool.Clear();
            yield break;
        }
        else
        {
            player = GameManager.Instance.playerstats.gameObject;
            player.transform.SetPositionAndRotation(GetDestination(destinationtag).transform.position, GetDestination(destinationtag).transform.rotation);
            yield return null;
        }
    }

    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationtag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {

            if (entrances[i].destinationtag == destinationtag)
            {
                return entrances[i];
            }
        }
        return null;
    }

    public void TransitionToMain()
    {
        StartCoroutine(LoadMain());
    }

    public void TransitionToLoadGame()
    {
        ObjectPool.Instance.objectPool.Clear();
        StartCoroutine(LoadLevel(SaveManager.Instance.sceneName));

    }

    public void TransitionToFirstLevel()
    {
        StartCoroutine(LoadLevel("新手教程"));
    }

    IEnumerator LoadLevel(string scene)
    {
        SceneFader fade = Instantiate(scenefaderprefab);
        if (scene != "")
        {
            yield return StartCoroutine(fade.FadeOut(1f));
            yield return SceneManager.LoadSceneAsync(scene);
            yield return player = Instantiate(playerprefeb, GameManager.Instance.GetEntrance().position, GameManager.Instance.GetEntrance().rotation);

            SaveManager.Instance.SavePlayerData();
            InventoryManager.Instance.SaveData();
            TimeManager.Instance.SaveTime();
            yield return StartCoroutine(fade.FadeIn(1f));
            yield break;
        }
    }

    IEnumerator LoadMain()
    {
        SceneFader fade = Instantiate(scenefaderprefab);
        yield return StartCoroutine(fade.FadeOut(1f));
        yield return SceneManager.LoadSceneAsync("MainMenu");
        yield return StartCoroutine(fade.FadeIn(1f));
        yield break;
    }
}
