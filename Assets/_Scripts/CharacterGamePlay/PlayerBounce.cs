using UnityEngine;
using System.Collections;

public class PlayerBounce : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isBouncing = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isBouncing) return;
        if (!controller.isGrounded)
            moveDirection += Physics.gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }


    public void Bounce(float upwardForce, float forwardForce, Vector3 padForward)
    {
        if (isBouncing) return;
        StartCoroutine(BounceRoutine(upwardForce, forwardForce, padForward));
    }

    private IEnumerator BounceRoutine(float upward, float forward, Vector3 padForward)
    {
        isBouncing = true;
        float timer = 0.1f;
        while (timer > 0)
        {
            moveDirection = Vector3.up * upward * 10f + padForward * forward * 10f;
            timer -= Time.deltaTime;
            yield return null;
        }
        while (!controller.isGrounded)
        {
            yield return null;
        }
        isBouncing = false;
    }
}
