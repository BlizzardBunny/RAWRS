using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSetupEngine : MonoBehaviour
{
    #region Object References

    [SerializeField] private GameObject tasklistEntryPrefab;
    [SerializeField] private Transform entryContainer;
    [SerializeField] private GameObject[] bathingTaskStations;
    [SerializeField] private GameObject[] feedingTaskStations;
    [SerializeField] private GameObject[] cleaningTaskStations;
    [SerializeField] private GameObject[] checkupTaskStations;
    [SerializeField] private bool isOverworld = false;

    #endregion

    #region Variables

    private int numOnList = 0;
    private List<GameObject> entries = new List<GameObject>();
    private static string[] taskNames =
    {
        "Bathe Animal", "Prepare pet food", "Clean up a kennel", "Check up on an animal"
    };
    
    #endregion

    #region Custom functions

    public static void ResetStaticVars()
    {
        StaticItems.init = true;
        StaticItems.isEnding = false;
        StaticItems.entriesData.Clear();
    }

    public void Init()
    {
        if (isOverworld)
        {
            StaticItems.inTutorial = false;
        }     
        
        if (StaticItems.inTutorial && StaticItems.tutorialState <= 1)
        {
            StaticItems.taskCompletion = new bool[1];
            StaticItems.init = false;
            StaticItems.entriesData.Add(new System.Tuple<int, int>(0, 0));
        }

        if (StaticItems.init)
        {          
            if (!StaticItems.inTutorial)
            {
                StaticItems.taskCompletion = new bool[StaticItems.levelNumber + 1];
            }

            StaticItems.entriesData.Clear();
            TaskEngine.currStationID = -1;

            for (int i = 0; i < StaticItems.taskCompletion.Length; i++)
            {
                StaticItems.taskCompletion[i] = false;
            }

            RandomizeTasks(StaticItems.taskCompletion.Length);

            StaticItems.init = false;
        }
        else
        {
            for (int i = 0; i < StaticItems.entriesData.Count; i++)
            {
                MakeEntry(StaticItems.entriesData[i].Item1, StaticItems.entriesData[i].Item2);
                if (StaticItems.taskCompletion[i])
                {
                    MarkCompleteTask(i);
                }
            }
        }

        if (TaskEngine.currStationID > -1)
        {
            MarkCompleteTask(TaskEngine.currStationID);
        }

        if (CheckLevelComplete())
        {
            StaticItems.isEnding = true;
            MakeEntry();
        }
    }

    public void RandomizeTasks(int maxTasks)
    {
        for (int i = 0; i < maxTasks; i++)
        {
            int taskType = 0;
            int taskStationID = 0;

            if (StaticItems.levelNumber == 1)
            {
                taskType = Random.Range(0, 2); // 0 or 1

                if (taskType == 0)
                {
                    taskStationID = 1;
                }
                else
                {
                    taskStationID = 0;
                }
            }
            else
            {
                if (StaticItems.levelNumber == 2)
                {
                    if (i == 0)
                    {
                        taskType = 2;
                    }
                    else
                    {
                        taskType = Random.Range(0, 3);
                    }
                }
                else if (StaticItems.levelNumber == 3)
                {
                    if (i == 0)
                    {
                        taskType = 3;
                    }
                    else
                    {
                        taskType = Random.Range(0, 4);
                    }
                }
                else
                {
                    taskType = Random.Range(0, 4);
                }

                switch (taskType)
                {
                    case 0: taskStationID = Random.Range(0, bathingTaskStations.Length); break;
                    case 1: taskStationID = Random.Range(0, feedingTaskStations.Length); break;
                    case 2: taskStationID = Random.Range(0, cleaningTaskStations.Length); break;
                    case 3: taskStationID = Random.Range(0, checkupTaskStations.Length); break;
                }
            }

            if (CheckIsUniqueTask(taskType, taskStationID))
            {
                MakeEntry(taskType, taskStationID);
            }
            else
            {
                i--;
            }
        }
    }

    public void MakeEntry(int taskType, int taskStationID)
    {
        GameObject entry = Instantiate(tasklistEntryPrefab, entryContainer);
        TaskEntry entryData = entry.GetComponent<TaskEntry>();
        entryData.taskName.text = taskNames[taskType];
        entryData.taskNumber.text = (++numOnList).ToString();

        switch (taskType)
        {
            case 0: entryData.taskStationMarker = bathingTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
            case 1: entryData.taskStationMarker = feedingTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
            case 2: entryData.taskStationMarker = cleaningTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
            case 3: entryData.taskStationMarker = checkupTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
        }

        entryData.taskStationMarker.GetComponentInParent<TaskStationInfo>().listID = numOnList - 1;

        entries.Add(entry);

        if (StaticItems.init)
        {
            StaticItems.entriesData.Add(new System.Tuple<int, int>(taskType, taskStationID));
        }
    }

    public void MakeEntry()
    {
        GameObject entry = Instantiate(tasklistEntryPrefab, entryContainer);
        TaskEntry entryData = entry.GetComponent<TaskEntry>();
        entryData.taskName.text = "Talk to Theo";
        entryData.taskNumber.text = (++numOnList).ToString();

        entries.Add(entry);
    }

    public void MarkCompleteTask(int listID)
    {
        StaticItems.taskCompletion[listID] = true;
        entries[listID].GetComponent<TaskEntry>().numberBG.color = Color.green;
    }

    public bool CheckLevelComplete()
    {
        bool ret = true;
        foreach (bool b in StaticItems.taskCompletion)
        {
            if (!b)
            {
                ret = false;
                break;
            }
        }
        return ret;
    }

    private bool CheckIsUniqueTask(int taskType, int taskStationID)
    {
        foreach(System.Tuple<int,int> entryData in StaticItems.entriesData)
        {
            if (entryData.Item1 == taskType && entryData.Item2 == taskStationID)
            {
                return false;
            }
        }

        return true;
    }

#endregion

    #region Unity functions

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (StaticItems.init)
        {
            Init();
        }
    }

    #endregion
}
