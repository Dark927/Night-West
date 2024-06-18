using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FoodTypes
{
    Food_invalidFirst = 0,

    Food_apple = 1,
    Food_carrot = 2,
    Food_cookie = 3,
    Food_meat = 4,

    Food_last
}

public class Food : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------
    // Parameters
    // --------------------------------------------------------------------------------------------------------------

    #region parameters 

    [SerializeField] FoodTypes type;
    [SerializeField] float speed = 10f;

    #endregion


    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
