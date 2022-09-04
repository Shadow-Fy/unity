using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    private Transform playertr;
    public GameObject smallswordprefeb;/* 吸血鬼刀 */
    public GameObject starprefeb;/* 狂星之怒 */
    public GameObject greenprefeb;/* 泰拉刀 */
    public GameObject catprefeb;/* 喵刀 */
    public GameObject arcticiceprefeb;/* 北极 */
    private Vector2 direction_arctic;

    [Header("鼠标")]
    private Vector2 mousepos;
    private Vector2 mouseposdown;
    private Vector2 direction;
    float dir;/* 鼠标和玩家的角度 */
    float dis;/* 鼠标和玩家的距离 */


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        dis = Vector3.Distance(mousepos, playertr.position);
        dir = Mathf.Atan2(mousepos.y - playertr.position.y, mousepos.x - playertr.position.x) * Mathf.Rad2Deg;/* 弧度变角度 */

        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction_arctic = (mousepos - new Vector2(playertr.position.x, playertr.position.y));
        direction = (mousepos - new Vector2(playertr.position.x, playertr.position.y)).normalized;
        if (Input.GetMouseButtonDown(0))
        {
            mouseposdown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void GetPlayerTR(Transform player)
    {
        playertr = player;
    }


    public void WeaponProjection()
    {
        if (GameManager.Instance.playerstats.WeaponCount != 0)
            if (GameManager.Instance.playerstats.currentEnergy >= 6)
                GameManager.Instance.playerstats.currentEnergy -= 6;

        if (GameManager.Instance.playerstats.currentEnergy > 6)
        {/* 北极 */
            if (GameManager.Instance.playerstats.WeaponCount == 1)
            {
                GameObject arcticice = ObjectPool.Instance.GetObject(arcticiceprefeb);
                arcticice.transform.position = playertr.position + new Vector3(0, 2);
                arcticice.transform.up = direction;
                arcticice.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction_arctic.x, direction_arctic.y) * 9, ForceMode2D.Impulse);
            }

            /* 喵刀 */
            if (GameManager.Instance.playerstats.WeaponCount == 2)
            {
                GameObject cat = ObjectPool.Instance.GetObject(catprefeb);
                cat.transform.position = playertr.position;
                cat.transform.up = direction;
                cat.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction_arctic.x, direction_arctic.y) * 9, ForceMode2D.Impulse);
            }

            /* 泰拉刀 */
            if (GameManager.Instance.playerstats.WeaponCount == 3)
            {
                GameObject green = ObjectPool.Instance.GetObject(greenprefeb);
                green.transform.position = playertr.position;
                green.transform.right = direction;
            }

            /* 狂星之怒 */
            if (GameManager.Instance.playerstats.WeaponCount == 4)
            {
                StartCoroutine("Star");
            }

            /* 吸血鬼刀 */
            if (GameManager.Instance.playerstats.WeaponCount == 5)
            {
                for (int i = -3; i < 3; i++)
                {
                    GameObject smallsword = ObjectPool.Instance.GetObject(smallswordprefeb);
                    smallsword.transform.position = playertr.position;
                    smallsword.transform.up = direction;
                    // float dir = Mathf.Atan2(smallsword.transform.position.x - mouseposdown.x, smallsword.transform.position.y - mouseposdown.y) * Mathf.Rad2Deg;
                    // smallsword.transform.rotation = Quaternion.Euler(0, 0, dir + i * 3);
                    smallsword.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos((dir + i * 3) * Mathf.Deg2Rad) * dis, Mathf.Sin((dir + i * 3) * Mathf.Deg2Rad) * dis) * 7, ForceMode2D.Impulse);
                }
            }
        }
    }

    IEnumerator Star()
    {
        for (int i = 1; i <= 8; i++)
        {
            yield return new WaitForSeconds(0.08f);
            GameObject star = ObjectPool.Instance.GetObject(starprefeb);
            star.transform.position = new Vector2(Random.Range(mouseposdown.x - 5, mouseposdown.x + 6), Random.Range(mouseposdown.y + 14, mouseposdown.y + 19));
            /* 目标点有个位置偏移  比较真实 */
            float targetpos = Random.Range(mouseposdown.x - 2, mouseposdown.x + 3);

            /* 数学方法计算角度 */
            float dir = Mathf.Atan2(star.transform.position.x - targetpos, star.transform.position.y - mouseposdown.y) * Mathf.Rad2Deg;
            star.transform.rotation = Quaternion.Euler(0, 0, 270 - dir);
        }
    }
}
