using System.Collections.Generic;
using UnityEngine;

public class MissionPanel : MonoBehaviour
{
    public Transform contentRoot;
    public GameObject missionItemPrefab;

    private List<MissionItemUI> missionUIs = new List<MissionItemUI>();

    public void ShowMissionPanel()
    {

        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }


        foreach (var mission in MissionManager.Instance.currentMissions)
        {
            GameObject obj = Instantiate(missionItemPrefab, contentRoot);
            MissionItemUI ui = obj.GetComponent<MissionItemUI>();
            ui.Setup(mission);
            missionUIs.Add(ui);
        }
    }

   
    public void RefreshMissionProgress()
    {
        foreach (var ui in missionUIs)
        {
            ui.UpdateProgress();
        }
    }
}
