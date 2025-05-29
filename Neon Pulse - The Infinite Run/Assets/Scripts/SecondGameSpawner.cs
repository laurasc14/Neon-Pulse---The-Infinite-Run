using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGameSpawner : MonoBehaviour
{
    public float spacing = 55f; // Cuántos metros debe avanzar el último obstáculo
    public List<GameObject> easy_presets;
    public List<GameObject> medium_presets;
    public List<GameObject> dificult_presets;

    private GameObject lastSpawned;
    private float lastSpawnInitialZ;

    enum Difficulty { Easy, Medium, Hard }

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Spawn();
        }
    }

    void Update()
    {
        if (lastSpawned == null) return;

        float distanceMoved = lastSpawnInitialZ - lastSpawned.transform.position.z;

        if (distanceMoved >= spacing)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        float zPosition = lastSpawned != null
            ? lastSpawned.transform.position.z + spacing
            : 45f;

        GameObject toSpawn = GetPresetByDifficulty(GetDifficulty());
        GameObject spawned = Instantiate(toSpawn, new Vector3(3.75f, 0, zPosition), Quaternion.Euler(0, -90, 0));

        lastSpawned = spawned;
        lastSpawnInitialZ = zPosition;
    }

    Difficulty GetDifficulty()
    {
        int score = ScoreManager.instance != null ? ScoreManager.instance.GetPoints() : 0;

        if (score < 100) return Difficulty.Easy;
        if (score < 250) return Difficulty.Medium;
        return Difficulty.Hard;
    }

    GameObject GetPresetByDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Medium:
                return medium_presets[Random.Range(0, medium_presets.Count)];
            case Difficulty.Hard:
                return dificult_presets[Random.Range(0, dificult_presets.Count)];
            default:
                return easy_presets[Random.Range(0, easy_presets.Count)];
        }
    }
}