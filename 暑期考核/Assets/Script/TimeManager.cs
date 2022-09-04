using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : Singleton<TimeManager>
{
    public TimeData_SO timedata;
    public Light2D skylight;

    public int Hour
    {
        get
        {
            if (timedata != null)
                return timedata.hour;
            else return 0;
        }
        set
        {
            timedata.hour = value;
        }
    }
    public int Minute
    {
        get
        {
            if (timedata != null)
                return timedata.minute;
            else return 0;
        }
        set
        {
            timedata.minute = value;
        }
    }



    int hour;
    int minute;
    // 已经花费的时间 
    float timeSpend = 0.0f;
    // 显示时间区域的文本 
    public Text text_timeSpend;

    void Start()
    {
        hour = Hour;
        minute = Minute;
        timeSpend = Minute;
    }

    void Update()
    {
        SetDay();
        TimeUpdate();

    }

    public void TimeUpdate()
    {
        timeSpend += Time.deltaTime;

        minute = (int)timeSpend;
        if (minute == 30)
        {
            hour += minute / 30;
            minute = 0;
            timeSpend = 0;
        }
        if (hour == 24)
            hour = 0;

        Hour = hour;
        Minute = minute;

        text_timeSpend.text = string.Format("{0:D2}:{1:D2}", timedata.hour, timedata.minute);
    }

    public void SetDay()/* 设置灯光随时间变化 */
    {
        if (hour >= 5 && hour < 9)
        {
            float sky = 0.1f + (float)((float)((hour - 5f) * 30f + minute) / 120f) * 0.9f;
            skylight.intensity = sky;
        }

        if (hour >= 9 && hour < 17)
        {
            skylight.intensity = 1;
        }


        if (hour >= 17 && hour < 21)
        {
            float sky = 1 - (float)((float)((hour - 17f) * 30f + minute) / 120f) * 0.9f;
            skylight.intensity = sky;
        }

        if ((hour >= 21 && hour <= 24) || (hour >= 0 && hour < 5))
        {
            skylight.intensity = 0.1f;
        }
    }

    public void SaveTime()
    {
        SaveManager.Instance.Save(timedata, timedata.name);
    }
}
