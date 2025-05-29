using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentGenerator : MonoBehaviour
{
    [Header("Road Settings")]
    public ObjectPooler roadPool;
    public Transform roadParent;
    public float roadLength = 27.5f;   

    [Header("Inicialització")]
    public int inicialSections = 10;
    public float initialZ = -30f;

    private float currentZPosition;
    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        currentZPosition = initialZ;

        for (int i = 0; i < inicialSections; i++)
        {
            GenerateRoadSection();
        }
    }

    /**void Update()
    {
        if (activeRoads.Count < 40)
        {
            GenerateRoadSection();
        }
    }*/

    public void CreateRoad()
    {
        GenerateRoadSection();
    }

    private void GenerateRoadSection()
    {
        float z = activeRoads.Count == 0
            ? currentZPosition
            : activeRoads[^1].transform.position.z + roadLength;

        Vector3 position = new Vector3(0f, 0f, z); // Centrat

        GameObject road = roadPool.GetFromPool(position, Quaternion.identity, roadParent);

        road.transform.localPosition = new Vector3(-4f, 0f, z);         // alineació segura
        road.transform.localRotation = Quaternion.identity * Quaternion.AngleAxis(90, Vector3.up);
        //road.transform.localScale = Vector3.one;

        activeRoads.Add(road);
    }

    public void RemoveRoad(GameObject road)
    {
        if (activeRoads.Contains(road))
        {
            activeRoads.Remove(road);
            roadPool.ReturnToPool(road);
        }
    }
}
