using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [HideInInspector] public CharacterStats characterstats;
    private ScreenFlash sf;
    public GameObject sfc;
    public GameObject wing;
    private Rigidbody2D rb;
    private Animator anim;
    private float speed;
    private int jumpcount = 1;
    private bool canjump;
    public bool isjump;
    private float jumpforce;
    public bool isground;
    public Transform groundcheck;
    public float checkradius;
    public LayerMask groundlayer;
    [HideInInspector] public float horizontalmove;
    [HideInInspector] public float verticalmove;

    private PlayerAttack playerattack;

    public float aimradius;
    public bool isdead;
    float energytime = 3;

    void OnEnable()
    {
        BuffManager.Instance.GetPlayer(transform);
        GameManager.Instance.RigisterPlayer(characterstats);
        WeaponManager.Instance.GetPlayerTR(transform);
        SkillManager.Instance.GetPlayer(gameObject);
        Cinema.Instance.GetPlayerTransform(gameObject);

    }

    void Start()
    {
        sf = GetComponent<ScreenFlash>();
        speed = 15;
        jumpforce = 25;
        rb = GetComponent<Rigidbody2D>();
        playerattack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
        characterstats = GetComponent<CharacterStats>();
        GameManager.Instance.RigisterPlayer(characterstats);
        SaveManager.Instance.LoadPlayerData();
    }

    void Update()
    {
        RecoverEnergy();

        if (GameManager.Instance.playerstats.currentHealth > GameManager.Instance.playerstats.maxHealth)
            GameManager.Instance.playerstats.currentHealth = GameManager.Instance.playerstats.maxHealth;

        anim.SetBool("dead", isdead);
        if (isdead)
            return;

        CheckInput();


        if (GameManager.Instance.playerstats.currentHealth < 1)
        {
            GameManager.Instance.playerstats.currentHealth = 0;
            isdead = true;
        }
    }

    void FixedUpdate()
    {
        if (isdead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Movement();
    }

    void Movement()/* 移动 */
    {
        if (SkillManager.Instance.useskill1)
        {
            speed = 27;
            jumpforce = 35;
        }
        else
        {
            speed = 15;
            jumpforce = 25;
        }

        horizontalmove = Input.GetAxisRaw("Horizontal");
        verticalmove = Input.GetAxisRaw("Vertical");

        if (!GameManager.Instance.playerstats.wingdata.canfly)
            Move_1();
        else
            Move_2();




    }

    void Move_1()/* 走路 */
    {
        rb.gravityScale = 7;
        wing.SetActive(false);
        bool isattack = (anim.GetCurrentAnimatorStateInfo(1).IsName("attack1") || anim.GetCurrentAnimatorStateInfo(1).IsName("attack2") || anim.GetCurrentAnimatorStateInfo(1).IsName("attackskill2"));

        if (!isattack && !Input.GetMouseButton(0))
        {
            if (!anim.GetCurrentAnimatorStateInfo(2).IsName("hurt"))
                rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
        }
        else
        {
            if (playerattack.skilltimeleft > 0)
                rb.velocity = new Vector2(transform.localScale.x * 3, rb.velocity.y);
            else
                rb.velocity = new Vector2(transform.localScale.x * 1, rb.velocity.y);
        }
        if (horizontalmove != 0 && !isattack)
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(horizontalmove, 1, 1);
        }
        else
        {
            anim.SetBool("run", false);
        }
        Jump();
    }

    void Move_2()/* 飞行 */
    {
        wing.SetActive(true);
        rb.gravityScale = 0;
        if (horizontalmove != 0)
            transform.localScale = new Vector3(horizontalmove, 1, 1);
        rb.velocity = new Vector2(horizontalmove * speed, verticalmove * speed * 0.85f);
    }

    void CheckInput()/* 跳跃判断 */
    {


        if (Input.GetButtonDown("Jump") && jumpcount > 0 && isground)
        {
            canjump = true;
        }
    }

    void Jump()/* 跳跃 */
    {

        isground = Physics2D.OverlapCircle(groundcheck.position, checkradius, groundlayer);


        if (canjump)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpcount--;
            canjump = false;
        }
        if (isground)
        {
            jumpcount = 1;
            anim.SetBool("fall", false);
            anim.SetBool("jump", false);
            anim.SetBool("idle", true);
        }
        else if (rb.velocity.y > 0 && !isground)
        {
            anim.SetBool("jump", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
    }

    void OnDrawGizmos()/* 画圈判断范围 */
    {
        Gizmos.DrawWireSphere(groundcheck.position, checkradius);
        Gizmos.DrawWireSphere(transform.position, 13.6f);
    }

    public void GetHit(float damage)/* 受伤 */
    {

        if (!anim.GetCurrentAnimatorStateInfo(2).IsName("hurt"))//受伤短暂无敌
        {
            sfc.SetActive(true);
            sf.FlashScreen();
            VolumeSetting.Instance.chromaticAberration.intensity.value = 0.3f;
            GameManager.Instance.playerstats.GetHurt((int)damage);
            if (GameManager.Instance.playerstats.currentHealth < 1)
            {
                GameManager.Instance.playerstats.currentHealth = 0;
                isdead = true;
            }
            anim.SetTrigger("hit");
        }
    }

    public void SetBackChromatic()
    {
        VolumeSetting.Instance.chromaticAberration.intensity.value = 0f;
    }


    public void RecoverEnergy()
    {
        energytime -= Time.deltaTime;
        if (energytime <= 0)
        {
            GameManager.Instance.playerstats.currentEnergy += 5;
            energytime = 3;
        }

        if (GameManager.Instance.playerstats.currentEnergy > 99)
            GameManager.Instance.playerstats.currentEnergy = 100;

        if (GameManager.Instance.playerstats.currentEnergy < 1)
        {
            GameManager.Instance.playerstats.currentEnergy = 0;
        }
    }


}