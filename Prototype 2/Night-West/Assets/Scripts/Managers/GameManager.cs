using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum UpdateUI
{
    Update_InvalidFirst = 0,

    Update_Heal = 1,
    Update_Damage = 2,


    Update_Last
}


public class GameManager : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters 
    // --------------------------------------------------------------------------------------------------------------

    #region Parameters 

    [Header("Settings")]
    [SerializeField] PlayerController player;

    [Space]
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreValueUI;

    int actualScore = 0;
    int startScoreToHeal = 50;
    int scoreToHeal = 50;
    int healForScore = 1;

    [Space]
    [Header("HP")]
    [SerializeField] GameObject heartsContainerUI;
    [SerializeField] GameObject hpHeartPrefab;

    List<GameObject> hearts;

    bool isGameStopped = false;

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods

    private void Start()
    {
        // Score check

        if (scoreValueUI == null)
        {
            Debug.Log($"# Error : GameManager.cs, Object -> {gameObject.name}, scoreValueUI is null!");
        }


        // HP 

        hearts = new List<GameObject>();
        float heartDistance = 120f;

        for (int i = 0; i < player.BasicHP; ++i)
        {
            GameObject createHeart = Instantiate(hpHeartPrefab, heartsContainerUI.transform);
            createHeart.GetComponent<RectTransform>().localPosition = new Vector3(heartDistance * i, 0f, 0f);
            createHeart.SetActive(true);

            hearts.Add(createHeart);
        }
    }

    private void Update()
    {
        // Check if player is dead

        if (player.IsGameOver() && !isGameStopped)
        {
            StopGameplay();
        }


        // Heal for score

        bool isHeal = actualScore >= scoreToHeal;

        if (isHeal)
        {
            HealPlayer(healForScore);
            scoreToHeal += startScoreToHeal;
        }
    }

    private void StopGameplay()
    {
        // Stop spawning animals

        FindAnyObjectByType<SpawnManager>().StopSpawning();


        // Stop all animals from running

        Animal[] activeAnimals = FindObjectsOfType<Animal>();

        foreach (Animal animal in activeAnimals)
        {
            animal.IdleState();
            Debug.Log("done");
        }

        isGameStopped = true;
    }

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Public Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public void AddScore(int amount = 1)
    {
        actualScore += amount;
        scoreValueUI.text = actualScore.ToString();
    }

    public void HitPlayer(int damage = 1)
    {
        player.TakeDamage(damage);
        UpdateHealthBar(UpdateUI.Update_Damage);
    }

    public void HealPlayer(int amount = 1)
    {
        player.Heal(amount);
        UpdateHealthBar(UpdateUI.Update_Heal);
    }


    public void UpdateHealthBar(UpdateUI UpdateType)
    {

        if (UpdateType == UpdateUI.Update_Damage)
        {
            int actualHeartIndex = player.ActualHP - 1;

            for (int i = 0; i < hearts.Count; ++i)
            {
                // If player take damage, remove one heart 

                if ((UpdateType == UpdateUI.Update_Damage) && (i > actualHeartIndex))
                {
                    hearts[i].gameObject.SetActive(false);
                }


                // If player get heal, add one heart 

                else if ((UpdateType == UpdateUI.Update_Heal) && (i <= actualHeartIndex))
                {
                    hearts[i].gameObject.SetActive(true);
                }
            }
        }
    }

    #endregion
}
