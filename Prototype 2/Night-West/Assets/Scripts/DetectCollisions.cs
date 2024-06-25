using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Animal animal = other.gameObject.GetComponent<Animal>();

        if (animal != null)
        {
            animal.Feed();
        }

        gameObject.SetActive(false);
    }
}
