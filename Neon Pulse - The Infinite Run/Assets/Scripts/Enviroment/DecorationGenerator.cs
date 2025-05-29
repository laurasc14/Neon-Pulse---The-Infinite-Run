using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
    [Header("Decoration Configuration")]
    public GameObject decorationParent;
    private float currentZPosition = -30f;

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

    private Queue<GameObject> leftPool = new Queue<GameObject>();
    private Queue<GameObject> rightPool = new Queue<GameObject>();

    private List<GameObject> activeDecoration;
    private GameObject lastDecorationGeneratedLeft;
    private GameObject lastDecorationGeneratedRight;

    private GameObject lastLeftElement;
    private GameObject lastRightElement;

    public int poolSize = 30;
    public float roadLength = 27.5f;

    private int contadorEdificios = 30;

    void Awake()
    {
        activeDecoration = new List<GameObject>();
        PreloadPools();

        GameObject left = GetFromPool(leftPool, leftSidewalkPrefabs);
        left.transform.position = new Vector3(-leftSidewalkOffsetX, 0, -30);
        left.SetActive(true);
        lastDecorationGeneratedLeft = left;

        GameObject right = GetFromPool(rightPool, rightSidewalkPrefabs);
        right.transform.position = new Vector3(rightSidewalkOffsetX, 0, -30);
        right.SetActive(true);
        lastDecorationGeneratedRight = right;

        GameObject leftElement = Instantiate(GetRandomPrefab(leftSideElements), new Vector3(-leftSideElementOffsetX, sideElementOffsetY, -30), Quaternion.identity, decorationParent.transform);
        lastLeftElement = leftElement;
        activeDecoration.Add(leftElement);

        GameObject rightElement = Instantiate(GetRandomPrefab(rightSideElements), new Vector3(rightSideElementOffsetX, sideElementOffsetY, -30), Quaternion.identity, decorationParent.transform);
        lastRightElement = rightElement;
        activeDecoration.Add(rightElement);

        for (int i = 0; i < 30; ++i)
        {
            //GenerateRoadSection();
        }
    }

   /** void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        if (player.transform.position.z + 30f > currentZPosition)
        {
            CreateDecoration();
        }
    }*/

    public void CreateDecoration()
    {
        GenerateRoadSection();
    }

    private void GenerateRoadSection()
    {
        GenerateSidewalks();
        GenerateSidewalks();
        GenerateSideElements();
    }

    private void GenerateSidewalks()
    {
        GameObject left = GetFromPool(leftPool, leftSidewalkPrefabs);
        if (left == null || lastDecorationGeneratedLeft == null) return;

        left.transform.position = new Vector3(-leftSidewalkOffsetX, 0, lastDecorationGeneratedLeft.transform.position.z + 5);
        left.transform.rotation = Quaternion.identity * Quaternion.AngleAxis(90, Vector3.up);
        left.SetActive(true);
        lastDecorationGeneratedLeft = left;
        activeDecoration.Add(left);

        GameObject right = GetFromPool(rightPool, rightSidewalkPrefabs);
        if (right == null || lastDecorationGeneratedRight == null) return;

        right.transform.position = new Vector3(rightSidewalkOffsetX, 0, lastDecorationGeneratedRight.transform.position.z + 5);
        right.transform.rotation = Quaternion.Euler(0, 180, 0);
        right.SetActive(true);
        lastDecorationGeneratedRight = right;
        activeDecoration.Add(right);
    }

    private void GenerateSideElements()
    {
        if(contadorEdificios >= 30)
        {
            if (leftSideElements.Count > 0)
            {
                GameObject left = Instantiate(GetRandomPrefab(leftSideElements),
                    new Vector3(-leftSideElementOffsetX, sideElementOffsetY, lastLeftElement.transform.position.z + 200f),
                    Quaternion.identity,
                    decorationParent.transform);
                lastLeftElement = left;
                activeDecoration.Add(left);
            }

            if (rightSideElements.Count > 0)
            {
                GameObject right = Instantiate(GetRandomPrefab(rightSideElements),
                    new Vector3(rightSideElementOffsetX, sideElementOffsetY, lastRightElement.transform.position.z + 200f),
                    Quaternion.identity,
                    decorationParent.transform);
                lastRightElement = right;
                activeDecoration.Add(right);
            }
            contadorEdificios -= 30;
        }
        else
        {
            contadorEdificios++;
        }
    }

    GameObject GetFromPool(Queue<GameObject> pool, List<GameObject> prefabs)
    {
        if (pool.Count > 0)
            return pool.Dequeue();

        GameObject prefab = GetRandomPrefab(prefabs);
        if (prefab == null /**|| decorationParent == null*/)
            return null;

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
            GameObject left = Instantiate(GetRandomPrefab(leftSidewalkPrefabs), Vector3.zero, Quaternion.identity * Quaternion.AngleAxis(90, Vector3.up), decorationParent.transform);
            left.SetActive(false);
            leftPool.Enqueue(left);

            GameObject right = Instantiate(GetRandomPrefab(rightSidewalkPrefabs), Vector3.zero, Quaternion.identity, decorationParent.transform);
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