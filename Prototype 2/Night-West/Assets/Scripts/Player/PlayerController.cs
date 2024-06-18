using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters
    // --------------------------------------------------------------------------------------------------------------

    #region parameters

    // ----------------------------------------------------

    [Space]
    [Header("Player Stats")]

    [SerializeField] float basicSpeed = 10f;
    float actualSpeed = 0f;

    [SerializeField] int basicHp = 5;
    int actualHp = 0;

    // ----------------------------------------------------

    [Space]
    [Header("Borders Setting")]

    [SerializeField] int maxBorderRangeX = 15;

    [SerializeField] int maxBorderDownZ = 0;
    [SerializeField] int maxBorderUpZ = 5;

    // ----------------------------------------------------

    [Space]
    [Header("Food projectile Setting")]

    [SerializeField] List<GameObject> projectileFoodPrefabs;
    [SerializeField] Transform projectileSpawnPoint;


    // ----------------------------------------------------

    Animator animator;

    bool gameOver = false;
    bool isFatalError = false;

    // Functions names for invoke

    string startGame = "StartGame";

    #endregion


    // --------------------------------------------------------------------------------------------------------------
    // Private methods 
    // --------------------------------------------------------------------------------------------------------------

    #region Private methods 

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isFatalError = (projectileFoodPrefabs.Count == 0) || (projectileSpawnPoint == null);

        if (!isFatalError)
        {
            Invoke(startGame, 1);
        }
        else
        {
            Debug.Log("# Fatal Error :: Game can't be started. Some references are null [ PlayerController.cs ]");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Errors check 

        if(isFatalError)
        {
            return;
        }

        // Main mechanics 

        if (!gameOver)
        {
            // -------------------------------------------------------------------------
            // Move character though player input ( up/down, left/right )
            // -------------------------------------------------------------------------

            PlayerMove();

            // -------------------------------------------------------------------------
            // Launch food projectile ( forward ) by pressing key
            // -------------------------------------------------------------------------

            if (Input.GetKeyDown(KeyCode.Space))
            {
                int randomIndex = Random.Range(0, projectileFoodPrefabs.Count);

                Instantiate(projectileFoodPrefabs[randomIndex], projectileSpawnPoint.position, projectileFoodPrefabs[randomIndex].transform.rotation);
            }
        }
    }

    private void PlayerMove()
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

        if(direction != Vector3.zero)
        {
            animator.SetFloat("Speed_f", 0.3f);
        }
        else
        {
            animator.SetFloat("Speed_f", 0.1f);
        }

        transform.Translate(direction * Time.deltaTime * actualSpeed);
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
        gameOver = false;

        actualSpeed = basicSpeed;
        actualHp = basicHp;
    }

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Public Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Public Methods 

    // -------------------------------------------------------
    // Getters and Setters
    // -------------------------------------------------------

    #region Getters and Setters

    public int BasicHP
    {
        get { return basicHp; }
        private set { BasicHP = value; }
    }

    public int ActualHP
    {
        get { return actualHp; }
        private set { actualHp = value; }
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    #endregion
    
    // -------------------------------------------------------

    public void TakeDamage(int damage = 1)
    {
        actualHp = ((actualHp - damage) >= 0) ? actualHp - damage : 0;

        if (actualHp == 0)
        {
            gameOver = true;

            animator.SetBool("Death_b", true);
            Debug.Log("GAME OVER!");
        }
    }


    public void Heal(int amount = 1)
    {
        if (actualHp < basicHp)
        {
            actualHp += amount;

            if (actualHp > basicHp)
            {
                actualHp = basicHp;
            }
        }
    }

    #endregion
}
