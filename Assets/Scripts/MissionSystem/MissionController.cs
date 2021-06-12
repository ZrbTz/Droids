using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    [Header("Missions Settings")]
    [SerializeField]
    private Mission[] missions;
    private Mission currentMission;
    private int numMissions;
    private int currentMissionIndex;
    private bool isActive = true;
    private GameObject player;

    //[Header("UI Settings")]
    //[SerializeField]
    //private MissionUI missionUI;

    void Awake()
    {
        player = GameObject.Find("P_First_Person_Controller");
    }

    void Start()
    {
        numMissions = missions.Length;
        if (numMissions > 0)
        {
            currentMissionIndex = 0;
            StartMission(0);
        }
    }


    void Update()
    {
        if (isActive)
        {
            if (currentMission.IsCompleted(player))
            {
                currentMissionIndex++;
                if (currentMissionIndex < numMissions)
                {
                    StartMission(currentMissionIndex);
                }
                else
                {
                    isActive = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //missionUI.SwitchWindow();
        }
    }

    private void StartMission(int index)
    {
        currentMission = missions[currentMissionIndex];
        UpdateUI(missions[currentMissionIndex]);
    }

    private void UpdateUI(Mission mission)
    {
        //missionUI.UpdateMission(mission.GetTitle(), mission.GetDescription());
    }
}
