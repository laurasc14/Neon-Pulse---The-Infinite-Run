using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPoints : MonoBehaviour
{
    public TMP_Text text;

    private int currentPoints = -1; // Valor inicial diferent de qualsevol puntuació possible

    void Start()
    {
        // Inicialitza el text amb la puntuació actual
        UpdatePointsText(ScoreManager.instance.GetPoints());
    }

    void Update()
    {
        // Només actualitza el text si la puntuació ha canviat
        int newPoints = ScoreManager.instance.GetPoints();
        if (newPoints != currentPoints)
        {
            UpdatePointsText(newPoints);
        }
    }

    private void UpdatePointsText(int points)
    {
        currentPoints = points;
        text.text = $"{points}"; 
    }
}
