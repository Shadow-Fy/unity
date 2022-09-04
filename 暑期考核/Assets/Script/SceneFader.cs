using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    CanvasGroup canvasgroup;
    public float fadeinduration;
    public float fadeoutduration;

    private void Awake()
    {
        canvasgroup = GetComponent<CanvasGroup>();

        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator FadeOutIn()
    {
        yield return FadeOut(fadeoutduration);
        yield return FadeIn(fadeinduration);
    }

    public IEnumerator FadeOut(float time)
    {
        while (canvasgroup.alpha < 1)
        {
            canvasgroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (canvasgroup.alpha != 0)
        {
            canvasgroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
        Destroy(gameObject);
    }
}
