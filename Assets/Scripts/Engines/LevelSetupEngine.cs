using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetupEngine : MonoBehaviour
{
    #region Object References
    [SerializeField] private GameObject tasklistEntryPrefab;
    [SerializeField] private Transform entryContainer;
    [SerializeField] private GameObject[] taskStationMarkers;
    #endregion

    #region Variables
    private int numOnList = 0;
    private GameObject newEntry;
    private static string[] taskNames =
    {
        "Bathe Animal", "Prepare pet food", "Clean up a kennel", "Check up on an animal"
    };
    #endregion

    private void Start()
    {
        foreach (GameObject x in taskStationMarkers)
        {
            x.SetActive(false);
        }
        RandomizeTasks(4);
    }

    public void RandomizeTasks(int maxTasks)
    {
        StaticItems.lvlTaskIDs = new int[maxTasks];
        for (int i = 0; i < maxTasks; i++)
        {
            int taskID = Random.Range(0, 4);
            StaticItems.lvlTaskIDs[i] = taskID;
            MakeEntry(taskID);
        }
    }

    public void MakeEntry(int i)
    {
        newEntry = Instantiate(tasklistEntryPrefab, entryContainer);
        TaskEntry entryData = newEntry.GetComponent<TaskEntry>();
        entryData.taskName.text = taskNames[i];
        entryData.taskStationMarkers = taskStationMarkers[i];
        entryData.taskNumber.text = (++numOnList).ToString();
    }
}
