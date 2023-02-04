using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetupEngine : MonoBehaviour
{
    #region Object References
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private GameObject tasklistEntryPrefab;
    [SerializeField] private Transform entryContainer;
    [SerializeField] private GameObject[] bathingTaskStations;
    [SerializeField] private GameObject[] feedingTaskStations;
    [SerializeField] private GameObject[] cleaningTaskStations;
    [SerializeField] private GameObject[] checkupTaskStations;

    #endregion

    #region Variables
    private int numOnList = 0;
    private GameObject[] entries;
    private static bool init = true;
    private static string[] taskNames =
    {
        "Bathe Animal", "Prepare pet food", "Clean up a kennel", "Check up on an animal"
    };
    private static bool[] taskCompletion = new bool[1];
    #endregion

    private void Start()
    {
        StaticItems.inTutorial = false;

        if (init)
        {
            entries = new GameObject[taskCompletion.Length];
            RandomizeTasks(taskCompletion.Length);

            for (int i = 0; i < taskCompletion.Length; i++)
            {
                taskCompletion[i] = false;
            }

            init = false;
        }

        if (TaskEngine.currStationID > -1)
        {
            MarkCompleteTask(TaskEngine.currStationID);
        }

        if (CheckLevelComplete())
        {
            endCanvas.enabled = true;
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

            MakeEntry(taskType, taskStationID);
        }
    }

    public void MakeEntry(int taskType, int taskStationID)
    {
        entries[numOnList] = Instantiate(tasklistEntryPrefab, entryContainer);
        TaskEntry entryData = entries[numOnList].GetComponent<TaskEntry>();
        entryData.taskName.text = taskNames[taskType];
        entryData.taskNumber.text = (++numOnList).ToString();

        switch (taskType)
        { 
            case 0: entryData.taskStationMarker = bathingTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
            case 1: entryData.taskStationMarker = feedingTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
            case 2: entryData.taskStationMarker = cleaningTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
            case 3: entryData.taskStationMarker = checkupTaskStations[taskStationID].transform.GetChild(0).gameObject; break;
        }

        entryData.taskStationMarker.GetComponentInParent<TaskStationInfo>().isActive = true;
        entryData.taskStationMarker.GetComponentInParent<TaskStationInfo>().listID = numOnList;
    }

    public void MarkCompleteTask(int listID)
    {
        taskCompletion[listID-1] = true;
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
}
