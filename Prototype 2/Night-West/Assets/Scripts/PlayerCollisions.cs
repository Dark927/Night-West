using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollisions : MonoBehaviour
{
    PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        player = GetComponent<PlayerController>();

        player.TakeDamage();
        Destroy(other.gameObject);
    }
}
