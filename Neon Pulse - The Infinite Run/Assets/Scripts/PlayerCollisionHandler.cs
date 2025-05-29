using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public int collisionsBeforeDeath = 2;
    private int currentCollisions = 0;
    public float safeDistance = 10f;

    private float speedReductionTime = 2f;
    private bool isSlowed = false;

    private MovimientoCapusla playerMovement; // Referència al moviment del jugador
    public GameObject policeCar;

    void Start()
    {
        playerMovement = GetComponent<MovimientoCapusla>(); // Assegura’t que tens aquest script
    }

    void Update()
    {
        // Reinicia el comptador si s'allunya molt
        if (Vector3.Distance(transform.position, policeCar.transform.position) > safeDistance)
        {
            currentCollisions = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PoliceCar"))
        {
            currentCollisions++;

            if (currentCollisions >= collisionsBeforeDeath)
            {
                Die();
            }
            else
            {
                Debug.Log("Primera col·lisió! Reduint velocitat...");
                if (!isSlowed)
                    StartCoroutine(TemporarySlow());
            }
        }
    }

    System.Collections.IEnumerator TemporarySlow()
    {
        isSlowed = true;
        float originalSpeed = playerMovement.velocity;
        playerMovement.velocity *= 0.5f; // Redueix la velocitat a la meitat
        yield return new WaitForSeconds(speedReductionTime);
        playerMovement.velocity = originalSpeed;
        isSlowed = false;
    }

    void Die()
    {
        Debug.Log("Jugador mort!");
        // Aquí pots posar animació, reload de l'escena, etc.
    }
}
