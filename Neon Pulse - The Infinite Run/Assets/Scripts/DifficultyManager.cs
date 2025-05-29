using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public float easySpeed = 5f;
    public float mediumSpeed = 7f;
    public float hardSpeed = 10f;
    public float easyObstacleFrequency = 2f;
    public float mediumObstacleFrequency = 1.5f;
    public float hardObstacleFrequency = 1f;

    //private ObstacleSpawner obstacleSpawner;  // Referència al component ObstacleSpawner

    public float spawnInterval = 2f; // Interval inicial de spawn
    public float difficultyIncreaseRate = 0.05f; // Quant augmenta la dificultat per segon

    private float timeSurvived = 0f;

    void Start()
    {
        // Associa el component ObstacleSpawner
        //obstacleSpawner = FindObjectOfType<ObstacleSpawner>();

        // Asegura't que obstacleSpawner no és nul
        /**if (obstacleSpawner == null)
        {
            Debug.LogError("ObstacleSpawner no trobat! Assegura't que tens aquest component a l'escena.");
            return;
        }*/

        // Si obstacleSpawner no és nul, continua amb la configuració
        UpdateDifficultySettings();
    }

    /**
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assigna l'instància
            DontDestroyOnLoad(gameObject); // Opcional, per no destruir aquest script entre escenes
        }
        else
        {
            Destroy(gameObject); // Elimina la instància duplicada
        }
    }*/
    void Update()
    {
        UpdateDifficultySettings();
    }

    void UpdateDifficultySettings()
    {

        void UpdateDifficultySettings()
        {
            Debug.Log("[Dificultat] Actualitzant configuració de dificultat...");
            spawnInterval = Mathf.Max(0.5f, spawnInterval - difficultyIncreaseRate * Time.deltaTime);
            Debug.Log($"[Dificultat] spawnInterval actualitzat a: {spawnInterval}");
        }

    }
}
