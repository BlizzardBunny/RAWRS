using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    #endregion

    private void Start()
    {
        defaultSprite = entryBG.sprite;
    }

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
        taskStationMarker.SetActive(!taskStationMarker.activeSelf);
    }


}
