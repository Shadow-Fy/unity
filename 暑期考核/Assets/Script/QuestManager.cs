using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : Singleton<QuestManager>
{
    [System.Serializable]
    public class QuestTask
    {
        public QuestData_SO questdata;
        public bool isStarted { get { return questdata.isstarted; } set { questdata.isstarted = value; } }
        public bool isComplete { get { return questdata.iscomplete; } set { questdata.iscomplete = value; } }
        public bool isFinished { get { return questdata.isfinished; } set { questdata.isfinished = value; } }

    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public List<QuestTask> tasks = new List<QuestTask>();

    void Start()
    {
        LoadQuestManager();
    }

    public void LoadQuestManager()
    {
        var questcount = PlayerPrefs.GetInt("QuestCount");
        for (int i = 0; i < questcount; i++)
        {
            var newquest = ScriptableObject.CreateInstance<QuestData_SO>();
            SaveManager.Instance.Load(newquest, "task" + i);
            tasks.Add(new QuestTask { questdata = newquest });
        }
    }

    public void SaveQuestManager()
    {
        PlayerPrefs.SetInt("QuestCount", tasks.Count);
        for (int i = 0; i < tasks.Count; i++)
        {
            SaveManager.Instance.Save(tasks[i].questdata, "task" + i);
        }
    }

    public void UpdateQuestProgress(string requirename, int amount)
    {
        foreach (var task in tasks)
        {
            if (task.isFinished)
                continue;
            var matchtask = task.questdata.questrequires.Find(r => r.name == requirename);
            if (matchtask != null)
            {
                matchtask.currentamount += amount;
            }

            task.questdata.CheckQuestProgress();
        }
    }

    public bool HaveQuest(QuestData_SO data)
    {
        if (data != null)
            return tasks.Any(q => q.questdata.questname == data.questname);
        else return false;
    }

    public QuestTask GetTask(QuestData_SO data)
    {
        return tasks.Find(q => q.questdata.questname == data.questname);
    }
}
