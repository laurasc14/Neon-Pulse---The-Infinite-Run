using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string powerUpName; // Nom del power-up per mostrar-lo
    public float duration = 5f; // Durada comuna del Power-Up
    public bool isPerma;

    PowerUpUIManager uiManager;

    // Aquest mètode serà implementat pels fills
    //public abstract void ApplyEffect(MovimientoCapusla player);

    public virtual void Start()
    {
        uiManager = FindObjectOfType<PowerUpUIManager>(); // Troba el manager de UI
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger activat per: " + other.name);
        if (other.gameObject.CompareTag("Player"))
        {
            
            MovimientoCapusla movimiento = other.gameObject.GetComponent<MovimientoCapusla>();

            if (movimiento != null)
            {
                // Activa el power-up i comunica-ho a la UI
                //uiManager = FindObjectOfType<PowerUpUIManager>(); // Troba el manager de UI

                if (uiManager != null)
                {
                    Debug.Log("no entrea");
                    uiManager.DisplayPowerUp(powerUpName, duration); // Mostra el power-up a la UI
                    ApplyPlayerEfect(movimiento.gameObject, duration);
                    
                }

                GetComponent<Collider>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);

                //ApplyEffect(movimiento); // Aplica l'efecte específic
                if (isPerma)
                {
                    gameObject.SetActive(false);
                }
                else {
                    Invoke("RevertirEfecto", duration);
                }
            }
            else
            {
                Debug.LogError("MovimientoCapusla no trobat al jugador!");
            }
        }
    }

    public virtual void RevertirEfecto() {
        
    }
    public virtual void ApplyPlayerEfect(GameObject targetObject, float duration) { }

}
