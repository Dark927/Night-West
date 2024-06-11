using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region parameters

    [SerializeField] float speed = 10f;
    [SerializeField] int maxBorderRange = 10;

    [SerializeField] GameObject projectileFoodPrefab;

    float horizontalInput = 0f;

    #endregion

    // Update is called once per frame
    void Update()
    {
        // -------------------------------------------------------------------------
        // Checking the boundaries so that the character cannot go beyond them
        // -------------------------------------------------------------------------

        Vector3 playerPosition = transform.position;
        int offset = 0;

        offset = (playerPosition.x < (-maxBorderRange)) ? -maxBorderRange : 
            (playerPosition.x > maxBorderRange) ? maxBorderRange : 0;
        
        if(offset != 0)
        {
            playerPosition.x = offset;
            transform.position = playerPosition;
        }


        // -------------------------------------------------------------------------
        // Player movement through user input ( left - right )
        // -------------------------------------------------------------------------

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);


        // -------------------------------------------------------------------------
        // Launch food projectile ( forward ) by pressing key
        // -------------------------------------------------------------------------

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectileFoodPrefab, transform.position, projectileFoodPrefab.transform.rotation);
        }

    }
}
