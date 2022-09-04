using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{
    Button newgame;
    Button continuegame;
    Button quitgame;
    PlayableDirector director;

    void Awake()
    {
        newgame = transform.GetChild(1).GetComponent<Button>();
        continuegame = transform.GetChild(2).GetComponent<Button>();
        quitgame = transform.GetChild(3).GetComponent<Button>();

        newgame.onClick.AddListener(PlayTimeline);
        continuegame.onClick.AddListener(ContinueGame);
        quitgame.onClick.AddListener(QuitGame);

        director = FindObjectOfType<PlayableDirector>();
        director.stopped += NewGame;
    }

    void PlayTimeline()
    {
        director.Play();
    }

    public void NewGame(PlayableDirector obj)
    {
        PlayerPrefs.DeleteAll();
        SceneController.Instance.TransitionToFirstLevel();

    }
    public void ContinueGame()
    {
        SceneController.Instance.TransitionToLoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
        // Debug.Log("退出游戏");
    }
}
