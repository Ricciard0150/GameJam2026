using UnityEngine;
using System.Collections.Generic;

public class MissionSystem : MonoBehaviour
{
    public static MissionSystem Instance; // Singleton

    [SerializeField] private List<Mission> missions = new List<Mission>();
    [SerializeField] private MissionUI missionUI;

    private int completedCount = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (missionUI != null)
            missionUI.UpdateUI(missions);
    }

    public void CompleteMission(string missionName)
    {
        foreach (Mission mission in missions)
        {
            if (mission.missionName == missionName && !mission.isCompleted)
            {
                mission.Complete();
                completedCount++;

                if (missionUI != null)
                    missionUI.UpdateUI(missions);

                CheckAllCompleted();
                return;
            }
        }
    }

    public void CompleteMissionByObject(GameObject obj)
    {
        foreach (Mission mission in missions)
        {
            if (mission.targetObject == obj && !mission.isCompleted)
            {
                mission.Complete();
                completedCount++;

                if (missionUI != null)
                    missionUI.UpdateUI(missions);

                CheckAllCompleted();
                return;
            }
        }
    }

    public List<Mission> GetMissions() => missions;

    private void CheckAllCompleted()
    {
        if (completedCount >= missions.Count)
        {
            Debug.Log("🎉 TODAS AS MISSÕES COMPLETADAS!");
            // Aqui você pode ativar algo, tipo portal, fim de jogo, etc.
        }
    }
}