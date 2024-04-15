using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionView : MonoBehaviour, IPointerEnterHandler
{
    private bool showPanel = false;

    [SerializeField] private Building buildingTiedTo;
    [SerializeField] private UiMissionFullPanel fullPanel;
    [SerializeField] private UIMissionSmallPanel smallPanel;

    private float currentDuration;
    public float durationToHideFullPanel;

    void Awake()
    {
        gameObject.SetActive(true);
    }
    void Update()
    {
        UpdateStats();

        AnimatePanels();
    }

    private void OnEnable()
    {
        MissionManager missionManager = FindObjectOfType<MissionManager>();
        missionManager.createdMission += EnableShow;
        missionManager.completedMission += DisableShow;
    }

    private void OnDisable()
    {
        MissionManager missionManager = FindObjectOfType<MissionManager>();
        missionManager.createdMission -= EnableShow;
        missionManager.completedMission -= DisableShow;
    }

    private void EnableShow(Building building)
    {
        if (building == buildingTiedTo)
        {
            fullPanel.gameObject.SetActive(true);
            smallPanel.gameObject.SetActive(false);
            currentDuration = 0;
            showPanel = true;
        }
    }

    private void DisableShow(Building building)
    {
        if (building == buildingTiedTo)
        {
            fullPanel.gameObject.SetActive(false);
            smallPanel.gameObject.SetActive(false);
            showPanel = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (showPanel)
        {
            ShowFullPanel();
            currentDuration = 0;
        }
    }

    private void AnimatePanels()
    {
        currentDuration += Time.deltaTime;

        if (currentDuration >= durationToHideFullPanel)
        {
            ShowSmallPanel();
        }
    }

    private void ShowSmallPanel()
    {
        fullPanel.gameObject.SetActive(false);
        smallPanel.gameObject.SetActive(true);
    }

    private void ShowFullPanel()
    {
        fullPanel.gameObject.SetActive(true);
        smallPanel.gameObject.SetActive(false);
    }



    private void UpdateStats()
    {
        if (buildingTiedTo.currentMission != null)
        {
            if (buildingTiedTo.currentMission.ifMissionAvailable)
            {
                Mission currentMission = buildingTiedTo.currentMission;
                if (fullPanel.gameObject.activeInHierarchy)
                {
                    fullPanel.SetText(currentMission.CurrentWorkDone, currentMission.NumberOfWorkRequired, currentMission.reward, currentMission.DurationLeft);
                }

                if (smallPanel.gameObject.activeInHierarchy)
                {
                    smallPanel.SetText(currentMission.CurrentWorkDone, currentMission.NumberOfWorkRequired, currentMission.DurationLeft);
                }
            }
        }
    }
}
