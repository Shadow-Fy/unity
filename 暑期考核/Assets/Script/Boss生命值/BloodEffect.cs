using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Image unmaskimage;
    public Image maskedimage;
    public Animator anim;
    public void InitBloodEffect(float cur, float pre)
    {
        unmaskimage.fillAmount = cur;
        maskedimage.fillAmount = pre;
        anim.SetBool("Show", true);
        StartCoroutine(BloodMove());
    }

    IEnumerator BloodMove()
    {
        float a = 400;
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            a-=10;
            MoveEffect(a);
            yield return new WaitForFixedUpdate();
        }
    }

    void MoveEffect(float a)
    {
        transform.position -= (Vector3.up *a * Time.deltaTime);
    }

    public void DestroyThis()
    {
        // Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }
}
