using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region parameters

    [SerializeField] float basicSpeed = 10f;
    float actualSpeed = 0f;

    [SerializeField] int maxBorderRangeX = 15;

    [SerializeField] int maxBorderDownZ = 0;
    [SerializeField] int maxBorderUpZ = 5;

    [SerializeField] GameObject projectileFoodPrefab;


    // Functions names for invoke

    string startGame = "StartGame";

    #endregion

    private void Start()
    {
        Invoke(startGame, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // -------------------------------------------------------------------------
        // Checking the boundaries so that the character cannot go beyond them
        // -------------------------------------------------------------------------

        Vector3 playerPosition = transform.position;

        // Check X borders

        int offsetX = (playerPosition.x < (-maxBorderRangeX)) ? int.MinValue :
            (playerPosition.x > maxBorderRangeX) ? int.MaxValue : 0;


        // Check Z borders

        int offsetZ = (playerPosition.z < maxBorderDownZ) ? int.MinValue :
            (playerPosition.z > maxBorderUpZ) ? int.MaxValue : 0;


        // Set Block for movement

        Vector2 blockHorizontal = new Vector2(0, 0);
        Vector2 blockForward = new Vector2(0, 0);

        if (offsetX != 0 || offsetZ != 0)
        {
            blockHorizontal = CheckOffsetBorder(offsetX);
            blockForward = CheckOffsetBorder(offsetZ);
        }

        // -------------------------------------------------------------------------
        // Player movement through user input ( left - right )
        // -------------------------------------------------------------------------

        // Get player input

        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        // Check if movement direction is blocked

        if ((blockHorizontal != Vector2.zero) || (blockForward != Vector2.zero))
        {
            if (CheckBlockedInput(blockHorizontal, horizontalInput))
            {
                horizontalInput = 0f;
            }

            if (CheckBlockedInput(blockForward, forwardInput))
            {
                forwardInput = 0f;
            }

        }

        // Saving movement direction

        Vector3 direction = new Vector3(horizontalInput, 0, forwardInput);

        // For fixing diagonal movement speed

        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        // Move player 

        transform.Translate(direction * Time.deltaTime * actualSpeed);


        // -------------------------------------------------------------------------
        // Launch food projectile ( forward ) by pressing key
        // -------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectileFoodPrefab, transform.position, projectileFoodPrefab.transform.rotation);
        }
    }


    /// <summary>
    /// Check if player input must be blocked
    /// </summary>
    /// <param name="blockAxis">Axis to check</param>
    /// <param name="AxisInput">Input to check</param>
    /// <returns>True - input must be blocked, False - input is correct</returns>
    private bool CheckBlockedInput(Vector2 blockAxis, float AxisInput)
    {
        // Left/Bottom
        bool isBlockFirst = (AxisInput < 0) && (blockAxis[0] != 0);

        // Right/Up
        bool isBlockSecond = (AxisInput > 0) && (blockAxis[1] != 0);


        if (isBlockFirst || isBlockSecond)
        {
            return true; ;
        }

        return false;
    }

    /// <summary>
    /// Check offset borders
    /// </summary>
    /// <param name="offset">Movement offset from border</param>
    /// <returns>Vector2: [0] -> is blocking left/bottom, [1] -> is blocking right/up</returns>
    private Vector2 CheckOffsetBorder(int offset)
    {
        Vector2 blockVector = new Vector2(0, 0);

        if (offset < 0)
        {
            blockVector[0] = 1f;
        }

        if (offset > 0)
        {
            blockVector[1] = 1f;
        }

        return blockVector;
    }

    private void StartGame()
    {
        actualSpeed = basicSpeed;
    }
}
