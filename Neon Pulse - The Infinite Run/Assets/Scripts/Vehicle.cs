using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Vehicle : MonoBehaviour
{
    private ObjectPool pool;

    // Assigna la referència al pool
    public void SetPool(ObjectPool objectPool)
    {
        pool = objectPool;
    }

    void Update()
    {

        if (pool == null)
        {
            //Debug.LogWarning($"[Vehicle] Vehicle {gameObject.name} has no pool assigned!");
            return; // Evita cridar ReturnToPool si el pool és null
        }

        //Debug.Log($"Vehicle {gameObject.name} at position {transform.position.z}"); // Depuració
        // Comprova si el vehicle ha sortit de l'àrea visible
        if (transform.position.z < -10f) 
        {
            pool.ReturnToPool(this.gameObject); // Torna’l al pool
        }
    }
}
