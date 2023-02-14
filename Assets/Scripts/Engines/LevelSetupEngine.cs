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
    public static bool init = true;
    private static bool[] taskCompletion;
    private static List<System.Tuple<int, int>> entriesData = new List<System.Tuple<int, int>>();
    private static string[] taskNames =
    {
        "Bathe Animal", "Prepare pet food", "Clean up a kennel", "Check up on an animal"
    };

    #endregion

    #region Custom functions

    public void Init()
    {
        StaticItems.firstTime = false;

        if (isOverworld)
        {
            StaticItems.inTutorial = false;
        }     
        
        if (StaticItems.inTutorial)
        {
            taskCompletion = new bool[1];
            init = false;
            entriesData.Add(new System.Tuple<int, int>(0, 0));
        }

        if (init)
        {          
            if (!StaticItems.inTutorial)
            {
                taskCompletion = new bool[LevelEndEngine.levelNumber + 1];
            }

            entriesData.Clear();
            TaskEngine.currStationID = -1;

            for (int i = 0; i < taskCompletion.Length; i++)
            {
                taskCompletion[i] = false;
            }

            RandomizeTasks(taskCompletion.Length);

            init = false;
        }
        else
        {
            for (int i = 0; i < entriesData.Count; i++)
            {
                MakeEntry(entriesData[i].Item1, entriesData[i].Item2);
                if (taskCompletion[i])
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
            init = true;
            SceneManager.LoadScene("Level End");
        }
    }

    public void RandomizeTasks(int maxTasks)
    {
        for (int i = 0; i < maxTasks; i++)
        {
            int taskType = Random.Range(0, 4);

            int taskStationID = 0;

            switch (taskType)
            {
                case 0: taskStationID = Random.Range(0, bathingTaskStations.Length); break;
                case 1: taskStationID = Random.Range(0, feedingTaskStations.Length); break;
                case 2: taskStationID = Random.Range(0, cleaningTaskStations.Length); break;
                case 3: taskStationID = Random.Range(0, checkupTaskStations.Length); break;
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

        if (init)
        {
            entriesData.Add(new System.Tuple<int, int>(taskType, taskStationID));
        }
    }

    public void MarkCompleteTask(int listID)
    {
        taskCompletion[listID] = true;
        entries[listID].GetComponent<TaskEntry>().numberBG.color = Color.green;
    }

    public bool CheckLevelComplete()
    {
        bool ret = true;
        foreach (bool b in taskCompletion)
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
        foreach(System.Tuple<int,int> entryData in entriesData)
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
        if (init)
        {
            Init();
        }
    }

    #endregion
}
