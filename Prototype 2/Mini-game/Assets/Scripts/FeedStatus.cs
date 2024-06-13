using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedStatus : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters
    // --------------------------------------------------------------------------------------------------------------

    #region Parameters 

    [SerializeField] Transform feedBar;

    float minScale = 0;
    float maxScale = 1;

    int minFeed = 0;

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods 

    private void Start()
    {
        if (feedBar == null)
        {
            Debug.Log("# Fatal Error :: FeedStatus.cs -> feedScale is null. GameObject -> " + gameObject.name);
        }
    }

    private float ConvertRange(float valueToConvert, float sourceRangeStart, float sourceRangeEnd, float targetRangeStart, float targetRangeEnd)
    {
        // A linear interpolation formula is used to convert a value from one range to another
        return ((valueToConvert - sourceRangeStart) / (sourceRangeEnd - sourceRangeStart)) * (targetRangeEnd - targetRangeStart) + targetRangeStart;
    }

    #endregion


    // --------------------------------------------------------------------------------------------------------------
    // Public Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public void UpdateFeedBarStatus(int maxFeed, int actualFeed)
    {
        float targetBarScale = ConvertRange(actualFeed, minFeed, maxFeed, minScale, maxScale);

        if (feedBar != null)
        {
            feedBar.localScale = new Vector3(feedBar.localScale.x, feedBar.localScale.y, targetBarScale);
        }
    }

    #endregion
}
