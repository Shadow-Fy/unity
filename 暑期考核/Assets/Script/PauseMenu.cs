using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool pause = false;
    public GameObject pausepanel;
    public GameObject deathpanel;
    public bool isdead;

    void Update()
    {
        pausepanel.SetActive(pause);
        if (GameManager.Instance.playerstats.currentHealth == 0)
        {

            isdead = true;
            Invoke("GameOver", 1f);
        }
    }

    public void GameOver()
    {
        if (isdead)
        {
            deathpanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Pause()
    {
        pause = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pause = false;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        isdead = false;
        Time.timeScale = 1;
        SceneController.Instance.TransitionToMain();

    }

    public void CountinueGame()
    {
        isdead = false;
        Time.timeScale = 1;
        deathpanel.SetActive(false);
        GameManager.Instance.playerstats.currentHealth = GameManager.Instance.playerstats.maxHealth;
        ObjectPool.Instance.objectPool.Clear();
        SceneController.Instance.TransitionToLoadGame();
    }
}
