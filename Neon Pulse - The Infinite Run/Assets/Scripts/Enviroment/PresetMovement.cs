using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * GameSpeedController.ScrollSpeed * Time.deltaTime;

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
