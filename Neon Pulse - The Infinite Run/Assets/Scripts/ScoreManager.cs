using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int points = 0;

    public int pointsPerSecond = 1; // Punts per segon
    
    private float timeCounter = 0f;
    private int coinsCollected = 0; // Monedes recollides

    public delegate void PointsChanged(int newPoints);
    public static event PointsChanged OnPointsChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // Incrementa punts per temps
        timeCounter += Time.deltaTime;
        if (timeCounter >= 1f)
        {
            timeCounter -= 1f;
            AddPoints(pointsPerSecond);
        }
    }

    public void AddPoints(int extraPoints)
    {
        points += extraPoints;
        OnPointsChanged?.Invoke(points);
    }

    public void AddCoin(int amount)
    {
        coinsCollected += amount;

        AddPoints(amount); 
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetCoins()
    {
        return coinsCollected;
    }
}
