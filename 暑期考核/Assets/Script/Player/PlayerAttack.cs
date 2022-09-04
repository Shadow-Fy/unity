using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool isattack;
    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;

    public GameObject attackprefeb;

    [HideInInspector] public float skilltimeleft;
    public bool isskill1;

    public float skillattacktime = 0.05f;

    private Vector2 mousepos;


    private Animator anim;
    private AnimatorStateInfo animstateinfo;
    private const string idlestate = "idlestate";
    private const string attack1state = "attack1";
    private const string attack2state = "attack2";
    private int hitcount = 0;
    // public bool canattack;

    void Start()
    {
        anim = GetComponent<Animator>();
        MyInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();

    }


    void FixedUpdate()
    {

    }
    void Update()
    {
        skillattacktime -= Time.deltaTime;


        Skill2();

        if (SkillManager.Instance.useskill1)
        {
            SkillAttack();
        }

        else
            Attack();

    }

    void Attack()
    {
        animstateinfo = anim.GetCurrentAnimatorStateInfo(1);
        if (animstateinfo.IsName(idlestate))
        {
            hitcount = 0;
            anim.SetInteger("attack", 0);
        }

        /* 鼠标左键攻击 */
        // catcd -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {

            WeaponManager.Instance.WeaponProjection();/* 武器系统 */



            if (animstateinfo.IsName(idlestate) && hitcount == 0)
            {
                anim.SetInteger("attack", 1);
                hitcount = 1;
            }

            else if (animstateinfo.IsName(attack1state) && hitcount == 1 && animstateinfo.normalizedTime > 0.3f)
            {

                anim.SetInteger("attack", 2);
                hitcount = 2;
            }

            else if (animstateinfo.IsName(attack2state) && hitcount == 2 && animstateinfo.normalizedTime > 0.3f)
            {
                anim.SetInteger("attack", 1);
                hitcount = 1;
            }
            return;
        }

    }

    void SkillAttack()
    {
        // if (Input.GetKeyDown(KeyCode.J))
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("skillattack", true);
            if (skillattacktime <= 0)
            {
                WeaponManager.Instance.WeaponProjection();
                skillattacktime = 0.05f;
            }
        }
        // if (Input.GetKeyUp(KeyCode.J))
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("skillattack", false);
        }
    }

    public void Skill2()/* 狂化时的动画修复 */
    {
        if (!SkillManager.Instance.useskill1)
        {
            if (isskill1)
            {
                isskill1 = false;
                anim.SetBool("skillattack", false);
            }
        }

        if (SkillManager.Instance.useskill1)
        {
            if (isskill1 == false)
            {
                anim.Play("idle");
                anim.SetInteger("attack", 0);
                isskill1 = true;
            }
        }
    }




    public float RandomDamage()/* 攻击伤害生成 */
    {
        float number = Random.Range(1, 100);
        int randdamage;
        if (number <= 20)
        {
            randdamage = GameManager.Instance.playerstats.weaponDamage * 2;
        }
        else
            randdamage = GameManager.Instance.playerstats.weaponDamage;
        return randdamage;
    }


    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Enemy"))
        {
            // canpause = true;
            VolumeSetting.Instance.chromaticAberration.intensity.value = 0.15f;
            StartCoroutine("Pause");
            other.GetComponent<IDamageable>().GetHit(RandomDamage());
            GameObject attackspecial = ObjectPool.Instance.GetObject(attackprefeb);
            MyInpulse.GenerateImpulse();
            attackspecial.transform.position = other.transform.position;
        }
    }

    IEnumerator Pause()
    {
        Time.timeScale = 0.02f;
        yield return new WaitForSecondsRealtime(0.06f);
        Time.timeScale = 1f;
        VolumeSetting.Instance.chromaticAberration.intensity.value = 0f;
        yield return null;
    }
}
