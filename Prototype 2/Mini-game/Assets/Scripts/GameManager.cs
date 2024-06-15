using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreValueUI;
    int actualScore = 0;

    private void Start()
    {
        if(scoreValueUI == null)
        {
            Debug.Log($"# Error : GameManager.cs, Object -> {gameObject.name}, scoreValueUI is null!");
        }
    }

    private void Update()
    {
        
    }

    public void AddScore(int amount = 1)
    {
        actualScore += amount;
        scoreValueUI.text = actualScore.ToString();
    }
}
