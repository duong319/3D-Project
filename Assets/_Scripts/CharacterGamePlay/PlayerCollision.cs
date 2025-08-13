using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
             
            PlayerController player = GetComponent<PlayerController>();
            if (player == null)
            {
                player = GetComponentInParent<PlayerController>();
            }

            if (player != null && player.isShieldAvtivate == false)
            {
                player.Die();
            }
            if (player.isShieldAvtivate == true)
            {
                Destroy(hit.gameObject);
                player.isShieldAvtivate = false;
            }
            else
            {
                Debug.LogWarning("PlayerController!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Coin"))
        {
           
            ScoreManager.Instance.AddCoin(1);
            Destroy(other.gameObject);
            MissionManager.Instance.ReportProgress(MissionType.CollectCoin, 1);
            CurrencyManager.Instance.AddCoins(1);
            AchievementManager.Instance.AddProgress(AchievementType.CollectCoins,1);
        }

        if (other.CompareTag("Shield"))
        {
           
            Destroy(other.gameObject);
            SpecialItemManager.Instance.UseItem(SpecialItemType.Shield);
            AchievementManager.Instance.AddProgress(AchievementType.PickupItem, 1);
        }
        if (other.CompareTag("Magnet"))
        {
            
            Destroy(other.gameObject);
            SpecialItemManager.Instance.UseItem(SpecialItemType.Magnet);
            AchievementManager.Instance.AddProgress(AchievementType.PickupItem, 1);
        }
        if (other.CompareTag("X2"))
        {
           
            Destroy(other.gameObject);
            SpecialItemManager.Instance.UseItem(SpecialItemType.ScoreMultiplier);
            AchievementManager.Instance.AddProgress(AchievementType.PickupItem, 1);
        }
    }
}
