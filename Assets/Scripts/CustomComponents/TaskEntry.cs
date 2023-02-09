using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TaskEntry : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Object References

    public Image numberBG;
    public TMPro.TextMeshProUGUI taskNumber, taskName;
    public GameObject taskStationMarker;
    [SerializeField] private Image entryBG;
    [SerializeField] private Sprite mouseOverSprite;

    #endregion

    #region Variables

    private Sprite defaultSprite;
    private bool init = true;

    #endregion

    public void OnPointerEnter(PointerEventData eventData)
    {
        entryBG.sprite = mouseOverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        entryBG.sprite = defaultSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        taskStationMarker.GetComponentInParent<TaskStationInfo>().isActive = !taskStationMarker.GetComponentInParent<TaskStationInfo>().isActive;
        taskStationMarker.SetActive(!taskStationMarker.activeSelf);
    }

    private void Start()
    {
        if (init)
        {
            defaultSprite = entryBG.sprite;
            init = false;
        }
    }
}
