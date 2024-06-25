using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters
    // --------------------------------------------------------------------------------------------------------------

    #region Parameters

    [Header("Main Settings")]
    [Space]

    public static ObjectPooler SharedInstance;
    [SerializeField] List<GameObject> prefabsToPool;
    [SerializeField] int amountToPool;
    int poolObjectsTypes;

    [Space]
    [Header("Pooled Info")]
    [Space]

    List<GameObject>[] objectsPool;

    #endregion


    // --------------------------------------------------------------------------------------------------------------
    // Private Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Private Methods

    private void Awake()
    {
        SharedInstance = this;
        poolObjectsTypes = prefabsToPool.Count;
    }

    private void Start()
    {
        ConfigurePool();
    }

    private void ConfigurePool()
    {
        InitPool();
        FillPool();
    }

    private void FillPool()
    {
        for (int currentType = 0; currentType < poolObjectsTypes; ++currentType)
        {
            for (int currentIndex = 0; currentIndex < amountToPool; ++currentIndex)
            {
                AddObjectOfType(currentType);
            }
        }
    }

    private void AddObjectOfType(int type)
    {
        GameObject foodProjectile = Instantiate(prefabsToPool[type], transform.position,Quaternion.identity);
        foodProjectile.SetActive(false);

        objectsPool[type].Add(foodProjectile);
        foodProjectile.transform.SetParent(transform);
    }

    private void InitPool()
    {
        objectsPool = new List<GameObject>[prefabsToPool.Count];

        for (int listIndex = 0; listIndex < objectsPool.Length; ++listIndex)
        {
            objectsPool[listIndex] = new List<GameObject>();
        }
    }

    #endregion


    // --------------------------------------------------------------------------------------------------------------
    // Public Methods
    // --------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public GameObject GetPooledObject(int objectType = 0)
    {
        for(int objectIndex = 0; objectIndex < objectsPool[objectType].Count; ++objectIndex)
        {
            GameObject currentObject = objectsPool[objectType][objectIndex];

            if (!currentObject.activeInHierarchy)
            {
                return currentObject;
            }
        }

        // otherwise, return null   
        return null;
    }

    #endregion
}
