using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string playerTag = "Player";
        GameObject player = GameObject.FindWithTag(playerTag);

        if (player != null)
        {
            player.GetComponent<PlayerController>().AddScore();
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
