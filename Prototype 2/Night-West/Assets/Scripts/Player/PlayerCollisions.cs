using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        GameManager gameManager = FindAnyObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.HitPlayer(1);
        }
    }
}
