using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty selectedDifficulty;

    public void SetDifficulty(Difficulty difficulty)
    {
        selectedDifficulty = difficulty;
        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                // Ajusta els valors per a la dificultat fàcil
                break;
            case Difficulty.Medium:
                // Ajusta els valors per a la dificultat mitjana
                break;
            case Difficulty.Hard:
                // Ajusta els valors per a la dificultat difícil
                break;
        }
    }
}
