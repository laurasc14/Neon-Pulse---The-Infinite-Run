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
    public float distance;
    private float speed;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        lastZGenerated = player.position.z;

        // Inicialización
        enviromentGenerator.CreateRoad();
        decorationGenerator.CreateDecoration();

        // Enlazar referencia para sincronizar distancia
        decorationGenerator.worldGenerator = this;

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
        }
        decorationGenerator.CreateDecoration();
    }

    void GenerateWorldStep()
    {
        enviromentGenerator.CreateRoad();

        lastZGenerated += enviromentGenerator.roadLength;
    }
}