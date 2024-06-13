using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    #region parameters

    [SerializeField] float runAwayBottomBound = -5f;
    [SerializeField] float runAwaySideBounds = 24f;

    bool isLost = false;

    #endregion

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
}
