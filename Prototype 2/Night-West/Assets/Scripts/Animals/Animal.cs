using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters
    // --------------------------------------------------------------------------------------------------------------

    #region parameters

    [Header("Main Settings")]

    [SerializeField] float basicSpeed;
    float actualSpeed = 0;

    [SerializeField] int basicDamage = 1;

    [SerializeField] int foodToLeave = 2;
    int actualFood = 0;

    [SerializeField] int scoreForFeed = 1;

    [Space]
    [Header("Run away Settings")]

    [SerializeField] float runAwayBottomBound = -5f;
    [SerializeField] float runAwaySideBounds = 24f;

    float startPositionX = 0f;
    bool isLost = false;

    Animator animator;
    string playerTag = "Player";

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        actualSpeed = basicSpeed;
        startPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();

        if (!isLost)
        {

            Vector3 animalPosition = transform.position;

            // Check different conditions for animal run away (right side, left side, bottom

            bool isRunAway = false;

            if ((int)startPositionX < 0)
            {
                isRunAway = (animalPosition.x > runAwaySideBounds);
            }
            else if ((int)startPositionX > 0)
            {
                isRunAway = (animalPosition.x < (-runAwaySideBounds));
            }

            isRunAway = isRunAway || (animalPosition.z < runAwayBottomBound);

            // If the animal run away, take away the player's HP

            if (isRunAway)
            {
                GameManager gameManager = FindAnyObjectByType<GameManager>();

                if (gameManager != null)
                {
                    gameManager.HitPlayer(basicDamage);
                    isLost = true;
                }
            }
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * actualSpeed);
    }


    #endregion


    // --------------------------------------------------------------------------------------------------------------
    // Public Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Public Methods 

    public void Feed(int foodAmount = 1)
    {
        actualFood += foodAmount;

        // Update feed bar

        FeedStatus feedBar = GetComponentInChildren<FeedStatus>(true);

        if (feedBar != null)
        {
            feedBar.gameObject.SetActive(true);
            feedBar.UpdateFeedBarStatus(foodToLeave, actualFood);
        }
        else
        {
            Debug.Log($"# Error : Animal.cs, Object -> {gameObject.name}, FeedStatus is null!");
        }

        // Check if the animal has been fed

        if (actualFood == foodToLeave)
        {
            GameManager gameManager = GameObject.FindAnyObjectByType<GameManager>();

            if (gameManager != null)
            {
                gameManager.AddScore(scoreForFeed);
            }
            else
            {
                Debug.Log($"# Error : Animal.cs, Object -> {gameObject.name}, gameManager is null!");
            }

            Destroy(gameObject);
        }
    }

    public void IdleState()
    {
        actualSpeed = 0f;
        animator.SetFloat("Speed_f", 0);
    }

    #endregion
}
