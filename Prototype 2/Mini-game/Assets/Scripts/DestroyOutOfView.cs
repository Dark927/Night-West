using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    #region parameters

    [SerializeField] float maxTopBound = 35f;
    [SerializeField] float maxBottomBound = -10f;

    #endregion

    // Update is called once per frame
    void Update()
    {
        if((transform.position.z > maxTopBound) || (transform.position.z < maxBottomBound))
        {
            if(transform.position.z < maxBottomBound)
            {
                Debug.Log("Game Over!");
            }

            Destroy(gameObject);
        }

    }
}
