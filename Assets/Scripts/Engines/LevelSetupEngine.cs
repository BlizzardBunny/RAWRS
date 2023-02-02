using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetupEngine : MonoBehaviour
{
    #region Object References
    [SerializeField] GameObject tasklistEntryPrefab;
    [SerializeField] Transform entryContainer;
    #endregion

    #region Variables
    private int numOnList = -1;
    private GameObject newEntry;
    private static string[] taskNames =
    {
        "Bathe Animal", "Prepare pet food", "Clean the center", "Check up on an animal"
    };
    #endregion

    private void Start()
    {
        RandomizeTasks(4);
    }

    public void RandomizeTasks(int maxTasks)
    {
        for (int i = 0; i < maxTasks; i++)
        {
            int taskID = Random.Range(0, 4);
            MakeEntry(taskID);
        }
    }

    public void MakeEntry(int i)
    {
        newEntry = Instantiate(tasklistEntryPrefab, entryContainer);
        TaskEntry entryData = newEntry.GetComponent<TaskEntry>();
        entryData.taskName.text = taskNames[i];
        entryData.taskNumber.text = (++numOnList).ToString();
    }
}
