using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireTime : MonoBehaviour
{
    public Light2D firelight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LightChange();
    }

    public void LightChange()
    {
        if (TimeManager.Instance.Hour >= 5 && TimeManager.Instance.Hour < 9)
        {
            float sky = 0.9f - (float)((float)((TimeManager.Instance.Hour - 5f) * 30f + TimeManager.Instance.Minute) / 120f) * 0.9f;
            firelight.intensity = sky;
        }

        if (TimeManager.Instance.Hour >= 9 && TimeManager.Instance.Hour < 17)
        {
            firelight.intensity = 0.1f;
        }


        if (TimeManager.Instance.Hour >= 17 && TimeManager.Instance.Hour < 21)
        {
            float sky = (float)((float)((TimeManager.Instance.Hour - 17f) * 30f + TimeManager.Instance.Minute) / 120f) * 0.9f;
            firelight.intensity = sky;
        }

        if ((TimeManager.Instance.Hour > 21 && TimeManager.Instance.Hour <= 24) || (TimeManager.Instance.Hour >= 0 && TimeManager.Instance.Hour < 6))
        {
            firelight.intensity = 0.9f;
        }
    }
}
