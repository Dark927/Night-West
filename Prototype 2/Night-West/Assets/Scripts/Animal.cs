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

    [SerializeField] int foodToLeave = 2;
    int actualFood = 0;

    [SerializeField] int scoreForFeed = 1;

    [Space]
    [Header("Run away Settings")]

    [SerializeField] float runAwayBottomBound = -5f;
    [SerializeField] float runAwaySideBounds = 24f;

    float startPositionX = 0f;

    bool isLost = false;

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods


    private void Start()
    {
        startPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLost)
        {
            Vector3 animalPosition = transform.position;

            // Check different conditions for animal run away (right side, left side, bottom

            bool isRunAway = false;

            if((int)startPositionX < 0)
            {
                isRunAway = (animalPosition.x > runAwaySideBounds);
            }
            else if((int)startPositionX > 0)
            {
                isRunAway = (animalPosition.x < (-runAwaySideBounds));
            }

            isRunAway = isRunAway || (animalPosition.z < runAwayBottomBound);

            // If the animal run away, take away the player's HP

            if (isRunAway)
            {
                string playerTag = "Player";
                GameObject player = GameObject.FindWithTag(playerTag);

                if (player != null)
                {
                    player.GetComponent<PlayerController>().TakeDamage();
                    isLost = true;
                }
            }
        }
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

    #endregion
}
