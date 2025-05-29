using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUpChance
{
    public GameObject powerUpPrefab;
    [Range(0f, 1f)]
    public float probability;
}

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Configuració de spawn")]
    [Range(0f, 1f)]
    public float spawnChance = 0.5f; // 50% de probabilitat d'intentar fer spawn

    public List<PowerUpChance> powerUpsDisponibles;

    void Start()
    {
        TrySpawnPowerUp();
    }

    void TrySpawnPowerUp()
    {
        float roll = Random.value; // 0.0 - 1.0

        if (roll > spawnChance)
        {
            return; // No fem spawn
            Debug.Log("No spawn powerup");
        } 

        GameObject seleccionat = GetPowerUpPerProbabilitat();

        if (seleccionat != null)
        {
            GameObject geo = Instantiate(seleccionat, transform.position, Quaternion.identity);
            geo.transform.parent = this.transform;

            Debug.Log("Intentant fer spawn d'un power-up...");

        }
        
    }

    GameObject GetPowerUpPerProbabilitat()
    {
        float total = 0;
        foreach (var entry in powerUpsDisponibles)
        {
            total += entry.probability;
        }

        float rand = Random.value * total;
        float acumulat = 0;

        foreach (var entry in powerUpsDisponibles)
        {
            acumulat += entry.probability;
            if (rand <= acumulat)
            {
                return entry.powerUpPrefab;
            }
        }

        return null; // Si no n’hi ha cap
    }
}
