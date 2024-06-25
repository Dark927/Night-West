using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters
    // --------------------------------------------------------------------------------------------------------------

    #region Parameters

    [Header("Bounds")]
    [SerializeField] float maxTopBound = 35f;
    [SerializeField] float maxBottomBound = -10f;
    [SerializeField] float maxSideBound = 24f;

    #endregion


    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods

    // Update is called once per frame
    void Update()
    {
        bool isOverVerticalBounds = (transform.position.z > maxTopBound) || (transform.position.z < maxBottomBound);
        bool isOverSideBounds = (transform.position.x > maxSideBound) || (transform.position.x < -maxSideBound);

        if (isOverVerticalBounds || isOverSideBounds)
        {
            bool isFood = (GetComponent<Food>() == null) ? false : true;

            if (isFood)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    #endregion
}
