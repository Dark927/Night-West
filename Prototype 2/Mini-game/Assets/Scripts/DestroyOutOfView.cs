using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    #region parameters

    [SerializeField] float maxTopBound = 35f;
    [SerializeField] float maxBottomBound = -10f;
    [SerializeField] float maxSideBound = 25f;

    #endregion

    // Update is called once per frame
    void Update()
    {
        bool isOverVerticalBounds = (transform.position.z > maxTopBound) || (transform.position.z < maxBottomBound);
        bool isOverSideBounds = (transform.position.x > maxSideBound) || (transform.position.x < -maxSideBound);

        if (isOverVerticalBounds || isOverSideBounds)
        {
            bool isAnimal = (transform.position.z < maxBottomBound) || isOverSideBounds;

            // ≈сли это животное, тогда мы должны отн€ть HP у игрока

            if (isAnimal)
            {
                Debug.Log("animal");
                string playerTag = "Player";
                GameObject player = GameObject.FindWithTag(playerTag);

                if (player != null)
                {
                    player.GetComponent<PlayerController>().TakeDamage();
                }
            }

            Destroy(gameObject);
        }
    }
}
