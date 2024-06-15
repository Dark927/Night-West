using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Space]
    [Header("HP")]
    [SerializeField] GameObject heartsContainerUI;
    [SerializeField] GameObject hpHeartPrefab;

    List<GameObject> hearts;

    #endregion

    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods

    private void Start()
    {
        // Score check

        if(scoreValueUI == null)
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
        // Heal for score

        bool isHeal = actualScore >= scoreToHeal;

        if(isHeal)
        {
            Debug.Log("heal");
            ApplyHeal(1);
            scoreToHeal += startScoreToHeal;
        }
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

    public void ApplyDamage()
    {
        int actualHeartIndex = player.ActualHP - 1;

        for(int i = 0; i < hearts.Count; ++i)
        {
            if(i > actualHeartIndex)
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    public void ApplyHeal(int amount = 1)
    {
        player.Heal(amount);

        int actualHeartIndex = player.ActualHP - 1;

        for (int i = actualHeartIndex - amount; i < hearts.Count; ++i)
        {
            if (i <= actualHeartIndex)
            {
                hearts[i].gameObject.SetActive(true);
            }
        }
    }

    #endregion
}
