using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Components")]
    public EnviromentGenerator enviromentGenerator;
    public DecorationGenerator decorationGenerator;

    [Header("Player")]
    public Transform player;

    [Header("Generation Settings")]
    public float generationDistance = 55f;
    public float velocitatMaxima = 25f;

    private float lastZGenerated;
    private float speed;
    private float distance;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        lastZGenerated = player.position.z;

        // Generació inicial
        enviromentGenerator.CreateRoad();          // Primera carretera
        decorationGenerator.CreateDecoration();    // Primera vorera

        for (int i = 0; i < 5; i++)
        {
            GenerateWorldStep();
        }
    }

    void Update()
    {
        GameSpeedController.ScrollSpeed = Mathf.Min(GameSpeedController.ScrollSpeed + 0.05f * Time.deltaTime, velocitatMaxima);
        speed = GameSpeedController.ScrollSpeed;

        distance += speed * Time.deltaTime;

        if (distance + generationDistance > lastZGenerated)
        {
            GenerateWorldStep();
            Debug.Log("generating road");
        }
    }

    void GenerateWorldStep()
    {
        enviromentGenerator.CreateRoad();
        decorationGenerator.CreateDecoration();

        lastZGenerated += enviromentGenerator.roadLength;
    }
}
