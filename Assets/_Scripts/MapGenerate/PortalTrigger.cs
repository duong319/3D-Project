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
                    generator.ExitPortal();
                }
                else
                {
                    generator.EnterPortal();
                }
            }
        }
    }
}
