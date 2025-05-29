using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{

    private float[] lanes = { -2.5f, 0f, 2.5f };
    private float timePlayed = 0;
    private float lastSpawnZ = 5;
    private float minSpawnDistance = 10f;

    public GameObject pickUp; //birra
    
    public List<GameObject> powerUpPrefabs; // Power-ups

    public List <SpawnData> objectList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in objectList)
        {
            data.nextSpawn = Time.time + data.velocitySpawn;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Genera els objectes configurats
        foreach (SpawnData data in objectList)
        {
            if (Time.time > data.nextSpawn)
            {
                Spawn(data);
                data.nextSpawn = Time.time + data.velocitySpawn * data.spawnCurve.Evaluate(timePlayed / 60);
            }
        }

        // Incrementa el temps jugat
        timePlayed += Time.deltaTime;

        // Opcional: Genera power-ups amb probabilitat fixa
        if (UnityEngine.Random.Range(0f, 1f) < 0.01f) // 1% probabilitat per frame
        {
            SpawnRandomPowerUp();
        }
    }

    void Spawn(SpawnData data)
    {
        float lane = lanes[UnityEngine.Random.Range(0, lanes.Length)];
        float spawnZ = lastSpawnZ + minSpawnDistance;

        Vector3 spawnPosition = new Vector3(
            lane,
            transform.position.y + 0.5f + data.heithSpawn,
            spawnZ
        );

        // Genera l'objecte del pool
        GameObject obj = data.pool.GetFromPool(spawnPosition, Quaternion.identity);

        // Assigna el pool si és necessari
        Vehicle vehicleScript = obj.GetComponent<Vehicle>();
        if (vehicleScript != null)
        {
            vehicleScript.SetPool(data.pool);
        }

        // Generació de monedes als altres carrils
        for (int i = 0; i < lanes.Length; i++)
        {
            if (lanes[i] != lane && UnityEngine.Random.Range(0f, 1f) < 0.3f) // Probabilitat 30%
            {
                Vector3 coinPosition = new Vector3(
                    lanes[i],
                    transform.position.y + data.heithSpawn,
                    spawnZ
                );
                Instantiate(pickUp, coinPosition, Quaternion.identity, obj.transform);
            }
        }

        lastSpawnZ = spawnZ;
    }

    void SpawnRandomPowerUp()
    {
        if (powerUpPrefabs.Count == 0) return;

        float lane = lanes[UnityEngine.Random.Range(0, lanes.Length)];
        float spawnZ = lastSpawnZ + minSpawnDistance;

        Vector3 spawnPosition = new Vector3(
            lane,
            transform.position.y + 0.5f,
            spawnZ
        );

        
       
        
            // Selecciona un power-up aleatoriament
            GameObject selectedPowerUp = powerUpPrefabs[UnityEngine.Random.Range(0, powerUpPrefabs.Count)];
            Instantiate(selectedPowerUp, spawnPosition, Quaternion.identity);
        

        
    }
    
}
[Serializable]
public class SpawnData
{
    public float probabilitySpawn;
    public ObjectPool pool;
    public float heithSpawn;
    public float velocitySpawn;
    internal float nextSpawn;
    public AnimationCurve spawnCurve;
    
}
