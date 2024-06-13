using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region parameters 

    [SerializeField] GameObject[] animalsPrefabs;

    // Top spawn parameters

    [SerializeField] float spawnStartZ = 20f;
    [SerializeField] float spawnSpreadX = 15f;

    // Side spawn parameters 
    
    [SerializeField] float spawnStartX = 21f;
    [SerializeField] float minPosZ = 3f;
    [SerializeField] float maxPosZ = 13f;

    float spawnStartDelay = 2f;
    float spawnInterval = 3f;

    // Functions names for Invoke

    string spawnRandomAnimalTop = "SpawnRandomAnimalTop";
    string spawnRandomAnimalSide = "SpawnRandomAnimalSide";

    #endregion


    private void Start()
    {
        InvokeRepeating(spawnRandomAnimalTop, spawnStartDelay, spawnInterval);
        InvokeRepeating(spawnRandomAnimalSide, spawnStartDelay, spawnInterval);
    }

    private void SpawnRandomAnimalTop()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnSpreadX, spawnSpreadX), 0, spawnStartZ);
        SpawnRandomAnimal(spawnPosition, 180f);
    }

    private void SpawnRandomAnimalSide()
    {
        int randomChoice = Random.Range(0, 2);

        float startPositionX = (randomChoice == 0) ? -spawnStartX : spawnStartX;
        float rotationY = (randomChoice == 0) ? 90f : -90f;
        Vector3 spawnPosition = new Vector3(startPositionX, 0f, Random.Range(minPosZ, maxPosZ));

        SpawnRandomAnimal(spawnPosition, rotationY);
    }

    private void SpawnRandomAnimal(Vector3 spawnPosition, float rotationY = 0)
    {
        int animalIndex = Random.Range(0, animalsPrefabs.Length);
        GameObject spawnAnimal = animalsPrefabs[animalIndex];

        Quaternion animalRotation;

        if ((int)rotationY != 0)
        {
            animalRotation = Quaternion.Euler(spawnAnimal.transform.rotation.x, rotationY, spawnAnimal.transform.rotation.z);
        }
        else
        {
            animalRotation = spawnAnimal.transform.rotation;
        }

        Instantiate(spawnAnimal, spawnPosition, animalRotation);
    }
}
