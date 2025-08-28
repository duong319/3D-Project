using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPanel : MonoBehaviour
{
    public Transform contentRoot;
    public GameObject missionItemPrefab;
    public GameObject missionCompletePanel;

    private List<MissionItemUI> missionUIs = new List<MissionItemUI>();


    private void OnEnable()
    {      
        StartCoroutine(CheckMissionsRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
      missionCompletePanel.gameObject.SetActive(MissionManager.Instance.isRewardClaim);
    }

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

    private IEnumerator CheckMissionsRoutine()
    {
        ShowMissionPanel(); 

        while (true)
        {
            yield return new WaitForSeconds(2f); 
            ShowMissionPanel(); 
        }
    }
}
