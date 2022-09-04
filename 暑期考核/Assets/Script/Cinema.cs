using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Cinema : Singleton<Cinema>
{
    private CinemachineVirtualCamera cinema;
    public GameObject player;
    	

    // Start is called before the first frame update

    void Start()
    {
        cinema = GetComponent<CinemachineVirtualCamera>();
    }

    public void GetPlayerTransform(GameObject gameObject)
    {
        player = gameObject;
    }

    public void Update()
    {
        cinema.Follow = player.transform;

    }
}
