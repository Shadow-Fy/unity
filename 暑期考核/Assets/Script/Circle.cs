using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void OnEnable()
    {
        // SkillManager.Instance.GetCircle(gameObject);
        // gameObject.SetActive(false);
    }
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 100);

    }
}
