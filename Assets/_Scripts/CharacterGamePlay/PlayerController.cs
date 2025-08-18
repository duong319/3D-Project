using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
   
    private Vector3 direction;
    public Animator animator;
    private bool isDead = false;
    public static PlayerController Instance;
    public GameObject shieldVisual;
    public GameObject magnetArea;
    public bool isShieldAvtivate = false;
    public bool isMagnetAvtivate = false;
    public bool isHeadStartAvtivate = false;
    public bool isScoreMultiplierAvtivate = false;
    public bool isScoreBoosterAvtivate = false;


    [Header("Movement")]

    public float laneDistance = 20f;
    private int currentLane = 1;
    public float forwardSpeed = 20f;

    public float laneSwitchSpeed = 10f;

    [Header("Jump")]
    public float jumpForce = 14f;
    public float gravity = -40f;
    public float downForce = -10f;



    [Header("Slide")]
    public float slideDuration = 0.5f;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private Vector3 originalCenter;
    private float originalHeight;

    private void Awake()
    {
        Instance = this;
        AudioManager.Instance.Play("GamePlayBG");
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalCenter = controller.center;
        originalHeight = controller.height;
        animator = GetComponent<Animator>();
        ScoreManager.Instance.Reset();
    }

    void Update()
    {
        if (isDead) return;
        float targetX = (currentLane - 1) * laneDistance;
        float deltaX = targetX - transform.position.x;
       
        Vector3 moveVector = new Vector3(deltaX, direction.y, forwardSpeed);
        controller.Move(moveVector * Time.deltaTime * laneSwitchSpeed);


        if (SwipeManager.swipeRight)
        {
            if (controller.isGrounded)
            {
                int rand = Random.Range(0, 2);
                string dogeState = rand == 0 ? "DodgeRight1" : "DodgeRight2";

                animator.SetTrigger(dogeState);
            }
            MoveLane(1);
        }

        if (SwipeManager.swipeLeft)
        {

            if (controller.isGrounded)
            {
                int rand = Random.Range(0, 2);
                string dogeState = rand == 0 ? "DodgeLeft1" : "DodgeLeft2";

                animator.SetTrigger(dogeState);
            }
            MoveLane(-1);
        }

        if (SwipeManager.swipeUp && controller.isGrounded)
        {
            Jump();
        }
        if (SwipeManager.swipeDown && !isSliding)
        {
            if (controller.isGrounded)
            {
                StartSlide();
            }
            else
            {
                Vector3 Down = new Vector3(currentLane, downForce, forwardSpeed);
                controller.Move(Down * Time.deltaTime);
                direction.y = downForce;
                Debug.Log("down");
                animator.SetTrigger("Landing");
            }
        }


        if (controller.isGrounded)
            direction.y = -2f;
        else
            direction.y += gravity * Time.deltaTime;




        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0f)
                EndSlide();
        }
    }

    private void MoveLane(int direction)
    {
        AudioManager.Instance.Play("Swipe");
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    private void Jump()
    {
        AudioManager.Instance.Play("Swipe");
        int rand = Random.Range(0, 2);
        string jumpState = rand == 0 ? "Jump1" : "Jump2";

        animator.SetTrigger(jumpState);
        Vector3 move = new Vector3(currentLane, jumpForce, forwardSpeed);
        controller.Move(move * Time.deltaTime);
        direction.y = jumpForce;
        MissionManager.Instance.ReportProgress(MissionType.Jump, 1);
        AchievementManager.Instance.AddProgress(AchievementType.Jump, 1);
    }

    private void StartSlide()
    {
        AudioManager.Instance.Play("Swipe");
        int rand = Random.Range(0, 2);
        string slideState = rand == 0 ? "Slide1" : "Slide2";

        animator.SetTrigger(slideState);
        isSliding = true;
        slideTimer = slideDuration;
        controller.height = originalHeight / 2f;
        controller.center = originalCenter / 2f;
    }

    private void EndSlide()
    {
        isSliding = false;
        controller.height = originalHeight;
        controller.center = originalCenter;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        AudioManager.Instance.Play("Dead");
        AudioManager.Instance.Stop("GamePlayBG");
        animator.SetTrigger("Die");
        forwardSpeed = 0f;
        direction = Vector3.zero;
        Debug.Log("Die");
        StartCoroutine(ShowRevivePanel());
        AchievementManager.Instance.AddProgress(AchievementType.PlayCount, 1);
        
    }

    public void SetShield(bool active)
    {
        isShieldAvtivate = active;
        shieldVisual.SetActive(active);
       
    }

    public void SetMagnet(bool active)
    {
        isMagnetAvtivate=active;
        magnetArea.SetActive(active);
       
    }

    public void ActivateHeadstart()
    {
        isHeadStartAvtivate = true;
    }

    public void EndHeadstart()
    {
        isHeadStartAvtivate=false;
    }

    public void SetMultiplier(int value)
    {
      
        CurrencyManager.Instance.scoreMultiplier *= value;
    }
    public void EndMultiplier(int value)
    {
       
        CurrencyManager.Instance.scoreMultiplier /= value;
    }

    public void AddMultiplier(int value)
    {
        CurrencyManager.Instance.scoreMultiplier += value;
    }

    IEnumerator ShowRevivePanel()
    {
        yield return new WaitForSeconds(2f);
        FindFirstObjectByType<ReviveUi>().ShowPanel();
       

    }




}
