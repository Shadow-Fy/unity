using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public CharacterStats playerstats;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void RigisterPlayer(CharacterStats player)
    {
        playerstats = player;
    }

    public Transform GetEntrance()
    {
        foreach (var item in FindObjectsOfType<TransitionDestination>())
        {
            if(item.destinationtag == TransitionDestination.DestinationTag.Enter)
            return item.transform;
        }
        return null;
    }
}
