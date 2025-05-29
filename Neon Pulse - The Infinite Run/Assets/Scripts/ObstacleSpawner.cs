using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array de prefabs d'obstacles per generar
    public float spawnInterval = 2f;     // Interval de temps entre cada generació d'obstacles
    public float spawnRangeX = 5f;       // Rangs d'esquerra i dreta per col·locar obstacles

    private float timer = 0f;            // Temporitzador intern per controlar l'interval de generació

    public float difficultyIncreaseRate = 0.05f; // Quant augmenta la freqüència d'obstacles
    private float timeSurvived = 0f;   // Temps de supervivència
    private float nextSpawnTime;

    public DifficultyManager difficultyManager; // Referència al DifficultyManager


    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;  // Assegurem-nos que la generació comenci al principi

        difficultyManager = FindObjectOfType<DifficultyManager>(); // Troba el DifficultyManager a l'escena

        if (difficultyManager != null)
        {
            Debug.Log("DifficultyManager trobat!");
        }
        else
        {
            Debug.LogError("No s'ha trobat el DifficultyManager a l'escena!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Actualitza el temporitzador
        timer += Time.deltaTime;

        // Guardem l'interval anterior per comprovar el canvi
        float oldSpawnInterval = spawnInterval;

        // Augmentem la dificultat amb el temps (reduint el spawnInterval)
        spawnInterval = Mathf.Max(0.5f, spawnInterval - difficultyIncreaseRate * Time.deltaTime);

        // Comprova si el temps ha superat l'interval de generació
        if (timer >= spawnInterval)
        {
            SpawnObstacle(); // Genera un obstacle
            timer = 0f;      // Reinicia el temporitzador
        }

        // Si el spawnInterval ha canviat, mostra el nou valor
       /** (Mathf.Abs(spawnInterval - oldSpawnInterval) > 0.01f) // 0.01f és el llindar de canvi
        {
            Debug.Log($"[Dificultat] Nova dificultat: spawnInterval = {spawnInterval}");
        }*/
    }


    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;  // Actualitza el valor de spawnInterval
    }

    void SpawnObstacle()
    {
        // Selecciona un prefab d'obstacle aleatoriament
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = obstaclePrefabs[obstacleIndex];

        // Determina la posició de generació dins del rang
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(spawnPosX, transform.position.y, transform.position.z);

        // Genera l'obstacle en la posició especificada
        Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }
}
