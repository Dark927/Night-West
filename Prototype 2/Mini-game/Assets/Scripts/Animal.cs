using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters
    // --------------------------------------------------------------------------------------------------------------

    #region parameters

    [Header("Run away Settings")]

    [SerializeField] int foodToLeave = 2;
    int actualFood = 0;

    [Space]
    [Header("Run away Settings")]

    [SerializeField] float runAwayBottomBound = -5f;
    [SerializeField] float runAwaySideBounds = 24f;

    bool isLost = false;

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods

    // Update is called once per frame
    void Update()
    {
        if (!isLost)
        {
            Vector3 animalPosition = transform.position;

            bool isRunAway = (animalPosition.z < runAwayBottomBound) || (animalPosition.x > runAwaySideBounds) || (animalPosition.x < (-runAwaySideBounds));

            // Если животное сбежало, отнимаем HP у игрока
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

        if(actualFood == foodToLeave)
        {
            string playerTag = "Player";
            GameObject player = GameObject.FindWithTag(playerTag);

            if (player != null)
            {
                player.GetComponent<PlayerController>().AddScore();
            }

            Destroy(gameObject);
        }
    }

    #endregion
}
