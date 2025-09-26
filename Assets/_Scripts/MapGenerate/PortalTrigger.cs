using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public bool isExitPortal = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelGenerator generator = FindFirstObjectByType<LevelGenerator>();
            if (generator != null)
            {
                if (isExitPortal)
                {
                    Debug.Log("portal exit");
                    generator.ExitPortal();
                }
                else
                {
                    Debug.Log("portal enter");
                    generator.EnterPortal();
                }
            }
        }
    }
}
