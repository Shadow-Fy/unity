using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeSetting : Singleton<VolumeSetting>
{


    public Volume volume;//定义一个Volume
    public Bloom bloom;//定义相关效果
    public ChromaticAberration chromaticAberration;

    int minute;
    // 已经花费的时间 
    float timeSpend = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out chromaticAberration);
    }

    void Update()
    {

        if (TimeManager.Instance.Hour >= 5 && TimeManager.Instance.Hour < 9)
        {
            bloom.intensity.value = 8 - (float)((float)((TimeManager.Instance.Hour - 5f) * 30f + minute) / 90f) * 7f;
            timeSpend += Time.deltaTime;
            minute = (int)timeSpend;
            bloom.tint.value = new Color(1 - (float)minute / 120, (float)minute / 120, (float)minute / 120);
            if (TimeManager.Instance.Hour >= 5 && TimeManager.Instance.Hour < 8)
            {
                bloom.intensity.value = 8 - (float)((float)((TimeManager.Instance.Hour - 5f) * 30f + minute) / 90f) * 7f;
            }
        }



        if (TimeManager.Instance.Hour >= 17 && TimeManager.Instance.Hour < 21)
        {
            bloom.intensity.value = 1 + (float)((float)((TimeManager.Instance.Hour - 17f) * 30f + minute) / 90f) * 7f;
            timeSpend += Time.deltaTime;
            minute = (int)timeSpend;
            bloom.tint.value = new Color((float)minute / 120, 1 - (float)minute / 120, 1 - (float)minute / 120);
            if (TimeManager.Instance.Hour >= 17 && TimeManager.Instance.Hour < 20)
            {
                bloom.intensity.value = 1 + (float)((float)((TimeManager.Instance.Hour - 17f) * 30f + minute) / 90f) * 7f;
            }
        }



        if (TimeManager.Instance.Hour < 5 || (TimeManager.Instance.Hour >= 9 && TimeManager.Instance.Hour < 17) || TimeManager.Instance.Hour >= 21)
        {
            timeSpend = 0;
        }
    }
}
