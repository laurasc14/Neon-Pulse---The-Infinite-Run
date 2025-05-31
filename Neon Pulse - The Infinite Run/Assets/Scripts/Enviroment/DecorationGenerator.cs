using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
    [Header("Decoration Configuration")]
    public GameObject decorationParent;

    [Header("Sidewalk Configuration")]
    public List<GameObject> leftSidewalkPrefabs;
    public List<GameObject> rightSidewalkPrefabs;
    public float leftSidewalkOffsetX = 5f;
    public float rightSidewalkOffsetX = 6f;

    [Header("Side Elements")]
    public List<GameObject> leftSideElements;
    public List<GameObject> rightSideElements;
    public float leftSideElementOffsetX = 15f;
    public float rightSideElementOffsetX = 6f;
    public float sideElementOffsetY = 0.2f;

    [Header("External Reference")]
    public WorldGenerator worldGenerator;

    private Queue<GameObject> leftPool = new Queue<GameObject>();
    private Queue<GameObject> rightPool = new Queue<GameObject>();
    private List<GameObject> activeDecoration;

    private GameObject lastLeftSidewalk;
    private GameObject lastRightSidewalk;
    private Vector3 initialLeftPos;
    private Vector3 initialRightPos;

    private GameObject lastLeftElement;
    private Vector3 initialLeftElementPos;
    private GameObject lastRightElement;
    private Vector3 initialRightElementPos;

    public int poolSize = 30;

    void Awake()
    {
        activeDecoration = new List<GameObject>();
        PreloadPools();

        // Sidewalks
        for (int i = 0; i < 10; i++)
            lastLeftSidewalk = GenerateLeftSidewalk();
        initialLeftPos = lastLeftSidewalk.transform.position;

        for (int i = 0; i < 20; i++)
            lastRightSidewalk = GenerateRightSidewalk();
        initialRightPos = lastRightSidewalk.transform.position;

        // Side Elements - inicial en -30, luego +200. Nuevo se convierte en el último.
        GenerateSideElementAt(true, -30f);

        GenerateSideElementAt(true, lastLeftElement.transform.position.z + 200f);

        GenerateSideElementAt(false, -30f);
        GenerateSideElementAt(false, lastRightElement.transform.position.z + 200f);

    }

    public void CreateDecoration()
    {
        // Sidewalks
        if (lastLeftSidewalk.transform.position.z < initialLeftPos.z)
            lastLeftSidewalk = GenerateLeftSidewalk();

        if (lastRightSidewalk.transform.position.z < initialRightPos.z)
            lastRightSidewalk = GenerateRightSidewalk();

        // Side Elements: Si el último ha recorrido 170 en Z negativa, generar otro a -200 más
        if (initialLeftElementPos.z - lastLeftElement.transform.position.z >= 190f)
        {
            GenerateSideElementAt(true, lastLeftElement.transform.localPosition.z + 200f);
        }

        if (initialRightElementPos.z - lastRightElement.transform.position.z >= 190f)
        {
            GenerateSideElementAt(false, lastRightElement.transform.position.z + 200f);
        }

        CleanupSideElements();
    }

    private GameObject GenerateLeftSidewalk()
    {
        GameObject left = GetFromPool(leftPool, leftSidewalkPrefabs);
        if (left == null) return null;

        float z = lastLeftSidewalk != null ? lastLeftSidewalk.transform.position.z + 19.5f : -30f;
        left.transform.position = new Vector3(-leftSidewalkOffsetX, 0, z);
        left.transform.rotation = Quaternion.Euler(0, 90, 0);
        left.SetActive(true);
        activeDecoration.Add(left);
        return left;
    }

    private GameObject GenerateRightSidewalk()
    {
        GameObject right = GetFromPool(rightPool, rightSidewalkPrefabs);
        if (right == null) return null;

        float z = lastRightSidewalk != null ? lastRightSidewalk.transform.position.z + 7.2f : -30f;
        right.transform.position = new Vector3(rightSidewalkOffsetX, 0, z);
        right.transform.rotation = Quaternion.Euler(0, 180, 0);
        right.SetActive(true);
        activeDecoration.Add(right);
        return right;
    }

    private GameObject GenerateSideElementAt(bool isLeft, float z)
    {
        List<GameObject> list = isLeft ? leftSideElements : rightSideElements;
        float offsetX = isLeft ? -leftSideElementOffsetX : rightSideElementOffsetX;

        if (list.Count == 0) return null;

        GameObject obj = Instantiate(
            GetRandomPrefab(list),
            new Vector3(offsetX, sideElementOffsetY, z),
            Quaternion.identity,
            decorationParent.transform
        );

        // Como el entorno se mueve hacia Z negativa, ajustamos:
        if (isLeft)
        {
            lastLeftElement = obj;
            initialLeftElementPos = obj.transform.position;
        }
        else
        {
            if (obj.transform.localPosition.z < 125 && lastRightElement != null)
            {

                Destroy(obj);
                return null;
            }
            lastRightElement = obj;
            initialRightElementPos = obj.transform.position;
        }

        activeDecoration.Add(obj);
        return obj;
    }


    private void CleanupSideElements()
    {
        for (int i = activeDecoration.Count - 1; i >= 0; i--)
        {
            GameObject deco = activeDecoration[i];
            if (deco == null) continue;

            float z = deco.transform.position.z;
            if (z <= -300f)
            {
                activeDecoration.RemoveAt(i);
                Destroy(deco);
            }
        }
    }

    GameObject GetFromPool(Queue<GameObject> pool, List<GameObject> prefabs)
    {
        if (pool.Count > 0)
            return pool.Dequeue();

        GameObject prefab = GetRandomPrefab(prefabs);
        if (prefab == null) return null;

        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, decorationParent.transform);
        obj.SetActive(false);
        return obj;
    }

    GameObject GetRandomPrefab(List<GameObject> prefabs)
    {
        if (prefabs == null || prefabs.Count == 0)
        {
            Debug.LogError("Prefab list is null or empty! Cannot instantiate.");
            return null;
        }

        return prefabs[Random.Range(0, prefabs.Count)];
    }

    void PreloadPools()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject left = Instantiate(GetRandomPrefab(leftSidewalkPrefabs), Vector3.zero, Quaternion.Euler(0, 90, 0), decorationParent.transform);
            left.SetActive(false);
            leftPool.Enqueue(left);

            GameObject right = Instantiate(GetRandomPrefab(rightSidewalkPrefabs), Vector3.zero, Quaternion.Euler(0, 180, 0), decorationParent.transform);
            right.SetActive(false);
            rightPool.Enqueue(right);
        }
    }

    public void RemoveDecoration(GameObject deco)
    {
        if (activeDecoration.Contains(deco))
        {
            activeDecoration.Remove(deco);
            deco.SetActive(false);

            bool isLeft = deco.transform.position.x < 0;
            if (isLeft)
                leftPool.Enqueue(deco);
            else
                rightPool.Enqueue(deco);
        }
    }
}
