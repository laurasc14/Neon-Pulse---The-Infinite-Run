using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ObjectPooler : MonoBehaviour
{
    [Header("Pool Settings")]
    public GameObject prefab;
    public int initialPoolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (prefab == null)
        {
            Debug.LogError("[ObjectPooler] Prefab no assignat.");
            return;
        }

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab);

        /**
#if UNITY_EDITOR
        if (PrefabUtility.IsPartOfPrefabAsset(obj))
        {
            Debug.LogError("[ObjectPooler] ERROR: Estàs intentant instanciar un prefab original!");
            return null;
        }
#endif
        */

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.transform.SetParent(parent, false); // false = no manté transformació antiga
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform, false); // per mantenir-ho organitzat
        pool.Enqueue(obj);
    }
}