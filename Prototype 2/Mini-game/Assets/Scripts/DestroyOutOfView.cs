using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    #region parameters

    [SerializeField] float maxTopBound = 35f;
    [SerializeField] float maxBottomBound = -8f;
    [SerializeField] float maxSideBound = 24f;

    #endregion

    // Update is called once per frame
    void Update()
    {
        bool isOverVerticalBounds = (transform.position.z > maxTopBound) || (transform.position.z < maxBottomBound);
        bool isOverSideBounds = (transform.position.x > maxSideBound) || (transform.position.x < -maxSideBound);

        if (isOverVerticalBounds || isOverSideBounds)
        {
            Destroy(gameObject);
        }
    }
}
