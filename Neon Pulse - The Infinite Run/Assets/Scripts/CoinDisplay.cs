using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public TMP_Text text; // Assigna el text des de l'Inspector

    private int currentCoins = 0;


    void Start()
    {
        UpdateCoinText(ScoreManager.instance.GetCoins());

    }

    void Update()
    {
        // Només actualitza el text si el nombre de monedes ha canviat
        int newCoins = ScoreManager.instance.GetCoins();
        if (newCoins != currentCoins)
        {
            UpdateCoinText(newCoins);
        }
    }

    private void UpdateCoinText(int coins)
    {
        currentCoins = coins;
        text.text = $"{coins}"; // Actualitza el text del HUD
    }
}