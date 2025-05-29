using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // Prefab a gestionar
    public int poolSize = 20; // Nombre inicial d'objectes al pool
    private Queue<GameObject> pool; // Cua per gestionar el pool

    void Start()
    {
        pool = new Queue<GameObject>();

        // Inicialitza el pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
            //Debug.Log($"[ObjectPool] Object {obj.name} created and added to pool. Pool size: {pool.Count}");
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            //Debug.Log($"[ObjectPool] Object retrieved: {obj.name}. Remaining in pool: {pool.Count}");
            return obj;
        }
        else
        {
            //Debug.LogWarning("[ObjectPool] Pool is empty! Creating a new object.");
            GameObject obj = Instantiate(prefab, position, rotation);

            // Assigna el pool al nou objecte
            Vehicle vehicleScript = obj.GetComponent<Vehicle>();
            if (vehicleScript != null)
            {
                vehicleScript.SetPool(this);
                //Debug.Log($"[ObjectPool] Pool assigned to new object: {obj.name}");
            }
            else
            {
                //Debug.LogError($"[ObjectPool] New object {obj.name} does not have a Vehicle script!");
            }

            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false); // Desactiva l'objecte
        pool.Enqueue(obj); // Torna al pool
        //Debug.Log($"[ObjectPool] Object returned: {obj.name}. Pool size: {pool.Count}");
    }
}

