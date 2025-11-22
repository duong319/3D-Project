using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private int playerHealth = 4;
    public GameObject ShieldCrashEfx;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        PlayerController player = GetComponent<PlayerController>();
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            if (player == null)
            {
                player = GetComponentInParent<PlayerController>();
            }

            if (player != null && player.isShieldAvtivate == false)
            {
                player.Die();
            }
            else if (player.isShieldAvtivate == true)
            {
                StartCoroutine(shieldCrashEfx());           
                Destroy(hit.transform.parent.gameObject);
                player.SetShield(false);
                FindFirstObjectByType<SpecialItemUI>().OnDestroy();
                player.isShieldAvtivate = false;
            }
      
        }
        else if (hit.gameObject.CompareTag("SideObstacle"))
        {
            if (player == null)
            {
                player = GetComponentInParent<PlayerController>();
            }
            if (player != null && player.isShieldAvtivate == false)
            {
                player.isHurt = true;
                player.KnockBack();
                playerHealth -= 1;
               
                if (playerHealth <= 0)
                    player.Die();
            }
            else if (player.isShieldAvtivate == true)
            {
                StartCoroutine(shieldCrashEfx());       
                Destroy(hit.transform.parent.gameObject);
                player.SetShield(false);
                FindFirstObjectByType<SpecialItemUI>().OnDestroy();
                player.isShieldAvtivate = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            AudioManager.Instance.Play("Coin");
            ScoreManager.Instance.AddCoin(1);
            Destroy(other.gameObject);
            MissionManager.Instance.ReportProgress(MissionType.CollectCoin, 1);
            CurrencyManager.Instance.AddCoins(1);
            AchievementManager.Instance.AddProgress(AchievementType.CollectCoins, 1);
        }

        if (other.CompareTag("Shield"))
        {
            AudioManager.Instance.Play("PickUp");
            Destroy(other.gameObject);
            SpecialItemManager.Instance.UseItem(SpecialItemType.Shield);
            AchievementManager.Instance.AddProgress(AchievementType.PickupItem, 1);
        }
        if (other.CompareTag("Magnet"))
        {
            AudioManager.Instance.Play("PickUp");
            Destroy(other.gameObject);
            SpecialItemManager.Instance.UseItem(SpecialItemType.Magnet);
            AchievementManager.Instance.AddProgress(AchievementType.PickupItem, 1);
        }
        if (other.CompareTag("X2"))
        {
            AudioManager.Instance.Play("PickUp");
            Destroy(other.gameObject);
            SpecialItemManager.Instance.UseItem(SpecialItemType.ScoreMultiplier);
            AchievementManager.Instance.AddProgress(AchievementType.PickupItem, 1);
        }
    }

    private IEnumerator shieldCrashEfx()
    {
        ShieldCrashEfx.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        ShieldCrashEfx.gameObject.SetActive(false);
    }
}
