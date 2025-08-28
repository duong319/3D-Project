
using UnityEngine;

public class CharacterUnlockPanel : MonoBehaviour
{
    [SerializeField] private GameObject HaileyUnlock;
    [SerializeField] private GameObject CaitlinUnlock;

    public void Haileyunlock()
    {
        HaileyUnlock.gameObject.SetActive(true);
    }

    public void Caitlinunlock()
    {
        CaitlinUnlock.gameObject.SetActive(true);
    }
    public void Continue()
    {
        HaileyUnlock.gameObject.SetActive(false);
        CaitlinUnlock.gameObject.SetActive(false);
    }
}
