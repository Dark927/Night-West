using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region parameters 

    [SerializeField] GameObject[] animalsPrefabs;

    [SerializeField] float spawnStartZ = 20f;
    [SerializeField] float spawnSpreadX = 15f;

    Vector3 spawnPosition = Vector3.zero;

    float spawnStartDelay = 2f;
    float spawnInterval = 2f;

    // Functions names for Invoke

    string spawnRandomAnimal = "SpawnRandomAnimal";

    #endregion


    private void Start()
    {
        InvokeRepeating(spawnRandomAnimal, spawnStartDelay, spawnInterval);
    }

    private void SpawnRandomAnimal()
    {
        spawnPosition = new Vector3(Random.Range(-spawnSpreadX, spawnSpreadX), 0, spawnStartZ);

        int animalIndex = Random.Range(0, animalsPrefabs.Length);
        GameObject spawnAnimal = animalsPrefabs[animalIndex];

        Instantiate(spawnAnimal, spawnPosition, spawnAnimal.transform.rotation);
    }
}
