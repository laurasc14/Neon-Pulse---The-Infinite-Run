using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePresetMovement : MonoBehaviour
{
    public float lateralAmplitude = 0.5f;
    public float lateralSpeed = 1.0f;
    private Vector3 initialPosition;
    private float randomOffset;

    void Start()
    {
        initialPosition = transform.position;
        randomOffset = Random.Range(0f, 100f); // per desincronitzar el moviment
    }

    void Update()
    {
        // Mou enrere en l'eix Z (com el central)
        transform.position -= Vector3.forward * GameSpeedController.ScrollSpeed * Time.deltaTime;

        // Oscil·lació lateral (X) o vertical (Y)
        float lateralMovement = Mathf.Sin(Time.time * lateralSpeed + randomOffset) * lateralAmplitude;
        transform.position = new Vector3(
            initialPosition.x + lateralMovement,
            initialPosition.y, // O pots afegir oscil·lació en Y si vols
            transform.position.z
        );

        // Elimina quan passa la zona de tall
        if (transform.position.z < -30f)
        {
            var envGen = FindObjectOfType<EnviromentGenerator>();
            if (envGen != null)
            {
                envGen.RemoveRoad(gameObject);
            }
        }
    }
}
