using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CoinMovement : MonoBehaviour
{
    private Transform player;

    private float magnetSpeed = 500f;
    private bool isBeingAttracted = false;

    public void AttractTo(Transform target, float speed)
    {
        Debug.Log("attract");
        player = target;
        magnetSpeed = speed;
       
        isBeingAttracted = true;
    }

    private void Update()
    {
        if (isBeingAttracted && player != null)
        {
          
            transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);

        
        }
    }
}
