using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject shieldFXPrefab;
    private GameObject shieldInstance;

    public void ActivateShield()
    {
        if (shieldInstance) Destroy(shieldInstance);
        shieldInstance = Instantiate(shieldFXPrefab, transform.position, Quaternion.identity, transform);
    }

    public void DeactivateShield()
    {
        if (shieldInstance) Destroy(shieldInstance);
    }
}
