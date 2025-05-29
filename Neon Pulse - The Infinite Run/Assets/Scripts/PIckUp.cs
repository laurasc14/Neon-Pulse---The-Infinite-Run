using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckUp : MonoBehaviour
{

    public int coinValue = 1; // Valor de la moneda
    public int points = 10;   // Punts que atorga la moneda
    private bool isCollected = false; // Marca si la moneda ja ha estat recollida

    void Start()
    {
        DifficultyController.instance.AddBottellaTotal();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isCollected) // Comprova que la moneda no estigui recollida
        {
            // Afegeix la moneda
            ScoreManager.instance.AddCoin(coinValue);

            // Afegeix punts
            ScoreManager.instance.AddPoints(points);

            DifficultyController.instance.AddBottellaRecogida();

            // Marca la moneda com recollida
            isCollected = true;

            // Destrueix la moneda
            Destroy(gameObject);
        }
    }

    public void SetCollected(bool value)
    {
        isCollected = value;
    }
}
