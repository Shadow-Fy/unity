using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{


    public bool useskill1;
    public bool useskill2;
    public bool useskill3;
    public bool useskill4;
    public bool useskill5;

    /* 是否装备了各项技能 */
    // public bool canskill1;
    // public bool canskill2;
    public bool canskill2;
    // public bool canskill4;
    // public bool canskill5;

    private Vector2 mousepos;
    private Vector2 direction;
    private GameObject player;
    [Header("狂化")]
    public float skilltime_1 = 5;
    public GameObject shadowprefab;
    public float skillcdtime_1;

    [Header("护盾")]
    public bool haveshield;
    public GameObject shieldprefeb;
    public GameObject shield;
    public float skillcdtime_4;

    [Header("充能")]
    public float skillcdtime_3;
    public float skilltime_3;

    [Header("缓速")]
    private float distance;
    public float speed;
    public float skillcdtime_2;
    public GameObject lineprefeb;
    private GameObject line;
    private LineRenderer lineer;
    public GameObject circleprefeb;
    private GameObject circle;
    // public GameObject redline;
    private Transform circletr;
    public GameObject aimprefeb;
    private GameObject aim;
    public Transform aimtr;
    // private bool canskill2 = true;
    private bool canline;


    public int isskillcount;


    private void Start()
    {
        skillcdtime_1 = -1;
        skillcdtime_2 = -1;
        skillcdtime_3 = -1;
        skillcdtime_4 = -1;

        skilltime_1 = 5;
        skilltime_3 = 2;

        speed = 0.1f;

    }

    void FixedUpdate()
    {
        Skill_1();

    }

    void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector3.Distance(mousepos, player.transform.position);
        direction = (mousepos - new Vector2(player.transform.position.x, player.transform.position.y)).normalized;
        // Debug.Log(distance);


        Skill();
        skillcdtime_4 -= Time.deltaTime;
        skillcdtime_3 -= Time.deltaTime;
        skillcdtime_2 -= Time.deltaTime;
        skillcdtime_1 -= Time.deltaTime;


        if (canline)
        {
            lineer.SetPosition(0, player.transform.position);
            lineer.SetPosition(1, aimtr.position);
            circle.transform.position = player.transform.position;

            if (distance <= 13.6f)
            {
                aimtr.transform.position = mousepos;
            }
        }
    }


    public void GetPlayer(GameObject goj)
    {
        player = goj;
    }




    public void Skill()
    {


        Skill_2();

        Skill_3();

        Skill_4();

        Skill_5();


    }

    /* 狂化 */
    public void Skill_1()
    {
        if (useskill1 && skillcdtime_1 <= 0)
        {
            skilltime_1 -= Time.deltaTime;
            if (skilltime_1 >= 0)
            {
                ObjectPool.Instance.GetObject(shadowprefab);
                isskillcount = 1;
            }
            else
            {
                skilltime_1 = 5;
                skillcdtime_1 = 5;
                useskill1 = false;
                isskillcount = 0;

            }
        }
        else
        {
            useskill1 = false;
        }

    }

    /* 缓速 */
    public void Skill_2()
    {
        if (useskill2 && skillcdtime_2 <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {

                circle = ObjectPool.Instance.GetObject(circleprefeb);
                line = ObjectPool.Instance.GetObject(lineprefeb);
                aim = ObjectPool.Instance.GetObject(aimprefeb);
                aim.transform.position = player.transform.position;
                aimtr = aim.transform;
                lineer = line.GetComponent<LineRenderer>();

                lineer.startWidth = 0.3f;
                lineer.endWidth = 0.3f;
                speed = 0.01f;
                canskill2 = true;
                canline = true;
                Time.timeScale = 0.05f;

            }

            if (Input.GetMouseButtonUp(1))
            {

                ObjectPool.Instance.PushObject(circle);
                ObjectPool.Instance.PushObject(aim);
                canskill2 = false;
                Time.timeScale = 1f;
                lineer.startWidth = 1f;
                lineer.endWidth = 1f;
                canline = false;
                Invoke("SetLineBack", 0.1f);
                Invoke("MoveSetting", 0.01f);
                skillcdtime_2 = 3f;
            }

        }

    }

    /* 充能 */
    public void Skill_3()
    {
        if (useskill3 && skillcdtime_3 <= 0)
        {
            skilltime_3 -= Time.deltaTime;
            if (skilltime_3 >= 0)
            {
                GameManager.Instance.playerstats.currentEnergy = 100;
            }
            else
            {
                skillcdtime_3 = 9;
                skilltime_3 = 2;
            }
        }
        else
        {
            useskill3 = false;
        }
    }

    /* 护盾 */
    void Skill_4()
    {
        if (useskill4 && skillcdtime_4 <= 0)
        {
            if (!haveshield)
            {
                shield = ObjectPool.Instance.GetObject(shieldprefeb);
                haveshield = true;
            }

            shield.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.1f);






            if (shield.GetComponent<Shield>().health <= 0)
            {
                ObjectPool.Instance.PushObject(shield);
                skillcdtime_4 = 15;
                haveshield = false;
            }
        }


    }

    void Skill_5()
    {

    }

    void SetLineBack()
    {
        ObjectPool.Instance.PushObject(line);
        canline = false;
    }

    void MoveSetting()
    {
        speed = 0.1f;
        player.transform.position = Vector3.MoveTowards(player.transform.position, aimtr.position, 90);
    }
}
